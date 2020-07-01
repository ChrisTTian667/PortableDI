using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Geeks.DependencyInjection
{
    internal class StandardDIContainer : IDIContainer
    {
        private readonly IList<Binding> _bindings = new List<Binding>();

        public StandardDIContainer()
        {
        }

        internal void FlushBindings()
        {
            _bindings.Clear();
        }

        private IBindingAndOrToSyntax<T> InnerBind<T>()
        {
            var binding = new Binding(typeof(T));
            _bindings.Add(binding);
            return new BindingBuilder<T>(binding, this);
        }

        private T InnerResolve<T>(string registrationName = null)
        {
            var request = new Request(typeof(T), registrationName);

            var binding = ResolveBinding(request);

            if (binding == null)
                throw new ResolverException(string.Format(Resources.Exception_NoBindingFound, request.ToString()));

            return (T)binding.Resolve(request);
        }

        private Binding ResolveBinding(Request request)
        {
            return _bindings.FirstOrDefault(
                b => b.Matches(request));
        }

        #region IDIContainer Members

        public IBindingAndOrToSyntax<T> Bind<T>()
        {
            return InnerBind<T>();
        }

        public IBindingAndOrToSyntax<object> Bind(Type service)
        {
            var binding = new Binding(service);
            _bindings.Add(binding);
            return new BindingBuilder<object>(binding, this);
        }

        public void Load(IDIPackage package)
        {
            package.Load(this);
        }

        public T Resolve<T>()
        {
            return this.InnerResolve<T>();
        }

        public T Resolve<T>(string registrationName)
        {
            return this.InnerResolve<T>(registrationName);
        }

        public object Resolve(Type service)
        {
            MethodInfo method = typeof(StandardDIContainer).GetMethod("Resolve", new Type[] { });
            MethodInfo genericMethod = method.MakeGenericMethod(service);

            try
            {
                return genericMethod.Invoke(this, new object[] { });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        public bool Unbind(Type service)
        {
            var request = new Request(service, null);
            var binding = ResolveBinding(request);

            _bindings.Remove(binding);

            return binding != null;
        }

        #region IsBound

        public bool IsBound(Type requestedService)
        {
            return InnerIsBound(requestedService);
        }

        public bool IsBound<T>()
        {
            return InnerIsBound(typeof(T));
        }

        private bool InnerIsBound(Type requestedService, string registrationName = null)
        {
            var request = new Request(requestedService, registrationName);
            return (ResolveBinding(request) != null);
        }

        #endregion IsBound

        public IEnumerable<T> ResolveAll<T>()
        {
            var request = new Request(typeof(T));

            var bindings = ResolveAllBindings(request).ToArray();

            if (!bindings.Any())
                return new List<T>();

            return new List<T>(bindings.Select(b => (T)b.Resolve(request)));
        }

        private IEnumerable<Binding> ResolveAllBindings(Request request)
        {
            return _bindings.Where(b => b.Matches(request));
        }

        #endregion IDIContainer Members
    }
}