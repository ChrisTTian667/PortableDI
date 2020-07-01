using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PortableDI.Resolvers
{
    internal class StandardResolver : IRequestResolver
    {
        private readonly IDIContainer _container;
        private readonly Type _implementationType;

        public StandardResolver(Type implementationType, IDIContainer container)
        {
            _implementationType = implementationType;
            _container = container;
        }

        #region IRequestResolver Members

        public object Resolve(IRequest request)
        {
            // this is automatic constructor injection!
            var ctor = FindConstructorWithFewestParameters();
            var parameters = ResolveConstructorParameters(ctor);

            return ctor.Invoke(parameters); // Activator.CreateInstance(_implementationType, parameters);
        }

        private object[] ResolveConstructorParameters(ConstructorInfo ctor)
        {
            var result = new List<object>();

            var parameters = ctor.GetParameters();
            if (!parameters.Any())
                return null;
            try
            {
                foreach (var param in parameters)
                    if (param.DefaultValue != null && _container.IsBound(param.ParameterType) == false)
                        result.Add(param.DefaultValue);
                    else
                        result.Add(_container.Resolve(param.ParameterType));
            }
            catch (Exception)
            {
                throw new ResolverException(Resources.Exception_UnresolvedCtorParams);
            }

            return result.ToArray();
        }

        private ConstructorInfo FindConstructorWithFewestParameters()
        {
            var ctors = _implementationType.GetConstructors();
            var smallestCount = 1000; // the potential that a ctor has more than 1000 parameters is very small ;-)
            ConstructorInfo smallest = null;
            foreach (var ctor in ctors)
            {
                var currentCount = ctor.GetParameters().Count();
                if (smallestCount <= currentCount)
                    continue;

                smallestCount = currentCount;
                smallest = ctor;
            }

            var result = smallest ?? _implementationType.GetConstructors(BindingFlags.NonPublic).FirstOrDefault();

            if (result == null)
                throw new ResolverException(Resources.Exception_NoValidCtor);

            return result;
        }

        #endregion IRequestResolver Members
    }
}