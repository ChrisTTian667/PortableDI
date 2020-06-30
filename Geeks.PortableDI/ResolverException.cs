using System;

namespace Geeks.DependencyInjection
{
    /// <summary>
    /// Exception is thrown if DI container could not find a binding for the requested type
    /// or if the resolver process threw an error.
    /// </summary>
    public class ResolverException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ResolverException(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ResolverException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
