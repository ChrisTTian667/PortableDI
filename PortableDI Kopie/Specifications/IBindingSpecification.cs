namespace Geeks.DependencyInjection.Specifications
{
    internal interface IBindingSpecification
    {
        bool Satisfied(IRequest request);
    }
}