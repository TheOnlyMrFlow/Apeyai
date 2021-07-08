namespace Apeyai.Core.Entities.ApiEndpoints.V2
{
    public class SingleItemEndpointNodeV2
    {
        public ItemCollectionEndpointNodeV2 Next { get; set; }

        public string IdentifierAttributeName { get; set; }
    }

    public class ItemCollectionEndpointNodeV2
    {
        public SingleItemEndpointNodeV2 Next { get; set; }

        public string CollectionName { get; set; }
    }
}
