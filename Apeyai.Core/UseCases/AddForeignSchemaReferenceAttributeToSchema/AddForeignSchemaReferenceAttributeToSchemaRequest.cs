using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apeyai.Core.UseCases.AddForeignSchemaReferenceAttributeToSchema
{
    public class AddForeignSchemaReferenceAttributeToSchemaRequest
    {
        public string AttributeName { get; set; }

        public string SchemaName { get; set; }

        public string ForeignSchemaName { get; set; }

        public bool IsRequired { get; set; }
    }
}
