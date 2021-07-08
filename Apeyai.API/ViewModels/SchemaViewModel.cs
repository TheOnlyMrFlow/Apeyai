using System.Collections.Generic;
using System.Linq;
using Apeyai.Core.Entities;

namespace Apeyai.API.ViewModels
{
    public class SchemaViewModel
    {
        public string Name { get; set; }
        public IEnumerable<BaseAttributeViewModel> Attributes { get; set; }
        public static SchemaViewModel FromSchemaEntity(Schema schemaEntity)
        {
            IEnumerable<BaseAttributeViewModel> boolAttributes = schemaEntity.BooleanAttributes
                .Select(attr => attr.ToViewModel());

            var textAttributes = schemaEntity.TextAttributes
                .Select(attr => attr.ToViewModel());

            return new SchemaViewModel()
            {
                Name = schemaEntity.Name,
                Attributes = boolAttributes.Concat(textAttributes)
            };
        }
    }
}
