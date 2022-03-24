using System.Runtime.Serialization;

namespace Common.Contracts.Exceptions.Types
{
    public class ResourceDeletedException : CoreException
    {
        public ResourceDeletedException(string message) : base(message)
        {

        }
        public ResourceDeletedException(string message, string friendlyMessage = "") : base(message, friendlyMessage)
        {
        }

        public ResourceDeletedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ResourceDeletedException(string message, System.Exception innerException, string friendlyMessage = "") : base(message, innerException, friendlyMessage)
        {
        }
    }
}
