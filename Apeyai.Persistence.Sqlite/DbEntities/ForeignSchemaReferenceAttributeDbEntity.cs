using Apeyai.Core.Entities;
using Apeyai.Core.Entities.Attributes;

namespace Apeyai.Persistence.Sqlite.DbEntities
{
    public class ForeignSchemaReferenceAttributeDbEntity: BaseAttributeDbEntity
    {
        public string ForeignSchemaName{ get; set; }

        public static ForeignSchemaReferenceAttributeDbEntity FromBusinessEntity(ForeignSchemaReferenceAttribute businessEntity)
        {
            return new ForeignSchemaReferenceAttributeDbEntity()
            {
                IsRequired = businessEntity.IsRequired,
                Name = businessEntity.Name,
                ForeignSchemaName = businessEntity.ForeignSchema?.Name,
            };
        }

        public override ForeignSchemaReferenceAttribute ToBusinessEntity()
        {
            return new ForeignSchemaReferenceAttribute()
            {
                IsRequired = IsRequired,
                Name = Name,
                ForeignSchema = Schema.ToBusinessEntity()
            };
        }

        public ForeignSchemaReferenceAttribute ToBusinessEntity(Schema schema)
        {
            return new ForeignSchemaReferenceAttribute()
            {
                IsRequired = IsRequired,
                Name = Name,
                ForeignSchema = schema
            };
        }
    }
}
