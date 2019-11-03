using System.Collections.Generic;

namespace M2MCommunication.Core
{
    public interface IConfiguration
    {
        IEnumerable<string> GetDataTypeNames();
    }
}
