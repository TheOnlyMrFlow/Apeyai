using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Apeyai.Core.Entities;
using Apeyai.Core.Entities.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Apeyai.Persistence.Sqlite.DbEntities
{
    [Table("Schema")]
    public class SchemaDbEntity
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string CollectionName { get; set; }

        public ICollection<BaseAttributeDbEntity> Attributes { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SchemaDbEntity>().HasIndex(x => x.Name);
        }

        public Schema ToBusinessEntity()
        {
            var schemaEntity = new Schema() { Name = Name };

            schemaEntity.TextAttributes = Attributes
                    .Where(attr => attr is TextAttributeDbEntity)
                    .Select(attr => ((TextAttributeDbEntity)attr).ToBusinessEntity())
                    .ToList();

            schemaEntity.ForeignSchemaReferenceAttributes = Attributes
                    .Where(attr => attr is ForeignSchemaReferenceAttributeDbEntity)
                    .Select(attr => ((ForeignSchemaReferenceAttributeDbEntity)attr).ToBusinessEntity(schemaEntity))
                    .ToList();

            schemaEntity.BooleanAttributes = new List<BooleanAttribute>();

            return schemaEntity;
        }
    }
}
