using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Geeks.DependencyInjection.Resolvers;
using Geeks.DependencyInjection.Specifications;

namespace Geeks.DependencyInjection
{
    internal enum BindingScope
    {
        Normal,
        Singleton,
        Thread
    }

    internal class Binding : IBinding
    {
        public Binding(Type boundType)
        {
            Scope = BindingScope.Normal;
            Specifications = new List<IBindingSpecification>();
            // there is always a type specification !
            Specifications.Add(new Specifications.TypeSpecification(boundType));
        }

        internal IRequestResolver Resolver { get; set; }

        internal List<IBindingSpecification> Specifications { get; private set; }

        public object ServiceInstance { get; set; }

        private static readonly Dictionary<int, object> _instances = new Dictionary<int, object>();

        private static object ThreadStaticInstance
        {
            get
            {
                lock (_instances)
                {
                    object instance;
                    _instances.TryGetValue(Thread.CurrentThread.ManagedThreadId, out instance);
                    return instance;
                }
            }
            set
            {
                lock (_instances)
                {
                    // should throw exception if instance for threadid is allready inside - 
                    _instances.Add(Thread.CurrentThread.ManagedThreadId, value);
                }
            }
        }

        public BindingScope Scope { get; set; }

        public bool Matches(IRequest request)
        {
            return Specifications.All(spec => spec.Satisfied(request) != false);
        }

        public object Resolve(IRequest request)
        {
            switch (Scope)
            {
                case BindingScope.Singleton:
                    if (ServiceInstance == null)
                        ServiceInstance = Resolver.Resolve(request);
                    return ServiceInstance;

                case BindingScope.Thread:
                    if (ThreadStaticInstance == null)
                        ThreadStaticInstance = Resolver.Resolve(request);
                    return ThreadStaticInstance;

                default: // BindingScope.Normal
                    return Resolver.Resolve(request);
            }
        }
    }
}