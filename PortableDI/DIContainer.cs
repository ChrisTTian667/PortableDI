using System;
using System.Collections.Generic;
using PortableDI.Syntax;

namespace PortableDI
{
    /// <summary>
    ///     The static DIContainer methods are used for binding and resolving of dependencies.
    /// </summary>
    public sealed class DIContainer
    {
        private static readonly StandardDIContainer _container;

        static DIContainer()
        {
            _container = new StandardDIContainer();
        }

        /// <summary>
        ///     Clears the complete list of bindings.
        /// </summary>
        public static void Flush()
        {
            _container.FlushBindings();
        }

        #region Resolver methods

        /// <summary>
        ///     Resolves a dependency by the given Type.
        /// </summary>
        /// <param name="service">The given Type you want to resolve</param>
        /// <returns>The resolved dependency.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static object Resolve(Type service)
        {
            return _container.Resolve(service);
        }

        /// <summary>
        ///     Resolves a dependency of type T.
        /// </summary>
        /// <typeparam name="T">The Type to resolve.</typeparam>
        /// <returns>The resolved instance of type T</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        /// <summary>
        ///     Resolves a dependency of type T which was bound with a registration name.
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <param name="registrationName">The registration name that was used to bind the specified type.</param>
        /// <returns>The resolved instance of type T</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static T Resolve<T>(string registrationName)
        {
            return _container.Resolve<T>(registrationName);
        }


        /// <summary>
        ///     Resolves a list previously bound dependencies of type T.
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <returns>A List of all bound instances of the given type T.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static IEnumerable<T> ResolveAll<T>()
        {
            return _container.ResolveAll<T>();
        }

        #endregion

        #region Query methods

        /// <summary>
        ///     Returns wether or not a given type T is already bound or not.
        /// </summary>
        /// <typeparam name="T">The type to query for.</typeparam>
        /// <returns>true if type T is already bound, false if not.</returns>
        public static bool IsBound<T>()
        {
            return _container.IsBound<T>();
        }

        /// <summary>
        ///     Returns wether or not a given Type is already bound or not.
        /// </summary>
        /// <param name="requestedService">The Type to query for.</param>
        /// <returns>true if requestedService is already bound, false if not.</returns>
        public static bool IsBound(Type requestedService)
        {
            return _container.IsBound(requestedService);
        }

        #endregion

        #region Binding methods

        /// <summary>
        ///     Registers a type to be resolveable and returns a fluent syntax to finish the binding process.
        /// </summary>
        /// <param name="service">The type to register as a resolvable dependency.</param>
        /// <returns>The fluent syntax to finish the binding process if the given type is of type interface or abstract.</returns>
        public static IBindingAndOrToSyntax<object> Bind(Type service)
        {
            return _container.Bind(service);
        }

        /// <summary>
        ///     Registers a type to be resolveable and returns a fluent syntax to finish the binding process.
        /// </summary>
        /// <typeparam name="T">The type to register as a resolvable dependency.</typeparam>
        /// <returns>The fluent syntax to finish the binding process if the given type is of type interface or abstract.</returns>
        public static IBindingAndOrToSyntax<T> Bind<T>()
        {
            return _container.Bind<T>();
        }

        /// <summary>
        ///     Removes a previously bound type from the list of resolveable dependencies.
        /// </summary>
        /// <param name="service">The Type to unregister.</param>
        /// <returns>True if the type was bound, false if there was no dependency registered for this type.</returns>
        public static bool Unbind(Type service)
        {
            return _container.Unbind(service);
        }

        /// <summary>
        ///     Calls package.Load with the integrated IDIContainer instance
        /// </summary>
        /// <param name="package"></param>
        public static void BindPackage(IDIPackage package)
        {
            package.Load(_container);
        }

        #endregion
    }
}