using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geeks.DependencyInjection.Resolvers;
using Geeks.DependencyInjection;
using NUnit.Framework;

namespace Geeks.PortableDI.Tests
{
    

    [TestClass]
    public class ResolverTests
    {
        private IDIContainer _container;

        [TestFixtureSetUp]
        public void Initialize()
        {
            _container = new StandardDIContainer();
        }


        [Test]
        public void StandardResolver_WithEmptyCtor_ReturnsTestInstance()
        {
            var resolver = new StandardResolver(typeof(EmptyCtorClass), _container);
            var result = resolver.Resolve(new Request(typeof(IEmptyClass)));

            Assert.IsTrue(result is EmptyCtorClass);
        }

        [Test]
        public void StandardResolver_WithCtorParam_ReturnsTestInstance()
        {
            _container.Bind<IEmptyClass>().To<EmptyCtorClass>();
            var resolver = new StandardResolver(typeof(OneParamCtorClass), _container);
            var result = resolver.Resolve(new Request(typeof(IParamClass))) as OneParamCtorClass;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.CtorParam is EmptyCtorClass);
        }

        [Test]
        public void MethodCallResolver_WithRequest_ReturnsNotNull()
        {
            var resolver = new MethodCallResolver<EmptyCtorClass>(r =>
            {
                return new EmptyCtorClass();
            });

            var result = resolver.Resolve(new Request(typeof(IEmptyClass)));

            Assert.IsTrue(result is EmptyCtorClass);
        }

        [Test]
        public void MethodCallResolver_WithoutRequest_ReturnsNotNull()
        {
            var resolver = new MethodCallResolver<EmptyCtorClass>(() =>
            {
                return new EmptyCtorClass();
            });

            var result = resolver.Resolve(new Request(typeof(IEmptyClass)));

            Assert.IsTrue(result is EmptyCtorClass);
        }

        [Test]
        [ExpectedException(typeof(ResolverException))]
        public void MethodCallResolver_ThrowsExcpeption_ThrowsResolverException()
        {
            var resolver = new MethodCallResolver<EmptyCtorClass>(() =>
            {
                throw new ArgumentException("Test Exception");
            });

            resolver.Resolve(new Request(typeof(IEmptyClass)));
        }

    }
}
