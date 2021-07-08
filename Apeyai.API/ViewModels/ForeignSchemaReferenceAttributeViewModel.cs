using Apeyai.Core.Entities;
using Apeyai.Core.Entities.Attributes;

namespace Apeyai.API.ViewModels
{
    public class ForeignSchemaReferenceAttributeViewModel: BaseAttributeViewModel
    {
        public override EAttributeType AttributeType => EAttributeType.Ref;
        public string ForeignSchemaName { get; set; }

        public static ForeignSchemaReferenceAttributeViewModel FromEntity(ForeignSchemaReferenceAttribute foreignSchemaRefAttributeEntity)
        {
            return new ForeignSchemaReferenceAttributeViewModel()
            {
                IsRequired = foreignSchemaRefAttributeEntity.IsRequired,
                Name = foreignSchemaRefAttributeEntity.Name,
                ForeignSchemaName = foreignSchemaRefAttributeEntity.ForeignSchema.Name
            };
        }

    }
}
