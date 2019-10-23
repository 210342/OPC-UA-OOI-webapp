using System;
using System.Collections.Generic;
using System.Text;

namespace MessageParsing.UaooiExtensions
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
    }
}
