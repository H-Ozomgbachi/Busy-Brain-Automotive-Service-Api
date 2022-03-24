using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.Contracts.Exceptions.Types
{
    [Serializable]
    public class CoreException : Exception
    {
        public string FriendlyMessage { get; private set; }
        public IEnumerable<ValidationError> ValidationErrors { get; set; }

        public CoreException(string message) : base(message)
        {

        }
        public CoreException(string message, string friendlyMessage = "")
            : base(message)
        {
            FriendlyMessage = friendlyMessage;
        }
        public CoreException(string message, System.Exception innerException, string friendlyMessage = "")
            : base(message, innerException)
        {
            FriendlyMessage = friendlyMessage;
        }
        public CoreException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
