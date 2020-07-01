using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using PortableDI.Syntax;

[assembly: InternalsVisibleTo("PortableDI.Tests, PublicKey=00240000048000009400000006020000002400005253413100040000010001002f8670765df1fa" + 
                                                           "e6d8d9096536f0215abfcd4a5a661db3ca56fc50ed7b212c5033594deb6655554f76ddf803d1d1" + 
                                                           "75dba8e5907cec75259671d69f3aba0477eedd421289d12b7894f164981c2fa4d5aa953d4ee884" + 
                                                           "531e334958e2f9e52c950bda1834f7328b445c7818847efa20c53f98ba38f9df3aa17e02ba053a" + 
                                                           "e034f888")]
namespace PortableDI
{
    internal class StandardDIContainer : IDIContainer
    {
        private readonly IList<Binding> _bindings = new List<Binding>();

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
                throw new ResolverException(string.Format(Resources.Exception_NoBindingFound, request));

            return (T) binding.Resolve(request);
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
            return InnerResolve<T>();
        }

        public T Resolve<T>(string registrationName)
        {
            return InnerResolve<T>(registrationName);
        }

        public object Resolve(Type service)
        {
            var method = typeof(StandardDIContainer).GetMethod("Resolve", new Type[] { });
            var genericMethod = method.MakeGenericMethod(service);

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
            var request = new Request(service);
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
            return ResolveBinding(request) != null;
        }

        #endregion IsBound

        public IEnumerable<T> ResolveAll<T>()
        {
            var request = new Request(typeof(T));

            var bindings = ResolveAllBindings(request).ToArray();

            if (!bindings.Any())
                return new List<T>();

            return new List<T>(bindings.Select(b => (T) b.Resolve(request)));
        }

        private IEnumerable<Binding> ResolveAllBindings(Request request)
        {
            return _bindings.Where(b => b.Matches(request));
        }

        #endregion IDIContainer Members
    }
}