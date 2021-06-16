using System;
using System.Threading.Tasks;
using Apeyai.Core.Infra.Persistence.Exceptions.RepositoryExceptions;
using Apeyai.Core.Infra.Persistence.Ports;

namespace Apeyai.Core.UseCases.Schemas.Create
{
    public class CreateSchemaInteractor
    {
        private readonly CreateSchemaRequest _request;

        private readonly ISchemaRepository _schemaRepository;
        private readonly CreateSchemaPresenter _presenter;
        public CreateSchemaInteractor(CreateSchemaRequest request, ISchemaRepository schemaGateway, CreateSchemaPresenter presenter)
        {
            _request = request;
            _schemaRepository = schemaGateway;
            _presenter = presenter;
        }

        public async Task<CreateSchemaResponse> Invoke()
        {
            var response = new CreateSchemaResponse();

            try
            {
                response.SchemaId = await _schemaRepository.CreateEmptySchema(_request.SchemaName);
            }
            catch (EntityAlreadyExistsException)
            {
                response.Error = CreateSchemaResponse.ECreateSchemaError.AlreadyExists;
            }
            catch (Exception)
            {
                response.Error = CreateSchemaResponse.ECreateSchemaError.Unknown;
            }

            await _presenter.Present(response);

            return response;
        }
    }
}
