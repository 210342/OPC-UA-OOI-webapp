using System;
using System.IO;
using System.Runtime.Serialization;

namespace ReactiveHMI.M2MCommunication.Core.Exceptions
{
    public class ConfigurationFileNotFoundException : FileNotFoundException
    {
        public ConfigurationFileNotFoundException()
        {
        }

        public ConfigurationFileNotFoundException(string message) : base(message)
        {
        }

        public ConfigurationFileNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ConfigurationFileNotFoundException(string message, string fileName) : base(message, fileName)
        {
        }

        public ConfigurationFileNotFoundException(string message, string fileName, Exception innerException) : base(message, fileName, innerException)
        {
        }

        protected ConfigurationFileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
