using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NationalInstruments.LabVIEW.Interop;
namespace OpenTap.LabView.Types
{
    public class LabViewTestStep : TestStep
    {
        public LabViewTypeData PluginType;
        public LabViewTestStep(LabViewTypeData type)
        {
            
            this.PluginType = type;
            Values = new Dictionary<string, object>();
            
            foreach (var labViewMemberData in type.GetMembers().OfType<LabViewMemberData>())
            {
                if (labViewMemberData.TypeDescriptor.DescendsTo(typeof(Input<>)))
                {
                    labViewMemberData.SetValue(this, labViewMemberData.TypeDescriptor.CreateInstance(Array.Empty<object>()));
                }
            }
        }
        
        [Browsable(false)]
        public Dictionary<string,object> Values
        {
            get;
            set;
        }
        public override void Run()
        {
            var p = PluginType.Method.GetParameters();
            // get the parameters for the methods. These should correspond 1:1 with the values.
            var parameters = p.Select(x =>
            {
                Values.TryGetValue(x.Name, out var value);
                if (value is IInput i)
                {
                    value = i.Property.GetValue(i.Step);
                }
                return value;
            }).ToArray();
            
            PluginType.Method.Invoke(null, parameters);
            for (int i = 0; i < p.Length; i++)
            {
                
                if (p[i].IsOut)
                {
                    Values[p[i].Name] = parameters[i];
                }
            }
        }
    }
}
