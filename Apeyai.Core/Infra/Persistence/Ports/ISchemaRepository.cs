using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apeyai.Core.Infra.Persistence.Ports
{
    public interface ISchemaRepository
    {
        Task<int> CreateEmptySchema(string schemaName);
    }
}
