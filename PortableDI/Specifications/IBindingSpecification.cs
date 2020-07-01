namespace PortableDI.Specifications
{
    internal interface IBindingSpecification
    {
        bool Satisfied(IRequest request);
    }
}