using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NationalInstruments.LabVIEW.Interop;
using OpenTap.Plugins.LabviewInterop;

namespace OpenTap.Plugins.LabView
{
    public class LvClassRootList : ComponentSettingsList<LvClassRootList, LVClassRoot>
    {
        
    }
    
    /// <summary>
    /// The type data provider that loads the VEE files and generates the collection of VEE type data.
    /// </summary>
    public class LabViewTypeDataProvider : ITypeDataProvider, ITypeDataSearcher
    {
        internal const string PREFIX = "vi:";

        internal static List<ITypeData> LabViewTypes = new List<ITypeData>();
        HashSet<Type> lvClassRoot = new HashSet<Type>();

        public IEnumerable<ITypeData> Types => LabViewTypes.Select(x => x);

        public LabViewTypeDataProvider()
        {
            
        }

        /// <summary>
        /// Locate a type based on string identifier.
        /// </summary>
        /// <param name="identifier">Prefix with "VEE:" if it is a VeeTypeData.</param>
        /// <returns></returns>
        public ITypeData GetTypeData(string identifier)
        {
            if (identifier.StartsWith(PREFIX))
            {
                //...
            }
            return null;
        }

        /// <summary>
        /// Identify if the type data of an object is a VeeTypeData. This is done by checking if its a VeeTestStep with an associated type.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ITypeData GetTypeData(object obj)
        {
            if (obj is LabViewTestStep step)
                return step.PluginType;
            return null;
        }

        /// <summary>  Priority slightly higher than default (required). </summary>
        public double Priority => 1;

        /// <summary> Load LabView DLLs and load labview types. </summary>
        public void Search()
        {
            var allAssemblies = PluginManager.GetSearcher().Assemblies.ToArray();
            
            foreach (var asm in allAssemblies)
            {
                bool isVi = false;
                foreach (var reference in asm.References)
                {
                    if (reference.Name.Contains("LabVIEW.Interop"))
                    {
                        isVi = true;
                        break;
                    }
                }
                if (isVi)
                {
                    var asm2 = asm.Load();
                    
                    Type[] types = asm2.GetTypes();

                    List<MethodInfo> methods = new List<MethodInfo>();
                    foreach (Type type in types)
                    {
                        MethodInfo[] declaredMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly);
                        methods.AddRange(declaredMethods);
                    }

                    foreach (MethodInfo method in methods)
                    {
                        var newType = new LabViewTypeData(method);
                        LabViewTypes.Add(newType);
                        foreach (var parameter in method.GetParameters())
                        {
                            if (typeof(LVClassRoot).IsAssignableFrom(parameter.ParameterType))
                            {
                                lvClassRoot.Add(parameter.ParameterType);
                            }
                        }
                        
                    }   
                }
                foreach (var thing in lvClassRoot)
                {
                    LabViewTypes.Add(TypeData.FromType(thing));
                }
            }

        }
    }

}