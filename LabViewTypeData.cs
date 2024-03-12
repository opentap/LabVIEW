using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenTap.Plugins.LabView
{
    /// <summary>
    /// </summary>
    public class LabViewTypeData : ITypeData
    {
        IMemberData[] customMemberDataCache;
        readonly LabViewMemberData[] mem;

        public LabViewTypeData(MethodInfo method)
        {
            Method = method;
            var attributes = new List<object>();
            
            // how the function represents itself to the user.
            var displayAttribute = new DisplayAttribute(Method.Name, "", null, -10000, false, new string[] { "LabVIEW" });
            attributes.Add(displayAttribute);

            Attributes = attributes;

            Name = GenerateTypeDataName(Method);
            BaseType = TypeData.FromType(typeof(LabViewTestStep));
            
            ParameterInfo[] parameters = method.GetParameters();
            string paramList = string.Join(", ", Array.ConvertAll(parameters, p => p.ParameterType.Name + " " + p.Name));

            this.mem = parameters.Select(p => new LabViewMemberData(this, p)).ToArray();

        }
        public MethodInfo Method
        {
            get;
            set;
        }

        public IEnumerable<object> Attributes { get; }

        public string Name { get; }

        public IEnumerable<IMemberData> GetMembers()
        {
            var baseMembers = BaseType.GetMembers();

            // Must cache the custom MemberData. Otherwise, new custom MemberData gets created on every function called.
            // OpenTap uses object referencing in GUI and step settings connection.
            // Creating new custom MemberData on every request will cause lost connection and unexpected behaviour. 
            return baseMembers.Concat(customMemberDataCache ?? (customMemberDataCache = getCustomMembers().ToArray()));
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        IEnumerable<IMemberData> getCustomMembers() => mem;
        
        public IMemberData GetMember(string name) => mem.FirstOrDefault(x => x.Name == name) ?? BaseType.GetMember(name);

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
            {
                return otherType.Name == Name;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode() => Name.GetHashCode() * 3128321;

        public static string GenerateTypeDataName(MemberInfo descriptor)
        {
            return $"{LabViewTypeDataProvider.PREFIX}{descriptor.Name}";
        }
    }

}