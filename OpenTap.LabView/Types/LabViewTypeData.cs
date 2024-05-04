using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NationalInstruments.LabVIEW.Interop;
using OpenTap.LabView.Utils;
using YamlDotNet.Core;
using YamlDotNet.RepresentationModel;

namespace OpenTap.LabView.Types
{
    static class YamlExtensions
    {
        public static string GetString(this YamlMappingNode node, string key)
        {
            if (node.Children.TryGetValue(new YamlScalarNode(key), out var node2) && node2 is YamlScalarNode scalarNode)
                return scalarNode.Value;
            
            return null;
        }
        public static YamlMappingNode GetNode(this YamlMappingNode node, string key)
        {
            if (node != null && node.Children.TryGetValue(new YamlScalarNode(key), out var node2) && node2 is YamlMappingNode mappingNode)
                return mappingNode;
            
            
            return null;
        }
    }
    
    /// <summary>
    /// This class represent a type from labview transformed into OpenTAP concepts.
    /// </summary>
    class LabViewTypeData : ITypeData
    {
        internal static string LvObjectConvertToString(object obj)
        {
            if(obj is LVClassRoot lvClassRoot)
                return  $"LabViewObject: 0x{lvClassRoot.InstanceIndex.ToInt64():X8}";
            
            var t = obj.GetType();
            if (LabViewMemberData.KnownClusterTypes.Contains(t))
            {

                // just print the pointer value of the object.
                return string.Join(" | ", t.GetFields().Select(field => LvObjectConvertToString(field.GetValue(obj))));
            }
            return obj?.ToString() ?? "<null>";
        } 
        public override string ToString() => $"labview:{this.Name}";
        
        IMemberData[] customMemberDataCache;
        readonly LabViewMemberData[] members;
        
        public MethodInfo Method { get;  }

        public LabViewTypeData(MethodInfo method, Type type, LabViewAssembly labViewAssembly, string docString = null)
        {
            this.LabViewAssembly = labViewAssembly;
            Method = method;
            var attributes = new List<object>();
            string displayName = Method.Name.FixString();
            string description = null;
            
            if (docString == null) docString = "";
            YamlMappingNode root = null;
            if (string.IsNullOrEmpty(docString) == false)
            {
                var parser = new Parser(new StringReader(docString));
                var str = new YamlStream();
                try
                {
                    str.Load(parser);
                    var docs = str.Documents.FirstOrDefault();
                    if (docs?.RootNode is YamlScalarNode rootScalar)
                    {
                        description = rootScalar?.Value;
                    }
                    root = docs?.RootNode as YamlMappingNode;
                }
                catch
                {
                    description = docString;
                }
            }
            if (root != null)
            {
                displayName = root.GetString("Display") ?? displayName;
                description = root.GetString("Description") ?? description;
            }
            
            
            
            var displayAttribute = new DisplayAttribute(displayName, description, null, -10000, false, type.Name == "LabVIEWExports" ? new []{"LabVIEW"} : new [] { "LabVIEW", type.Name.FixString() });
            attributes.Add(displayAttribute);

            Attributes = attributes;

            Name = GenerateTypeDataName(Method);
            BaseType = TypeData.FromType(typeof(LabViewTestStep));
            
            // parameters are the settings in this mode.
            ParameterInfo[] parameters = method.GetParameters();
            members = parameters.Select(p =>
            {
                
                var name = p.Name;
                name = name.Substring(0, 1).ToUpper() + name.Substring(1);
                var node2 = root.GetNode(p.Name) ?? root.GetNode(name);
                return new LabViewMemberData(this, p, node2?.GetString("Display"), node2?.GetString("Description"), node2?.GetString("Unit"));
            }).ToArray();

        }
        
        internal LabViewAssembly LabViewAssembly { get; }
        
        /// <summary> The attributes of the type. </summary> 
        public IEnumerable<object> Attributes { get; }

        /// <summary> The name of the type. </summary>
        public string Name { get; }

        /// <summary> The members of the type. </summary>
        public IEnumerable<IMemberData> GetMembers()
        {
            var baseMembers = BaseType.GetMembers();
            return baseMembers.Concat(customMemberDataCache ?? (customMemberDataCache = getCustomMembers().ToArray()));
        }

        IEnumerable<IMemberData> getCustomMembers() => members;
         
        /// <summary> Get a member by name. </summary>
        public IMemberData GetMember(string name) => members.FirstOrDefault(x => x.Name == name) ?? BaseType.GetMember(name);

        /// <summary> Create an instance of the type. </summary>
        public object CreateInstance(object[] arguments)
        {
            return new LabViewTestStep(this);
        }

        /// <summary> The base type of the type. </summary>
        public ITypeData BaseType { get; }

        /// <summary> The attributes of the type. </summary>
        public bool CanCreateInstance => true;

        // Equals / GetHashCode must be overridden, otherwise these new types will not work well.
        public override bool Equals(object obj)
        {
            if (obj is LabViewTypeData otherType)
                return otherType.Name == Name;
                    
            return base.Equals(obj);
        }

        public override int GetHashCode() => Name.GetHashCode() * 3128321;

        static string GenerateTypeDataName(MemberInfo descriptor)
        {
            return $"{LabViewTypeDataProvider.PREFIX}{descriptor.DeclaringType.Name + "." + descriptor.Name}";
        }
    }

}