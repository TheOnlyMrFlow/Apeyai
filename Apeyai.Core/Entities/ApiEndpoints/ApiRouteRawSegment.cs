namespace Apeyai.Core.Entities.ApiEndpoints
{
    public class ApiRouteRawSegment
    {
        public string Value { get; set; }

        public ApiRouteDynamicSegment NextSegment { get; set; }
    }
}
