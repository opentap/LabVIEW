using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using InteropAssembly;
using NationalInstruments.LabVIEW.Interop;
namespace OpenTap.LabView.Types
{
    public class LabViewMemberData : IMemberData
    {
        public readonly object DefaultValue;
        public LabViewMemberData(LabViewTypeData labViewTypeData, ParameterInfo parameterInfo)
        {
            
            Attributes = Array.Empty<object>();
            bool writable = true;
            Name = parameterInfo.Name;
            if (parameterInfo.IsOut)
            {
                Attributes = new object[]
                {
                    new DisplayAttribute(Name.FixString()),
                    new OutputAttribute(),
                    new BrowsableAttribute(true)
                };
                writable = false;
            }
            else
            {
                Attributes = new object[]
                {
                    new DisplayAttribute(Name.FixString())
                };
            }
            
            
            DeclaringType = labViewTypeData;
            TypeDescriptor = TypeData.FromType(parameterInfo.ParameterType);
            
            // Generally Out parameters are marked ith a &. These we can just handle
            // by their underlying type. we know that they are references in the call.
            if (parameterInfo.IsOut && parameterInfo.ParameterType.Name.EndsWith("&"))
                TypeDescriptor = TypeData.FromType(parameterInfo.ParameterType.GetElementType());
            
            // if its a resource class we need to use the wrapper class.
            if (!parameterInfo.IsOut &&TypeDescriptor.DescendsTo(typeof(LVClassRoot)) && TypeDescriptor is TypeData td)
                TypeDescriptor = TypeData.FromType(typeof(Input<>).MakeGenericType(td.Type));
            Writable = writable;
            Readable = true;
            if (TypeDescriptor.DescendsTo(typeof(double)))
            {
                DefaultValue = 0.0;
            }
            if (TypeDescriptor.DescendsTo(typeof(int)))
            {
                DefaultValue = 0;
            }
            if (TypeDescriptor.DescendsTo(typeof(string)))
            {
                DefaultValue = "";
            }
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
            return DefaultValue;
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

    public struct InputObject<T> : IInput
    {

        public ITestStep Step
        {
            get;
            set;
        }
        public IMemberData Property
        {
            get;
            set;
        }
    }
}
