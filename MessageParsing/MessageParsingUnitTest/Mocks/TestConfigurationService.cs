using M2MCommunication.Core;
using System.Collections.Generic;

namespace MessageParsingUnitTest.Mocks
{
    internal class TestConfigurationService : IConfiguration
    {
        public IEnumerable<UaTypeMetadata> GetTypeMetadata()
        {
            return new[]
            {
                new UaTypeMetadata("repo", "First type name"),
                new UaTypeMetadata("repo", "Second type name")
            };
        }
    }
}
