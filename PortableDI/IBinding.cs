namespace PortableDI
{
    internal interface IBinding
    {
        BindingScope Scope { get; }

        bool Matches(IRequest request);

        object Resolve(IRequest request);
    }
}