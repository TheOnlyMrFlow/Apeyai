using System;
using System.Threading.Tasks;
using Apeyai.Core.Entities;
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

        public async Task Invoke()
        {
            var response = new CreateEmptySchemaResponse();

            try
            {
                await _schemaRepository.CreateEmptySchemaAsync(_request.SchemaName);
            }
            catch (EntityAlreadyExistsException)
            {
                _presenter.PresentSchemaAlreadyExistsError();

                return;
            }
            catch (Exception)
            {
                _presenter.PresentUnknownError();

                return;
            }

            _presenter.PresentSuccess(response);
        }
    }
}
