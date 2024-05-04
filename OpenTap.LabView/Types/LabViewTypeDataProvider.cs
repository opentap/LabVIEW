using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;
using NationalInstruments.LabVIEW.Interop;

namespace OpenTap.LabView.Types
{

    /// <summary>
    /// The type data provider that loads the VEE files and generates the collection of VEE type data.
    /// </summary>
    public class LabViewTypeDataProvider : ITypeDataProvider, ITypeDataSearcher, ITypeDataSourceProvider
    {
        static LabViewTypeDataProvider()
        {
         
        }
        
        internal const string PREFIX = "vi:";
        
        static readonly TraceSource log = Log.CreateSource("LabView");
        static readonly List<LabViewTypeData> LabViewTypes = new List<LabViewTypeData>();

        public IEnumerable<ITypeData> Types => LabViewTypes.Select(x => x);
        ITypeDataSource ITypeDataSourceProvider.GetSource(ITypeData typeData)
        {
            if (typeData is LabViewTypeData lvType)
            {
                return lvType.LabViewAssembly;
            }
            return null;
        }

        /// <summary> Locate a type based on string identifier. </summary>
        public ITypeData GetTypeData(string identifier)
        {
            if (identifier.StartsWith(PREFIX))
                return LabViewTypes.FirstOrDefault(type => type.Name == identifier);
            
            return null;
        }

        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ITypeData GetTypeData(object obj)
        {
            if (obj is LabViewTestStep step)
                return step.PluginType;
            
            return null;
        }

        bool IsLabViewPluginAssembly(string file)
        {
            // If the dll refers to NationalInstrments.LabVIEW.interop
            // then it is likely to be a LabVIEW plugin assembly.
            if (file.Contains("Dependencies"))
            {
                return false;
            }
            using (FileStream str = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                if (str.Length > int.MaxValue)
                    return false; // otherwise PEReader() will throw.
                if (str.Length < 1000)
                    return false; // Don't consider super small assemblies.
                using (PEReader header = new PEReader(str, PEStreamOptions.LeaveOpen))
                {
                    if (!header.HasMetadata)
                        return false;
                    MetadataReader metadata = header.GetMetadataReader();
                    foreach (var asmRefHandle in metadata.AssemblyReferences)
                    {
                          var asmRef = metadata.GetAssemblyReference(asmRefHandle);
                          var name = metadata.GetString(asmRef.Name);
                          if (name.Contains("NationalInstruments.LabVIEW.Interop"))
                              return true;


                    } 
                }
            }
            return false;
        }

        /// <summary>  Priority slightly higher than default (required). </summary>
        public double Priority => 1;

        /// <summary> Load LabView DLLs and load labview types. </summary>
        public void Search()
        {
            var sw = Stopwatch.StartNew();
            log.Debug("Searching for LabVIEW plugins.");
            object lvRuntimeLoadLock = new object();
            var allAssemblies = PluginManager.GetSearcher().Assemblies.ToArray();

            foreach (var asm in allAssemblies)
            {
                
                bool isVi = IsLabViewPluginAssembly(asm.Location);
                
                if (!isVi) continue;
                
                
                var asm2 = asm.Load();
                { 
                    // try pre-loading the labview runtime engine
                    // otherwise this tends to take a few seconds the first time the test plan starts.
                    // this way, it can happen concurrently while everything gets loaded.
                    var lvClient = asm2.DefinedTypes.FirstOrDefault(t => t.Name == "LvClient");
                    if (lvClient != null)
                    {
                        var fld = lvClient.GetField("LVRuntimeInst", BindingFlags.Static | BindingFlags.NonPublic);
                        if (fld != null)
                        {
                            // do it in a separate thread to avoid slowing everyone else down.
                            // this usually takes around 3 seconds.
                            TapThread.Start(() =>
                            {
                                try
                                {
                                    lock (lvRuntimeLoadLock)
                                    {
                                        var sw2 = Stopwatch.StartNew();
                                        // this where the magic happens
                                        fld.GetValue(null);
                                        var el = sw2.Elapsed;
                                        log.Debug(el, $"Started LabVIEW runtime for {asm.Location}.");
                                    }
                                }
                                catch
                                {
                                    
                                }
                            });
                        }
                    }
                }
                var labViewAssembly = new LabViewAssembly(asm);

                Type[] types = asm2.GetTypes();

                var xmlDocFile = Path.ChangeExtension(labViewAssembly.Location, "xml");
                XDocument xdoc = null;
                if (File.Exists(xmlDocFile))
                {
                    try
                    {
                        xdoc = XDocument.Load(xmlDocFile);
                    }
                    catch(Exception e)
                    {
                        log.Error($"Unable to load xml doc file: {xmlDocFile}");
                        log.Debug(e);
                    }
                }
                Dictionary<string, string> memberSummary = null;
                if (xdoc != null)
                {
                    memberSummary = new Dictionary<string, string>();
                    var members = xdoc.Root?.Element("members")?.Elements("member");
                    if (members != null)
                    {
                        foreach (var member in members)
                        {
                            var summary = member.Element("summary")?.Value;
                            if (string.IsNullOrWhiteSpace(summary)) continue;
                            var name2 = member.Attribute("name")?.Value;
                            if (string.IsNullOrWhiteSpace(name2)) continue;
                            var name3 = name2.Split(':').ElementAtOrDefault(1)?.Split('(')?.FirstOrDefault();
                            memberSummary[name3] = summary;
                        }           
                    }
                }

                foreach (Type type in types)
                {
                    if (typeof(LVClassRoot).IsAssignableFrom(type) == false && type.Name != "LabVIEWExports")
                        continue;
             
                    var declaredMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly);
                    foreach (var method in declaredMethods)
                    {
                        var fullName = method.DeclaringType.FullName + "." + method.Name;
                        string doc = null;
                        if (memberSummary != null)
                        {
                            memberSummary.TryGetValue(fullName, out doc);
                        }
                        var newType = new LabViewTypeData(method, type, labViewAssembly, doc);
                        labViewAssembly.LabViewTypes.Add(newType);
                        LabViewTypes.Add(newType);
                    }
                }

            }
            if (LabViewTypes.Count == 0)
            {
                log.Debug(sw, "No LabVIEW plugin types discovered.");
            }
            else
            {
                log.Debug(sw, $"Found {LabViewTypes.Count} LabVIEW plugins.");
            }
        }
    }

}
