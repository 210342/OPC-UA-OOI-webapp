using System.Collections.Generic;

namespace M2MCommunication.Core
{
    public interface IConfiguration
    {
        /// <summary>
        /// Returns all type names in the configuration along with their repositories' names
        /// </summary>
        /// <returns> A tuple of repository name (string) and type name (string), respectively </returns>
        IEnumerable<UaTypeMetadata> GetTypeMetadata();
    }
}
