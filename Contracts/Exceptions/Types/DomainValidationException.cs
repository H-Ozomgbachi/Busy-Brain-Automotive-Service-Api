using System;
using System.Runtime.Serialization;

namespace Common.Contracts.Exceptions.Types
{
    [Serializable]
    public class DomainValidationException : CoreException
    {
        public DomainValidationException(string message) : base(message)
        {
        }

        public DomainValidationException(string message, string friendlyMessage = "") : base(message, friendlyMessage)
        {
        }

        public DomainValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public DomainValidationException(string message, System.Exception innerException, string friendlyMessage = "") : base(message, innerException, friendlyMessage)
        {
        }
    }
}
