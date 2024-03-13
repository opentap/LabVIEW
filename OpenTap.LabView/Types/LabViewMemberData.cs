using System;
using System.Collections.Generic;
using System.Reflection;
using NationalInstruments.LabVIEW.Interop;
namespace OpenTap.LabView.Types
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
            
            // Generally Out parameters are marked ith a &. These we can just handle
            // by their underlying type. we know that they are references in the call.
            if (parameterInfo.IsOut && parameterInfo.ParameterType.Name == "String&")
                TypeDescriptor = TypeData.FromType(typeof(string));
            if (parameterInfo.IsOut && parameterInfo.ParameterType.Name == "Int32&")
                TypeDescriptor = TypeData.FromType(typeof(int));
            
            // if its a resource class we need to use the wrapper class.
            if (TypeDescriptor.DescendsTo(typeof(LVClassRoot)))
                TypeDescriptor = new LabViewTypeData(((TypeData)TypeDescriptor).Type);
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
