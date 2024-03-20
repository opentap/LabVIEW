using NationalInstruments.LabVIEW.Interop;
namespace OpenTap.LabView.Types
{
    /// <summary>
    /// This class is used to enabled special UI annotation for LabView related objects.
    /// </summary>
    public class LabViewAnotator : IAnnotator
    {
        public void Annotate(AnnotationCollection annotations)
        {
            var mem = annotations.Get<IMemberAnnotation>()?.Member;
            if (mem != null && mem.TypeDescriptor.DescendsTo(typeof(LVClassRoot)))
            {
                annotations.Add(new LvClassStringReadOnlyStringValueAnnotation(annotations));
            }
        }
        public double Priority => 0.0;
    }
}
