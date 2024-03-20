using NationalInstruments.LabVIEW.Interop;
namespace OpenTap.LabView.Types
{
    /// <summary>
    /// This class provides a read-only view of LvClassRoot objects.
    /// </summary>
    class LvClassStringReadOnlyStringValueAnnotation : IStringReadOnlyValueAnnotation
    {
        readonly AnnotationCollection annotation;
        public LvClassStringReadOnlyStringValueAnnotation(AnnotationCollection annotation)
        {
            this.annotation = annotation;
        }
        
        public string Value
        {
            get
            {
                var objValue = annotation.Get<IObjectValueAnnotation>()?.Value;
                if (objValue is LVClassRoot lvClassRoot)
                {
                    // just print the pointer value of the object.
                    objValue = $"LabViewObject: 0x{lvClassRoot.InstanceIndex.ToInt64():X8}";
                }
                
                return objValue?.ToString();
            }
        }
    }
}
