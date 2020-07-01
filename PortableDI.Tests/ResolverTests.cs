using System;
using NUnit.Framework;
using PortableDI.Resolvers;

namespace PortableDI.Tests
{
    [TestFixture]
    public class ResolverTests
    {
        private IDIContainer _container;

        [OneTimeSetUp]
        public void Initialize()
        {
            _container = new StandardDIContainer();
        }

        [Test]
        public void MethodCallResolver_ThrowsExcpeption_ThrowsResolverException()
        {
            var resolver = new MethodCallResolver<EmptyCtorClass>(() => throw new ArgumentException("Test Exception"));
            Assert.That(() => resolver.Resolve(new Request(typeof(IEmptyClass))),
                Throws.TypeOf(typeof(ResolverException)));
        }

        [Test]
        public void MethodCallResolver_WithoutRequest_ReturnsNotNull()
        {
            var resolver = new MethodCallResolver<EmptyCtorClass>(() => { return new EmptyCtorClass(); });

            var result = resolver.Resolve(new Request(typeof(IEmptyClass)));

            Assert.IsTrue(result is EmptyCtorClass);
        }

        [Test]
        public void MethodCallResolver_WithRequest_ReturnsNotNull()
        {
            var resolver = new MethodCallResolver<EmptyCtorClass>(r => new EmptyCtorClass());
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
        public void StandardResolver_WithEmptyCtor_ReturnsTestInstance()
        {
            var resolver = new StandardResolver(typeof(EmptyCtorClass), _container);
            var result = resolver.Resolve(new Request(typeof(IEmptyClass)));

            Assert.IsTrue(result is EmptyCtorClass);
        }
    }
}