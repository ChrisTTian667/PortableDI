using System;
using Geeks.DependencyInjection.Resolvers;
using Geeks.DependencyInjection.Syntax;

namespace Geeks.DependencyInjection
{
    internal class BindingBuilder<T> : IBindingAndOrToSyntax<T>, IBindingAndSyntax<T>, IBindingToSyntax<T>, IBindingInSyntax<T>
    {
        private readonly Binding _binding;
        private readonly IDIContainer _container;


        public BindingBuilder(Binding binding, IDIContainer container)
        {
            _binding = binding;
            _container = container;
        }

        #region IBindingToSyntax<T> Members

        public IBindingInSyntax<T> To<TImplementation>() where TImplementation : T
        {
            _binding.Resolver = new StandardResolver(typeof(TImplementation), _container);
            return this;
        }

        public IBindingInSyntax<T> To<TImplementation>(string registrationName) where TImplementation : T
        {
            _binding.Specifications.Add(new Specifications.NameRegistrationSpecification(registrationName));
            return this.To<TImplementation>();
        }

        public IBindingInSyntax<T> To(Type type)
        {
            _binding.Resolver = new StandardResolver(type,_container);
            return this;
        }

        public IBindingInSyntax<T> ToMethod(Func<IRequest, T> method)
        {
            _binding.Resolver = new MethodCallResolver<T>(method);
            return this;
        }

        public IBindingInSyntax<T> ToMethod(Func<T> method)
        {
            _binding.Resolver = new MethodCallResolver<T>(method);
            return this;
        }

        public void ToInstance(T instance)
        {
            _binding.ServiceInstance = instance;
            _binding.Scope = BindingScope.Singleton;
        }

        #endregion IBindingToSyntax<T> Members

        #region IBindingInSyntax<T> Members

        public void InSingletonScope()
        {
            _binding.Scope = BindingScope.Singleton;
        }

        public void InThreadScope()
        {
            _binding.Scope = BindingScope.Thread;
        }

        #endregion IBindingInSyntax<T> Members

    }
}