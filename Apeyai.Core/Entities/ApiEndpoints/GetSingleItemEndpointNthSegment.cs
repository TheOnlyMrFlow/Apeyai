using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apeyai.Core.Entities.Attributes;

namespace Apeyai.Core.Entities.ApiEndpoints
{
    class GetSingleItemEndpointNthSegment : GetSingleItemEndpointSegment
    {
        public ForeignSchemaReferenceAttribute ReferenceAttribute { get; set; }
        public override Schema Schema => ReferenceAttribute.ForeignSchema;
        public override string SegmentName => ReferenceAttribute.Name;
    }
}
