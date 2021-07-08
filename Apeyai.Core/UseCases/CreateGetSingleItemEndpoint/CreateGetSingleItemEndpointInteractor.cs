using System.Collections.Generic;
using System.Threading.Tasks;
using Apeyai.Core.Entities.ApiEndpoints.V2;
using Apeyai.Core.Infra.Persistence.Ports;

namespace Apeyai.Core.UseCases.CreateGetSingleItemEndpoint
{
    public class CreateGetSingleItemEndpointInteractor
    {
        private readonly CreateGetSingleItemEndpointRequest _request;
        private readonly ISchemaRepository _schemaRepository;
        private readonly IEndpointRepository _endpointResRepository;
        private readonly ICreateGetSingleItemEndpointPresenter _presenter;

        public CreateGetSingleItemEndpointInteractor(CreateGetSingleItemEndpointRequest request,
            ISchemaRepository schemaRepository, IEndpointRepository endpointResRepository,
            ICreateGetSingleItemEndpointPresenter presenter)
        {
            _request = request;
            _schemaRepository = schemaRepository;
            _endpointResRepository = endpointResRepository;
            _presenter = presenter;
        }

        public async Task Invoke()
        {
            var endpoint = BuildSingleItemEndpointNodeFromItemPath(new Stack<string>(_request.ItemPath));
        }

        private ItemCollectionEndpointNodeV2 BuildItemCollectionEndpointNodeFromItemPath(Stack<string> itemPath)
        {
            var node = new ItemCollectionEndpointNodeV2();
            node.CollectionName = itemPath.Pop();
            if (itemPath.Count > 0)
                node.Next = BuildSingleItemEndpointNodeFromItemPath(itemPath);

            return node;
        }

        private SingleItemEndpointNodeV2 BuildSingleItemEndpointNodeFromItemPath(Stack<string> itemPath)
        {
            var node = new SingleItemEndpointNodeV2();
            node.IdentifierAttributeName = itemPath.Pop();
            if (itemPath.Count > 0)
                node.Next = BuildItemCollectionEndpointNodeFromItemPath(itemPath);

            return node;
        }
    }
}
