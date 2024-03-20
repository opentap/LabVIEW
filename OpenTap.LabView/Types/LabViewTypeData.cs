using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenTap.LabView.Types
{
    /// <summary>
    /// This class represent a type from labview transformed into OpenTAP concepts.
    /// </summary>
    public class LabViewTypeData : ITypeData
    {
        public override string ToString() => $"labview:{this.Name}";
        
        IMemberData[] customMemberDataCache;
        readonly LabViewMemberData[] members;
        
        /// <summary>
        ///  LabViewTypeData Can either represent a resource (LvClass) or a test step(method) 
        /// </summary>
        public bool IsResource => Type != null;
        
        public Type Type { get; }
        public MethodInfo Method { get;  }

        public LabViewTypeData(MethodInfo method, Type type)
        {
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
        
        public IEnumerable<object> Attributes { get; }

        public string Name { get; }

        public IEnumerable<IMemberData> GetMembers()
        {
            var baseMembers = BaseType.GetMembers();
            return baseMembers.Concat(customMemberDataCache ?? (customMemberDataCache = getCustomMembers().ToArray()));
        }

        IEnumerable<IMemberData> getCustomMembers() => members;
        
        public IMemberData GetMember(string name) => members.FirstOrDefault(x => x.Name == name) ?? BaseType.GetMember(name);

        public object CreateInstance(object[] arguments)
        {

            return new LabViewTestStep(this)
            {
                Name = Method.Name
            };    
        }

        public ITypeData BaseType { get; }

        public bool CanCreateInstance => true;

        // Equals / GetHashCode must be overridden, otherwise these new types will not work well.
        public override bool Equals(object obj)
        {
            if (obj is LabViewTypeData otherType)
                return otherType.Name == Name;
                    
            return base.Equals(obj);
        }

        public override int GetHashCode() => Name.GetHashCode() * 3128321;

        public static string GenerateTypeDataName(MemberInfo descriptor)
        {
            return $"{LabViewTypeDataProvider.PREFIX}{descriptor.Name}";
        }
    }

}