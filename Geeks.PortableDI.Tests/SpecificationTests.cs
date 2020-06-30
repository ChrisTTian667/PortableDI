using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geeks.DependencyInjection.Specifications;
using Geeks.DependencyInjection;

namespace Geeks.PortableDI.Tests
{
    [TestClass]
    public class SpecificationTests
    {
        [TestMethod]
        public void NameRegistration_ReturnsTrue()
        {
            var spec = new NameRegistrationSpecification("MyRegName");
            var request = new Request(typeof(object), "MyRegName");
            Assert.IsTrue(spec.Satisfied(request));
        }

        [TestMethod]
        public void NameRegistration_CamelcaseMisstake_ReturnsFalse()
        {
            var spec = new NameRegistrationSpecification("MyRegname");
            var request = new Request(typeof(object), "MyRegName");
            Assert.IsFalse(spec.Satisfied(request));
        }

        [TestMethod]
        public void TypeSpecification_ReturnsTrue()
        {
            var spec = new TypeSpecification(typeof(IEmptyClass));
            var request = new Request(typeof(IEmptyClass));
            Assert.IsTrue(spec.Satisfied(request));
        }

        [TestMethod]
        public void TypeSpecification_ReturnsFalse()
        {
            var spec = new TypeSpecification(typeof(IEmptyClass));
            var request = new Request(typeof(IParamClass));
            Assert.IsFalse(spec.Satisfied(request));
        }
        
    }
}
