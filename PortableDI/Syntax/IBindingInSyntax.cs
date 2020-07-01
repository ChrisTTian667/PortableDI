namespace PortableDI.Syntax
{
    /// <summary>
    ///     Methods that describe the binding scope of a resolved instance
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBindingInSyntax<T>
    {
        /// <summary>
        ///     If called in the binding process, the resolved instance will be a singleton.
        /// </summary>
        void InSingletonScope();

        /// <summary>
        ///     If called in the binding process, thre resolved instance will be thread static.
        /// </summary>
        void InThreadScope();
    }
}