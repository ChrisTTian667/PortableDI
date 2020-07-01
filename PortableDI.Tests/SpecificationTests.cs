using NUnit.Framework;
using PortableDI.Specifications;

namespace PortableDI.Tests
{
    [TestFixture]
    public class SpecificationTests
    {
        [Test]
        public void NameRegistration_CamelcaseMisstake_ReturnsFalse()
        {
            var spec = new NameRegistrationSpecification("MyRegname");
            var request = new Request(typeof(object), "MyRegName");
            Assert.IsFalse(spec.Satisfied(request));
        }

        [Test]
        public void NameRegistration_ReturnsTrue()
        {
            var spec = new NameRegistrationSpecification("MyRegName");
            var request = new Request(typeof(object), "MyRegName");
            Assert.IsTrue(spec.Satisfied(request));
        }

        [Test]
        public void TypeSpecification_ReturnsFalse()
        {
            var spec = new TypeSpecification(typeof(IEmptyClass));
            var request = new Request(typeof(IParamClass));
            Assert.IsFalse(spec.Satisfied(request));
        }

        [Test]
        public void TypeSpecification_ReturnsTrue()
        {
            var spec = new TypeSpecification(typeof(IEmptyClass));
            var request = new Request(typeof(IEmptyClass));
            Assert.IsTrue(spec.Satisfied(request));
        }
    }
}