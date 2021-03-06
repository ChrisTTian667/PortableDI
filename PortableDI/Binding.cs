﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PortableDI.Resolvers;
using PortableDI.Specifications;

namespace PortableDI
{
    internal class Binding : IBinding
    {
        private static readonly Dictionary<int, object> _instances = new Dictionary<int, object>();

        public Binding(Type boundType)
        {
            Scope = BindingScope.Normal;
            Specifications = new List<IBindingSpecification>();
            // there is always a type specification !
            Specifications.Add(new TypeSpecification(boundType));
        }

        internal IRequestResolver Resolver { get; set; }

        internal List<IBindingSpecification> Specifications { get; }

        public object ServiceInstance { get; set; }

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
            return Specifications.All(spec => spec.Satisfied(request));
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