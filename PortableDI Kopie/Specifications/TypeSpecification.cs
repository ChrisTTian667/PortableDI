using System;

namespace Geeks.DependencyInjection.Specifications
{
    internal class TypeSpecification : IBindingSpecification
    {
        public TypeSpecification(Type service)
        {
            Service = service;
        }

        public Type Service { get; private set; }

        public bool Satisfied(IRequest request)
        {
            return (Service == request.RequestedService);
        }
    }
}