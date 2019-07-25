using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReferenceWebApplication.Data
{
    internal class DummyService
    {
        internal async Task<string> ReceiveMessageAsync()
        {
            return await Task.FromResult(Guid.NewGuid().ToString());
        }
    }
}
