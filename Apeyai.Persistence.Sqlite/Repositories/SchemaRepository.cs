using System.Linq;
using System.Threading.Tasks;
using Apeyai.Core.Entities;
using Apeyai.Core.Entities.Attributes;
using Apeyai.Core.Infra.Persistence.Exceptions.RepositoryExceptions;
using Apeyai.Core.Infra.Persistence.Ports;
using Apeyai.Persistence.Sqlite.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Apeyai.Persistence.Sqlite.Repositories
{
    public class SchemaRepository : ISchemaRepository
    {
        private readonly SqliteContext _db;

        public SchemaRepository(SqliteContext db)
        {
            _db = db;
        }

        public async Task<bool> SchemaExistsAsync(string schemaName)
        {
            return (await _db.Schemas.FirstOrDefaultAsync(s => s.Name == schemaName)) != null;
        }

        public async Task<bool> SchemaHasAttributeAsync(string schemaName, string attributeName)
        {
            var schemaDbEntity = await GetSchemaDbEntityAsync(schemaName);

            return schemaDbEntity.Attributes.Any(attr => attr.Name == attributeName);
        }

        public async Task CreateEmptySchemaAsync(string schemaName)
        {
            if (_db.Schemas.Any(s => s.Name == schemaName))
                throw new EntityAlreadyExistsException();

            _db.Schemas.Add(new SchemaDbEntity() {Name = schemaName});
            await _db.SaveChangesAsync();
        }

        public async Task AddTextAttributeToSchemaAsync(string schemaName, TextAttribute textAttribute)
        {
            var schema = _db.Schemas.FirstOrDefault(s => s.Name == schemaName);

            if (schema == null)
                throw new SchemaNotFoundException();

            var alreadyExistingAttribute =
                _db.Attributes.FirstOrDefault(attr =>
                    attr.SchemaId == schema.Id && attr.Name == textAttribute.Name);

            if (alreadyExistingAttribute != null)
                throw new AttributeAlreadyExistsException();

            var txtAttrDbEntity = TextAttributeDbEntity.FromBusinessEntity(textAttribute);
            txtAttrDbEntity.SchemaId = schema.Id;
            _db.TextAttributes.Add(txtAttrDbEntity);

            await _db.SaveChangesAsync();
        }

        public async Task AddForeignSchemaReferenceAttributeToSchemaAsync(
            string schemaName,
            ForeignSchemaReferenceAttribute foreignSchemaRefAttribute)
        {
            var schema = _db.Schemas.FirstOrDefault(s => s.Name == schemaName);

            if (schema == null)
                throw new SchemaNotFoundException();

            var alreadyExistingAttribute =
                _db.Attributes.FirstOrDefault(attr =>
                    attr.SchemaId == schema.Id && attr.Name == foreignSchemaRefAttribute.Name);

            if (alreadyExistingAttribute != null)
                throw new AttributeAlreadyExistsException();

            var foreignSchemaRefAttrDbEntity = ForeignSchemaReferenceAttributeDbEntity.FromBusinessEntity(foreignSchemaRefAttribute);
            foreignSchemaRefAttrDbEntity.SchemaId = schema.Id;
            _db.ForeignSchemaReferenceAttributes.Add(foreignSchemaRefAttrDbEntity);

            await _db.SaveChangesAsync();
        }

        public async Task<Schema> GetSchemaByNameAsync(string schemaName)
        {
            var schemaDbEntity = await GetSchemaDbEntityAsync(schemaName);

            return schemaDbEntity.ToBusinessEntity();
        }

        public async Task<SchemaDbEntity> GetSchemaDbEntityAsync(string schemaName)
        {
            var schemaDbEntity = await _db.Schemas
                .Include(s => s.Attributes)
                .FirstOrDefaultAsync(s => s.Name == schemaName);

            if (schemaDbEntity == null)
                throw new SchemaNotFoundException();

            return schemaDbEntity;
        }

        public async Task RemoveAttributeFromSchemaAsync(string schemaName, string attributeName)
        {
            var schemaDbEntity = await _db.Schemas
                .Include(s => s.Attributes)
                .FirstOrDefaultAsync(s => s.Name == schemaName);

            if (schemaDbEntity == null)
                throw new SchemaNotFoundException();

            var attributeDbEntityToRemove = schemaDbEntity.Attributes.FirstOrDefault(ta => ta.Name == attributeName);
            
            if (attributeDbEntityToRemove == null)
                throw new AttributeNotFoundException();

            schemaDbEntity.Attributes.Remove(attributeDbEntityToRemove);

            await _db.SaveChangesAsync();
        }

        public Task<bool> SchemaHasAttributeAsync<T>(string schemaName, string attributeName) where T : BaseAttribute
        {
            throw new System.NotImplementedException();
        }

        public async Task<BaseAttribute> GetSchemaAttributeByNameAsync(string schemaName, string attributeName)
        {
            var schemaDbEntity = await GetSchemaDbEntityAsync(schemaName);

            if (schemaDbEntity is null) return null;

            var attributeDbEntity = schemaDbEntity
                .Attributes
                .FirstOrDefault(attr => attr.Name == attributeName);

            return attributeDbEntity.ToBusinessEntity();
        }

        public async Task<Schema> GetSchemaByCollectionNameAsync(string schemaCollectionName)
        {
            var schemaDbEntity = await _db.Schemas
                .FirstOrDefaultAsync(s => s.CollectionName == schemaCollectionName);
            
            return schemaDbEntity.ToBusinessEntity();
        }
    }
}
