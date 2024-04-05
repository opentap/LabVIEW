﻿using NationalInstruments.LabVIEW.Interop;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenTap.LabView.Types
{
    public class LabViewTestStep : TestStep
    {
        internal readonly LabViewTypeData PluginType;
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
                else
                {
                    labViewMemberData.SetValue(this, labViewMemberData.DefaultValue);
                }
            }
            this.Name = type.GetDisplayAttribute().Name;
        }
        
        public Dictionary<string,object> Values { get; }
        
        public override void Run()
        {
            var p = PluginType.Method.GetParameters();
            // get the parameters for the methods. These should correspond 1:1 with the values.
            var parameters = p.Select(x =>
            {
                Values.TryGetValue(x.Name, out var value);
                
                // if its an IInput, it means that it should be connected to some output elsewhere in the test plan.
                if (value is IInput i)
                {
                    if (i.Property == null || i.Step == null)
                        value = null;
                    else
                        // fetch the value of the output.
                        value = i.Property.GetValue(i.Step);
                }
                return value;
            }).ToArray();

            try
            {
                PluginType.Method.Invoke(null, parameters);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is VIAssemblyException viEx)
                {
                    throw new Exception($"{viEx.ErrorSource} (Error Code: {viEx.ErrorCode})");
                }
                else
                    throw;
            }

            for (int i = 0; i < p.Length; i++)
            {
                
                // if its an output, the value should be written back.
                if (p[i].IsOut)
                    Values[p[i].Name] = parameters[i];
            }
        }
    }
}
