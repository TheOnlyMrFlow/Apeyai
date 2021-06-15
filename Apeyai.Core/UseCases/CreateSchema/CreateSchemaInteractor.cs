using Apeyai.Core.Common.UseCases;
using Apeyai.Core.Ports.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apeyai.Core.UseCases.CreateSchema
{
    public class CreateSchemaInteractor
    {
        private readonly CreateSchemaRequest _request;

        private readonly ISchemaRepository _schemaRepository;
        private readonly UseCasePresenter<CreateSchemaResponse> _presenter;
        public CreateSchemaInteractor(CreateSchemaRequest request, ISchemaRepository schemaGateway, UseCasePresenter<CreateSchemaResponse> presenter)
        {
            _request = request;
            _schemaRepository = schemaGateway;
            _presenter = presenter;
        }

        public async Task<CreateSchemaResponse> Invoke()
        {
            var schemaId = await _schemaRepository.CreateEmptySchema(_request.SchemaName);

            var response = new CreateSchemaResponse()
            {
                SchemaId = schemaId
            };

            await _presenter.Present(response);

            return response;
        }
    }
}
