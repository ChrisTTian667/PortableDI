using System;

namespace PortableDI.Specifications
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