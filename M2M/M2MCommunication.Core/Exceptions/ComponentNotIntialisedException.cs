using System;
using System.Runtime.Serialization;

namespace M2MCommunication.Core.Exceptions
{
    public class ComponentNotIntialisedException : Exception
    {
        public ComponentNotIntialisedException()
        {
        }

        public ComponentNotIntialisedException(string message) : base(message)
        {
        }

        public ComponentNotIntialisedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ComponentNotIntialisedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
