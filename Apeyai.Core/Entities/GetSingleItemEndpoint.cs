using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apeyai.Core.Entities
{
    public class GetSingleItemEndpoint
    {
        public string Route { get; set; }
        public Schema Schema { get; set; }

        public object IdentifierAttribute { get; set; }
    }
}
