using System;
using System.Text;

namespace PortableDI
{
    internal class Request : IRequest
    {
        public Request(Type requestedService, string registrationName = null)
        {
            RequestedName = registrationName;
            RequestedService = requestedService;
        }

        public Type RequestedService { get; private set; }

        public string RequestedName { get; private set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (RequestedService != null)
                sb.AppendFormat("Type {0} ", RequestedService.FullName);
            if (string.IsNullOrEmpty(RequestedName) == false)
                sb.AppendFormat(" as '{0}'", RequestedName);
            return sb.ToString();
        }
    }
}