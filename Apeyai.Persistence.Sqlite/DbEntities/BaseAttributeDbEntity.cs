using System.ComponentModel.DataAnnotations.Schema;
using Apeyai.Core.Entities;
using Apeyai.Core.Entities.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Apeyai.Persistence.Sqlite.DbEntities
{
    [Table("Attribute")]
    public abstract class BaseAttributeDbEntity
    {
        public int Id { get; set; }

        public int SchemaId { get; set; }

        public SchemaDbEntity Schema { get; set; }

        public string Name { get; set; }

        public bool IsRequired { get; set; }

        public abstract BaseAttribute ToBusinessEntity();

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseAttributeDbEntity>()
                .HasIndex(x => x.Name);

            modelBuilder.Entity<BaseAttributeDbEntity>()
                .HasIndex(x => new {x.Name, x.SchemaId})
                .IsUnique(true)
                .HasDatabaseName("IX_UniqueAttributeNameAmongSchemaConstraint");

            modelBuilder.Entity<BaseAttributeDbEntity>()
                .HasDiscriminator<string>("Type")
                .HasValue<TextAttributeDbEntity>("Text")
                .HasValue<ForeignSchemaReferenceAttributeDbEntity>("Ref")
                .IsComplete(false);
        }
    }
}
