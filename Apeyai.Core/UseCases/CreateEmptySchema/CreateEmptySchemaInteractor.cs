using System;
using System.Threading.Tasks;
using Apeyai.Core.Infra.Persistence.Exceptions.RepositoryExceptions;
using Apeyai.Core.Infra.Persistence.Ports;

namespace Apeyai.Core.UseCases.CreateEmptySchema
{
    public class CreateEmptySchemaInteractor
    {
        private readonly CreateEmptySchemaRequest _request;

        private readonly ISchemaRepository _schemaRepository;
        private readonly ICreateEmptySchemaPresenter _presenter;
        public CreateEmptySchemaInteractor(CreateEmptySchemaRequest request, ISchemaRepository schemaGateway, ICreateEmptySchemaPresenter presenter)
        {
            _request = request;
            _schemaRepository = schemaGateway;
            _presenter = presenter;
        }

        public async Task<CreateEmptySchemaResponse> Invoke()
        {
            var response = new CreateEmptySchemaResponse();

            try
            {
                await _schemaRepository.CreateEmptySchema(_request.SchemaName);
            }
            catch (EntityAlreadyExistsException)
            {
                response.Error = CreateEmptySchemaResponse.ECreateSchemaError.AlreadyExists;
            }
            catch (Exception)
            {
                response.Error = CreateEmptySchemaResponse.ECreateSchemaError.Unknown;
            }

            await _presenter.Present(response);

            return response;
        }
    }
}
