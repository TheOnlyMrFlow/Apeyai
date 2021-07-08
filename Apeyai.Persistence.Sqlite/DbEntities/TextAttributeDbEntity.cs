using Apeyai.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apeyai.Persistence.Sqlite.DbEntities
{
    public class TextAttributeDbEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsRequired { get; set; }

        public int MinLength { get; set; }

        public int MaxLength { get; set; }

        public int SchemaId { get; set; }

        public SchemaDbEntity Schema { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TextAttributeDbEntity>().HasIndex(x => x.Name);
        }

        public static TextAttributeDbEntity FromBusinessEntity(TextAttribute businessEntity)
        {
            return new TextAttributeDbEntity()
            {
                MinLength = businessEntity.MinLength,
                IsRequired = businessEntity.IsRequired,
                MaxLength = businessEntity.MaxLength,
                Name = businessEntity.Name
            };
        }

        public TextAttribute ToBusinessEntity()
        {
            return new TextAttribute()
            {
                IsRequired = IsRequired,
                MaxLength = MaxLength,
                MinLength = MinLength,
                Name = Name
            };
        }
    }
}
