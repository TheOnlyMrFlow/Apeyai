using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apeyai.Core.Entities;

namespace Apeyai.Core.Infra.Persistence.Ports
{
    public interface ISchemaRepository
    {
        Task CreateEmptySchema(string schemaName);

        Task AddTextAttributeToSchema(string schemaName, TextAttribute textAttribute);

        Task<Schema> GetSchema(string schemaName);

        Task RemoveAttributeFromSchema(string schemaName, string attributeName);
    }
}
