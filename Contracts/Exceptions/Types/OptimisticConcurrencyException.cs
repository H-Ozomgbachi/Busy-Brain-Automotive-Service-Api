using System.Runtime.Serialization;

namespace Common.Contracts.Exceptions.Types
{
    public class OptimisticConcurrencyException : CoreException
    {
        public OptimisticConcurrencyException(int currentVersion, int providedVersion)
            : base($"Cannot update resource as the provided version {providedVersion} does not match the current version {currentVersion} on the server",
                  "Could not perform action as a more up to date version exists, please refresh your browser")
        {

        }

        public OptimisticConcurrencyException(string message) : base(message)
        {

        }
        public OptimisticConcurrencyException(string message, string friendlyMessage = "") : base(message, friendlyMessage)
        {
        }

        public OptimisticConcurrencyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public OptimisticConcurrencyException(string message, System.Exception innerException, string friendlyMessage = "") : base(message, innerException, friendlyMessage)
        {
        }
    }
}
