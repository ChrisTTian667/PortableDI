namespace PortableDI.Resolvers
{
    internal interface IRequestResolver
    {
        object Resolve(IRequest request);
    }
}