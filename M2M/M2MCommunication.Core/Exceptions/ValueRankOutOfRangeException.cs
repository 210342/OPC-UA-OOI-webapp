using System;

namespace ReactiveHMI.M2MCommunication.Core.Exceptions
{
    public class ValueRankOutOfRangeException : ArgumentOutOfRangeException
    {
        public ValueRankOutOfRangeException() { }

        public ValueRankOutOfRangeException(string message) : base(message)
        {
        }

        public ValueRankOutOfRangeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
