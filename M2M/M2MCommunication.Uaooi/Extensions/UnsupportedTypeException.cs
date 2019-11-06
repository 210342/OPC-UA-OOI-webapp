using System;
using System.Runtime.Serialization;

namespace M2MCommunication.Uaooi.Extensions
{
    public class UnsupportedTypeException : Exception
    {
        public UnsupportedTypeException() { }

        public UnsupportedTypeException(string message) : base(message)
        {
        }

        public UnsupportedTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnsupportedTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
