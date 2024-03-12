using System;
using System.Collections.Generic;
using System.Reflection;
namespace OpenTap.Plugins.LabView
{
    public class LabViewMemberData : IMemberData
    {
        public LabViewMemberData(LabViewTypeData labViewTypeData, ParameterInfo parameterInfo)
        {
            
            Attributes = Array.Empty<object>();
            if (parameterInfo.IsOut)
            {
                Attributes = new[]
                {
                    new OutputAttribute()
                };
            }
            Name = parameterInfo.Name;
            DeclaringType = labViewTypeData;
            TypeDescriptor = TypeData.FromType(parameterInfo.ParameterType);
            Writable = true;
            Readable = true;
        }

        public IEnumerable<object> Attributes
        {
            get;
        }
        public string Name
        {
            get;
        }
        public void SetValue(object owner, object value)
        {
            ((LabViewTestStep)owner).Values[this.Name] = value;
        }
        public object GetValue(object owner)
        {
            if (((LabViewTestStep)owner).Values.TryGetValue(this.Name, out var current))
            {
                return current;
            }
            return null;
        }
        public ITypeData DeclaringType
        {
            get;
        }
        public ITypeData TypeDescriptor
        {
            get;
        }
        public bool Writable
        {
            get;
        }
        public bool Readable
        {
            get;
        }
    }
}
