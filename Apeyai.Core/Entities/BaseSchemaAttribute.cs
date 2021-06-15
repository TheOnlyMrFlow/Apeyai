using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apeyai.Core.Entities
{
    public abstract class BaseSchemaAttribute: ISchemaAttribute
    {
        public int Id { get; }
        public bool IsRequired { get; set; }
    }
}
