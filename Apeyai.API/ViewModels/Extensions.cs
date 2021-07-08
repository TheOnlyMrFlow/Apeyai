using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apeyai.Core.Entities;

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
    }
}
