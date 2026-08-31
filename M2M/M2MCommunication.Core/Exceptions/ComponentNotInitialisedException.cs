using System;

namespace ReactiveHMI.M2MCommunication.Core.Exceptions
{
    public class ComponentNotInitialisedException : Exception
    {
        public ComponentNotInitialisedException()
        {
        }

        public ComponentNotInitialisedException(string message) : base(message)
        {
        }

        public ComponentNotInitialisedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
