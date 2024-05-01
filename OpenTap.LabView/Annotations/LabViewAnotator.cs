using System;
using NationalInstruments.LabVIEW.Interop;
namespace OpenTap.LabView.Types
{
    /// <summary>
    /// This class is used to enabled special UI annotation for LabView related objects.
    /// </summary>
    public class LabViewAnotator : IAnnotator
    {
        static Type AsType(ITypeData typeData)
        {
            while (typeData != null)
            {
                if (typeData is TypeData td)
                    return td.Type;
                typeData = typeData.BaseType;
            }
            return null;
        }
        
        public void Annotate(AnnotationCollection annotations)
        {
            var mem = annotations.Get<IMemberAnnotation>()?.Member;
            if (mem != null)
            {
                if (mem.TypeDescriptor.DescendsTo(typeof(LVClassRoot)))
                    annotations.Add(new LvClassStringReadOnlyStringValueAnnotation(annotations));
                else if (AsType(mem.TypeDescriptor) is Type t && LabViewMemberData.KnownClusterTypes.Contains(t))
                    annotations.Add(new ClusterStringReadOnlyStringValueAnnotation(annotations));
            }
        }
        public double Priority => 0.0;
    }
}
