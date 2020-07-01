using System;

namespace PortableDI
{
    /// <summary>
    ///     A Request stores all the information that is needed to resolve a dependency at runtime.
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        ///     When a binding was done with a registration name, this name is stored here
        /// </summary>
        string RequestedName { get; }

        /// <summary>
        ///     The type that should be resolved
        /// </summary>
        Type RequestedService { get; }
    }
}