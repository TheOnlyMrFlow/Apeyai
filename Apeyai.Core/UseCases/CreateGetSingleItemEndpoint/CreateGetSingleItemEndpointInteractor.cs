using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apeyai.Core.Infra.Persistence.Ports;

namespace Apeyai.Core.UseCases.CreateGetSingleItemEndpoint
{
    public class CreateGetSingleItemEndpointInteractor
    {
        private readonly CreateGetSingleItemEndpointRequest _request;
        private readonly ISchemaRepository _schemaRepository;
        private readonly IEndpointRepository _endpointResRepository;
        private readonly ICreateGetSingleItemEndpointPresenter _presenter;

        public CreateGetSingleItemEndpointInteractor(CreateGetSingleItemEndpointRequest request, ISchemaRepository schemaRepository, IEndpointRepository endpointResRepository, ICreateGetSingleItemEndpointPresenter presenter)
        {
            _request = request;
            _schemaRepository = schemaRepository;
            _endpointResRepository = endpointResRepository;
            _presenter = presenter;
        }
        public async Task Invoke()
        {
            
        }
    }
}
