using System;
using System.Collections.Generic;
using System.Text;

namespace M2MCommunication.Uaooi.Extensions
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
