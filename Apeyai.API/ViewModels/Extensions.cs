using Apeyai.Core.Entities;
using Apeyai.Core.Entities.Attributes;

namespace Apeyai.API.ViewModels
{
    public static class Extensions
    {
        public static SchemaViewModel ToViewModel(this Schema schemaEntity)
        {
            return SchemaViewModel.FromSchemaEntity(schemaEntity);
        }
        public static BooleanAttributeViewModel ToViewModel(this BooleanAttribute boolAttributeEntity)
        {
            return BooleanAttributeViewModel.FromEntity(boolAttributeEntity);
        }

        public static TextAttributeViewModel ToViewModel(this TextAttribute textAttributeEntity)
        {
            return TextAttributeViewModel.FromEntity(textAttributeEntity);
        }

        public static ForeignSchemaReferenceAttributeViewModel ToViewModel(this ForeignSchemaReferenceAttribute foreignSchemaRefAttributeEntity)
        {
            return ForeignSchemaReferenceAttributeViewModel.FromEntity(foreignSchemaRefAttributeEntity);
        }
    }
}
