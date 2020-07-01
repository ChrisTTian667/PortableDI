using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geeks.DependencyInjection;
using System.Collections;

namespace Geeks.PortableDI.Tests
{
    [TestClass]
    public class StandardDIContainerTests
    {
        private StandardDIContainer _container;

        [TestInitialize]
        public void Initialize()
        {
            _container = new StandardDIContainer();
        }

        [Test]
        public void Bind_NonGeneric_IsBound()
        {
            _container.Bind(typeof(IEmptyClass)).To<EmptyCtorClass>();

            Assert.IsTrue(_container.IsBound<IEmptyClass>());
        }

        [Test]
        public void Bind_Generic_IsBound()
        {
            _container.Bind<IEmptyClass>().To<EmptyCtorClass>();

            Assert.IsTrue(_container.IsBound<IEmptyClass>());
        }

        [Test]
        public void Bind_ToMethod_WithoutRequest_ReturnsInstance()
        {
            _container.Bind<IEmptyClass>().ToMethod(() => new EmptyCtorClass());

            Assert.IsNotNull(_container.Resolve<IEmptyClass>());
        }

        [Test]
        public void Bind_ToMethod_WithRequest_ReturnsInstance()
        {
            _container.Bind<IEmptyClass>().ToMethod(r => new EmptyCtorClass());

            Assert.IsNotNull(_container.Resolve<IEmptyClass>());
        }

        [Test]
        public void Resolve_ClassWithCtorParam_ReturnsFilledInstance()
        {
            _container.Bind<IEmptyClass>().To<EmptyCtorClass>();
            _container.Bind<IParamClass>().To<OneParamCtorClass>();
            var result = _container.Resolve<IParamClass>();
            Assert.IsNotNull(result);
        }

        [Test]
        [ExpectedException(typeof(ResolverException))]
        public void Resolve_ClassWithPrivateCtor_ThrowsResolverException()
        {
            _container.Bind<IEmptyClass>().To<PrivateCtorClass>();
            _container.Resolve<IEmptyClass>();
        }

        [Test]
        public void Resolve_WithSameRegistrationName_ThrowsNoException()
        {
            _container.Bind<IEmptyClass>().To<EmptyCtorClass>("regname");
            _container.Bind<IEnumerable>().To<ArrayList>("regname");
            var empty = _container.Resolve<IEmptyClass>("regname");
            var enumerable = _container.Resolve<IEnumerable>("regname");

            Assert.IsInstanceOfType(empty, typeof(EmptyCtorClass));
            Assert.IsInstanceOfType(enumerable, typeof(ArrayList));
        }

        [Test]
        public void ResolveAll_TwoClassesRegistered_ReturnsCount2()
        {
            _container.Bind<IEmptyClass>().To<EmptyCtorClass>();
            _container.Bind<IEmptyClass>().To<EmptyClassTwo>();
            var result = _container.ResolveAll<IEmptyClass>();
            Assert.IsTrue(result.Count() == 2);
        }

        [Test]
        public void ResolveAll_NoClassRegistered_ReturnsEmptyList()
        {
            var result = _container.ResolveAll<IEmptyClass>();
            Assert.IsTrue(result.Count() == 0);
        }
    }
}
