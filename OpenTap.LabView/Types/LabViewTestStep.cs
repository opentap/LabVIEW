using NationalInstruments.LabVIEW.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("OpenTap.labView.UnitTests")]

namespace OpenTap.LabView.Types
{
    class LabViewTestStep : TestStep
    {
        public class SelectedOutput
        {
            public string MemberName { get; set; }
            public string DisplayName { get; set; }
            public override string ToString()
            {
                return DisplayName ?? MemberName ?? "<null>";
            }
        }
            
        internal readonly LabViewTypeData PluginType;

        readonly LabViewMemberData[] outputMembers;
        public IEnumerable<SelectedOutput> AvailableOutputs => outputMembers.Select(x => new SelectedOutput{MemberName = x.Name, DisplayName = x.GetDisplayAttribute()?.Name});
        
        [Display("Publish Results", "Select which outputs or inputs should be saved as results", Group:"Utils", Order:1)]
        [AvailableValues(nameof(AvailableOutputs))]
        public List<SelectedOutput> ResultMembers { get; set; } = new List<SelectedOutput>();
        
        [Display("Log Inputs and Outputs", "Should outputs and inputs be logged?", Group:"Utils", Order:1)]
        public bool LogMembers { get; set; } = true;
        
        public LabViewTestStep(LabViewTypeData type)
        {
            outputMembers = type.GetMembers().OfType<LabViewMemberData>().ToArray();
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
                throw;
            }

            for (int i = 0; i < p.Length; i++)
            {
                
                // if its an output, the value should be written back.
                if (p[i].IsOut)
                    Values[p[i].Name] = parameters[i];
            }

            if (ResultMembers.Any())
            {
                var columns = ResultMembers.Select(outp => outputMembers.FirstOrDefault(x => x.Name == outp.MemberName)).Where(item => item != null)
                    .Select(item =>
                    {
                        var result = item.GetValue(this);
                        if (!(result is Array))
                        {
                            var arr = Array.CreateInstance(result.GetType(), 1);
                            arr.SetValue(result, 0);
                            result = arr;
                        }
                        return new ResultColumn(item.Name, (Array)result);
                    }) .ToArray();
                Results.PublishTable(new ResultTable(this.GetFormattedName(), columns));
            }
            if(LogMembers)
            {
                Log.Info("{0}", string.Join(", ", Values.Select(kv => $"{kv.Key}: {LabViewTypeData.LvObjectConvertToString(kv.Value)}")));
            }
        }
    }
}
