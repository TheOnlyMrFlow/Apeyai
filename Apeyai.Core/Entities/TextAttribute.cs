using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apeyai.Core.Entities
{
    public class TextAttribute: BaseSchemaAttribute
    {
        public int MinLength { get; set; }

        public int MaxLength { get; set; }
    }
}
