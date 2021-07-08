using System.Collections.Generic;
using System.Linq;
using Apeyai.Core.Common;
using Apeyai.Core.Entities.Attributes;
using Apeyai.Core.Exceptions;

namespace Apeyai.Core.Entities
{
    public class Schema
    {
        public string Name { get; set; }

        public string CollectionName { get; set; }
        
        public List<TextAttribute> TextAttributes { get; set; }

        public List<BooleanAttribute> BooleanAttributes { get; set; }

        public List<ForeignSchemaReferenceAttribute> ForeignSchemaReferenceAttributes { get; set; }

        public IEnumerable<BaseAttribute> AllAttributes =>
            TextAttributes.Cast<BaseAttribute>().Concat(BooleanAttributes).Concat(ForeignSchemaReferenceAttributes);

        public void AssertValidity()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new SchemaNameIsNullOrWhitespacesException();
        }
    }
}
