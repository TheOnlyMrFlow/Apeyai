using System.Runtime.InteropServices;
using Apeyai.Core.Entities.Attributes;

namespace Apeyai.Core.Entities.ApiEndpoints
{
    public class GetSingleItemEndpointRootSegment : GetSingleItemEndpointSegment
    {

        private Schema _schema;
        public override Schema Schema => _schema;

        // see https://stackoverflow.com/questions/82437/why-is-it-impossible-to-override-a-getter-only-property-and-add-a-setter
        public void SetSchema(Schema schema)
            => _schema = schema;
        public override string SegmentName => Schema.CollectionName;
    }
}