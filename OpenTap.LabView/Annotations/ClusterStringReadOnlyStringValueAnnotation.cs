using System.Linq;
namespace OpenTap.LabView.Types
{
    /// <summary>
    /// This class provides a read-only view of LvClassRoot objects.
    /// </summary>
    class ClusterStringReadOnlyStringValueAnnotation : IStringReadOnlyValueAnnotation
    {
        readonly AnnotationCollection annotation;
        public ClusterStringReadOnlyStringValueAnnotation(AnnotationCollection annotation)
        {
            this.annotation = annotation;
        }
        
        public string Value
        {
            get
            {
                var objValue = annotation.Get<IObjectValueAnnotation>()?.Value;
                if(objValue != null) 
                {
                    // just print the pointer value of the object.
                    return string.Join(" | ", objValue.GetType().GetFields().Select(field => field.GetValue(objValue)?.ToString() ?? "<null>"));
                }
                
                return objValue?.ToString();
            }
        }
    }
}
