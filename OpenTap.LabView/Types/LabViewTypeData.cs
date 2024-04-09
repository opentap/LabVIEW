using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenTap.LabView.Utils;

namespace OpenTap.LabView.Types
{
    /// <summary>
    /// This class represent a type from labview transformed into OpenTAP concepts.
    /// </summary>
    class LabViewTypeData : ITypeData
    {
        public override string ToString() => $"labview:{this.Name}";
        
        IMemberData[] customMemberDataCache;
        readonly LabViewMemberData[] members;
        
        public MethodInfo Method { get;  }

        public LabViewTypeData(MethodInfo method, Type type, LabViewAssembly labViewAssembly)
        {
            this.LabViewAssembly = labViewAssembly;
            Method = method;
            var attributes = new List<object>();
            
            
            var displayAttribute = new DisplayAttribute(Method.Name.FixString(), "", null, -10000, false, type.Name == "LabVIEWExports" ? new []{"LabVIEW"} : new [] { "LabVIEW", type.Name.FixString() });
            attributes.Add(displayAttribute);

            Attributes = attributes;

            Name = GenerateTypeDataName(Method);
            BaseType = TypeData.FromType(typeof(LabViewTestStep));
            
            // parameters are the settings in this mode.
            ParameterInfo[] parameters = method.GetParameters();
            members = parameters.Select(p => new LabViewMemberData(this, p)).ToArray();

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