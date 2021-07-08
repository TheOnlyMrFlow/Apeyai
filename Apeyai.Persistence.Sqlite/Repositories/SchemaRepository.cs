using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apeyai.Core.Entities;
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
        public async Task CreateEmptySchema(string schemaName)
        {
            if (_db.Schemas.Any(s => s.Name == schemaName))
                throw new EntityAlreadyExistsException();

            _db.Schemas.Add(new SchemaDbEntity() {Name = schemaName});
            await _db.SaveChangesAsync();
        }

        public async Task AddTextAttributeToSchema(string schemaName, TextAttribute textAttribute)
        {
            var schema = _db.Schemas.FirstOrDefault(s => s.Name == schemaName);

            if (schema == null)
                throw new SchemaNotFoundException();

            var alreadyExistingAttribute =
                _db.TextAttributes.FirstOrDefault(attr =>
                    attr.SchemaId == schema.Id && attr.Name == textAttribute.Name);

            if (alreadyExistingAttribute != null)
                throw new AttributeAlreadyExistsException();

            var txtAttrDbEntity = TextAttributeDbEntity.FromBusinessEntity(textAttribute);
            txtAttrDbEntity.SchemaId = schema.Id;
            _db.TextAttributes.Add(txtAttrDbEntity);

            await _db.SaveChangesAsync();
        }

        public Task<Schema> GetSchema(string schemaName)
        {
            var schema = _db.Schemas
                .Include(s => s.TextAttributes)
                .FirstOrDefault(s => s.Name == schemaName);

            if (schema == null)
                throw new SchemaNotFoundException();

            return Task.FromResult(schema.ToBusinessEntity());
        }

        public async Task RemoveAttributeFromSchema(string schemaName, string attributeName)
        {
            var schema = _db.Schemas
                .Include(s => s.TextAttributes)
                .FirstOrDefault(s => s.Name == schemaName);

            if (schema == null)
                throw new SchemaNotFoundException();

            var attributeToRemove = schema.TextAttributes.FirstOrDefault(ta => ta.Name == attributeName);
            
            if (attributeToRemove == null)
                throw new AttributeNotFoundException();

            schema.TextAttributes.Remove(attributeToRemove);

            await _db.SaveChangesAsync();
        }
    }
}
