using System.Collections.Generic;
using System.ComponentModel;
namespace OpenTap.Plugins.LabView
{
    public class LabViewTestStep : TestStep
    {
        public ITypeData PluginType;
        public LabViewTestStep(ITypeData type)
        {
            this.PluginType = type;
            Values = new Dictionary<string, object>();
        }
        [Browsable(false)]
        public Dictionary<string,object> Values
        {
            get;
            set;
        }
        public override void Run()
        {
            
        }
    }
}
