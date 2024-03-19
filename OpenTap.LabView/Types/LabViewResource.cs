using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using NationalInstruments.LabVIEW.Interop;
namespace OpenTap.LabView.Types
{   
     class LabViewResource : Instrument
    {
        public readonly LabViewTypeData PluginType;
        LVClassRoot labViewObject;
        internal object LabViewObject;
        public LabViewResource()
        {
            
        }

        [Browsable(false)]
        public Dictionary<string, object> Values
        {
            get;
            set;
        }
     
        public LabViewResource(LabViewTypeData labViewPluginTypeData)
        {
            this.PluginType = labViewPluginTypeData;
            var tp = this.PluginType.Type;
            var ctor = tp.GetMethod(tp.Name, BindingFlags.Static | BindingFlags.Public);
            if (ctor != null)
            {
                var parameters = new object[1];
                ctor.Invoke(null, parameters);
               
                this.LabViewObject = parameters[0];
            }
            else
            {
                var innerObject = Activator.CreateInstance(this.PluginType.Type);
                LabViewObject = innerObject;
            }
    

        }

    }
}
