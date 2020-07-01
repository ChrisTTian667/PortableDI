namespace PortableDI.Resolvers
{
    interface IRequestResolver
    {
        object Resolve(IRequest request);
    }
}