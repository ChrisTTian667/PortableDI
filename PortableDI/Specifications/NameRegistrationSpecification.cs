namespace PortableDI.Specifications
{
    internal class NameRegistrationSpecification : IBindingSpecification
    {
        private readonly string _registrationName;

        public NameRegistrationSpecification(string registrationName)
        {
            _registrationName = registrationName;
        }

        #region IBindingSpecification Members

        public bool Satisfied(IRequest request)
        {
            return (request.RequestedName == _registrationName);
        }

        #endregion IBindingSpecification Members
    }
}