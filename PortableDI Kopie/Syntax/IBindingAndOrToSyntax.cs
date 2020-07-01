namespace Geeks.DependencyInjection.Syntax
{
    /// <summary>
    /// Combines IBindingAndSyntax and IBindingToSyntax
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBindingAndOrToSyntax<T> : IBindingAndSyntax<T>, IBindingToSyntax<T>
    {
    }
}