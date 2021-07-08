using System.Collections.Generic;
using System.Linq;
using Apeyai.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apeyai.Persistence.Sqlite.DbEntities
{
    public class SchemaDbEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TextAttributeDbEntity> TextAttributes { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SchemaDbEntity>().HasIndex(x => x.Name);
        }

        public Schema ToBusinessEntity()
        {
            return new Schema()
            {
                Name = Name,
                TextAttributes = TextAttributes.Select(ta => ta.ToBusinessEntity()).ToList(),
                BooleanAttributes = new List<BooleanAttribute>(),
            };
        }
    }
}
