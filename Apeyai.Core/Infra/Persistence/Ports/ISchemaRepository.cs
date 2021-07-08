using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apeyai.Core.Entities;
using Apeyai.Core.Entities.Attributes;

namespace Apeyai.Core.Infra.Persistence.Ports
{
    public interface ISchemaRepository
    {
        Task<bool> SchemaExistsAsync(string schemaName);

        Task<bool> SchemaHasAttributeAsync(string schemaName, string attributeName);

        Task<bool> SchemaHasAttributeAsync<T>(string schemaName, string attributeName) where T : BaseAttribute;

        Task<BaseAttribute> GetSchemaAttributeByNameAsync(string schemaName, string attributeName);

        Task CreateEmptySchemaAsync(string schemaName);

        Task AddTextAttributeToSchemaAsync(string schemaName, TextAttribute textAttribute);

        Task AddForeignSchemaReferenceAttributeToSchemaAsync(string requestSchemaName, ForeignSchemaReferenceAttribute foreignSchemaRefAttribute);

        Task<Schema> GetSchemaByNameAsync(string schemaName);

        Task<Schema> GetSchemaByCollectionNameAsync(string schemaCollectionName);

        Task RemoveAttributeFromSchemaAsync(string schemaName, string attributeName);
    }
}
