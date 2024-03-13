using System.Linq;
using OpenTap.LabView.Types;
using OpenTap.UnitTest;

namespace OpenTap.LabView.UnitTests
{  
    public class TypeDataTests : ITestFixture
    {
        static TraceSource log = Log.CreateSource("test");
        
        [Test]
        public void DetectPlugins()
        {
            // TODO: Add actual tests.
            var labViewResourceTypes = TypeData.GetDerivedTypes<Instrument>().ToArray();
            //var ins = labViewResourceTypes[1].CreateInstance();
        }

        [Test]
        public void AnnotateResource()
        {
            // TODO: Add actual tests.
            var stepTypes =TypeData.GetDerivedTypes<LabViewTestStep>().ToArray();
            var connectType = stepTypes.FirstOrDefault(x => x.Name.Contains("Connect"));
            var instance = connectType.CreateInstance();
            var a = AnnotationCollection.Annotate(instance);
            var member= a.Get<IMembersAnnotation>().Members.FirstOrDefault(x => x.Get<IMemberAnnotation>()?.Member.Name.Contains("ServiceFoo") ?? false);


        }
        
        
        
    }
}