using System;

namespace PortableDI.Syntax
{
    /// <summary>
    /// Methods in this interface are defining which type and/or how this type actually should be resolved for a given binding.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBindingToSyntax<T>
    {
        /// <summary>
        /// Tells the resolver to resolve an object of type TImplementation. If you do not call InSingletonScope after this, the resolver is always creating a new instance of TImplementation.
        /// </summary>
        /// <typeparam name="TImplementation">The Type to resolve/create</typeparam>
        /// <returns></returns>
        IBindingInSyntax<T> To<TImplementation>() where TImplementation : T;

        /// <summary>
        /// Tells the resolver to resolve an object of type TImplementation which can only be resolved when resolved with the correct regstration name. If you do not call InSingletonScope after this, the resolver is always creating a new instance of TImplementation.
        /// </summary>
        /// <typeparam name="TImplementation">The Type to resolve/create</typeparam>
        /// <param name="registrationName"></param>
        /// <returns></returns>
        IBindingInSyntax<T> To<TImplementation>(string registrationName) where TImplementation : T;

        /// <summary>
        /// Tells the resolver to resolve an object of the given Type. If you do not call InSingletonScope after this, the resolver is always creating a new instance.
        /// </summary>
        /// <param name="type">The Type to resolve/create</param>
        /// <returns></returns>
        IBindingInSyntax<T> To(Type type);

        /// <summary>
        /// Tells the resolver to resolve a dependency by calling the given method.
        /// </summary>
        /// <param name="method">a method that resolves the requested type T.</param>
        /// <returns></returns>
        IBindingInSyntax<T> ToMethod(Func<IRequest, T> method);

        /// <summary>
        /// Tells the resolver to resolve a dependency by calling the given method.
        /// </summary>
        /// <param name="method">a method that resolves the requested type T.</param>
        /// <returns></returns>
        IBindingInSyntax<T> ToMethod(Func<T> method);

        /// <summary>
        /// Registers an already created instance.
        /// </summary>
        /// <param name="instance"></param>
        void ToInstance(T instance);
    }
}