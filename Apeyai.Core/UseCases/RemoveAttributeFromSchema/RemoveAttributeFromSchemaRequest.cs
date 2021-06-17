using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apeyai.Core.UseCases.RemoveAttributeFromSchema
{
    public class RemoveAttributeFromSchemaRequest
    {
        public string SchemaName { get; set; }

        public string AttributeName { get; set; }
    }
}
