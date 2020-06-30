using Geeks.DependencyInjection.Properties;
using System;

namespace Geeks.DependencyInjection.Resolvers
{
    internal class MethodCallResolver<T> : IRequestResolver
    {
        private readonly Func<IRequest, T> _callbackWithRequest;
        private readonly Func<T> _callbackWithoutRequest;

        public MethodCallResolver(Func<IRequest, T> callback)
        {
            _callbackWithRequest = callback;
        }

        public MethodCallResolver(Func<T> callback)
        {
            _callbackWithoutRequest = callback;
        }

        public object Resolve(IRequest request)
        {
            try
            {
                var result = _callbackWithRequest != null
                    ? _callbackWithRequest(request)
                    : _callbackWithoutRequest();
                
                return result;
            }
            catch(Exception ex)
            {
                throw new ResolverException(Resources.Exception_BoundMethodException, ex);
            }
        }
    }
}