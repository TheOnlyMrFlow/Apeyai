using Apeyai.Core.Entities.Attributes;

namespace Apeyai.Core.Entities.ApiEndpoints
{
    public class ApiRouteDynamicSegment
    {
        public Schema SchemaReference { get; set; }

        public BaseAttribute AttributeReference { get; set; }

        public ApiRouteRawSegment NextSegment { get; set; }
    }
}
