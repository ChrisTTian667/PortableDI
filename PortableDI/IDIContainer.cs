using System;
using System.Collections.Generic;
using PortableDI.Syntax;

namespace PortableDI
{
    /// <summary>
    /// The DIContainer interface 
    /// </summary>
    public interface IDIContainer
    {
        /// <summary>
        /// Registers a type to be resolveable and returns a fluent syntax to finish the binding process.
        /// </summary>
        /// <typeparam name="T">The type to register as a resolvable dependency.</typeparam>
        /// <returns>The fluent syntax to finish the binding process if the given type is of type interface or abstract.</returns>
        IBindingAndOrToSyntax<T> Bind<T>();

        /// <summary>
        /// Registers a type to be resolveable and returns a fluent syntax to finish the binding process.
        /// </summary>
        /// <param name="service">The type to register as a resolvable dependency.</param>
        /// <returns>The fluent syntax to finish the binding process if the given type is of type interface or abstract.</returns>
        IBindingAndOrToSyntax<object> Bind(Type service);

        /// <summary>
        /// Removes a previously bound type from the list of resolveable dependencies.
        /// </summary>
        /// <param name="service">The Type to unregister.</param>
        /// <returns>True if the type was bound, false if there was no dependency registered for this type.</returns>
        bool Unbind(Type service);

        /// <summary>
        /// Resolves a dependency of the given type.
        /// </summary>
        /// <returns>The resolved instance of the given type</returns>
        /// <exception cref="ResolverException"></exception>
        object Resolve(Type service);

        /// <summary>
        /// Resolves a dependency of type T.
        /// </summary>
        /// <typeparam name="T">The Type to resolve.</typeparam>
        /// <returns>The resolved instance of type T</returns>
        /// <exception cref="ResolverException"></exception>
        T Resolve<T>();

        /// <summary>
        /// Resolves a dependency of type T which was bound with a registration name.
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <param name="registrationName">The registration name that was used to bind the specified type.</param>
        /// <returns>The resolved instance of type T</returns>
        /// <exception cref="ResolverException"></exception>
        T Resolve<T>(string registrationName);

        /// <summary>
        /// Resolves a list previously bound dependencies of type T.
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <returns>A List of all bound instances of the given type T.</returns>
        /// <exception cref="ResolverException"></exception>
        IEnumerable<T> ResolveAll<T>();

        /// <summary>
        /// Returns wether or not a given type T is already bound or not.
        /// </summary>
        /// <typeparam name="T">The type to query for.</typeparam>
        /// <returns>true if type T is already bound, false if not.</returns>
        bool IsBound<T>();

        /// <summary>
        /// Returns wether or not a given Type is already bound or not.
        /// </summary>
        /// <param name="requestedService">The Type to query for.</param>
        /// <returns>true if requestedService is already bound, false if not.</returns>
        bool IsBound(Type requestedService);
    }
}