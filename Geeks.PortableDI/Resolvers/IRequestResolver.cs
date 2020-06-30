namespace Geeks.DependencyInjection.Resolvers
{
    interface IRequestResolver
    {
        object Resolve(IRequest request);
    }
}