using Apeyai.Core.Entities.Attributes;

namespace Apeyai.Persistence.Sqlite.DbEntities
{
    public class TextAttributeDbEntity: BaseAttributeDbEntity
    {
        public int MinLength { get; set; }

        public int MaxLength { get; set; }

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

        public override TextAttribute ToBusinessEntity()
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
