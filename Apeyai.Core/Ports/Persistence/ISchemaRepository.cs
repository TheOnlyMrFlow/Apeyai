using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apeyai.Core.Ports.Persistence
{
    public interface ISchemaRepository
    {
        public Task<int> CreateEmptySchema(string schemaName);
    }
}
