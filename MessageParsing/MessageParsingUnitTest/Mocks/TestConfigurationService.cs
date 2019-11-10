using M2MCommunication.Core;
using System.Collections.Generic;

namespace MessageParsingUnitTest.Mocks
{
    internal class TestConfigurationService : IConfiguration
    {
        public IEnumerable<string> GetDataTypeNames()
        {
            return new[] { "First type name", "Second type name" };
        }
    }
}
