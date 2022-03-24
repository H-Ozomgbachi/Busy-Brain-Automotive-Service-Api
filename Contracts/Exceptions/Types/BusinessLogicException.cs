using System.Runtime.Serialization;

namespace Common.Contracts.Exceptions.Types
{
    public class BusinessLogicException : CoreException
    {
        public BusinessLogicException(string message) : base(message)
        {

        }
        public BusinessLogicException(string message, string friendlyMessage = "") : base(message, friendlyMessage)
        {
        }

        public BusinessLogicException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public BusinessLogicException(string message, System.Exception innerException, string friendlyMessage = "") : base(message, innerException, friendlyMessage)
        {
        }
    }
}
