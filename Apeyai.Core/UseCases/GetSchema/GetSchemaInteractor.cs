using System;
using System.Threading.Tasks;
using Apeyai.Core.Infra.Persistence.Exceptions.RepositoryExceptions;
using Apeyai.Core.Infra.Persistence.Ports;

namespace Apeyai.Core.UseCases.GetSchema
{
    public class GetSchemaInteractor
    {
        private readonly GetSchemaRequest _request;

        private readonly ISchemaRepository _schemaRepository;
        private readonly IGetSchemaPresenter _presenter;
        public GetSchemaInteractor(GetSchemaRequest request, ISchemaRepository schemaGateway, IGetSchemaPresenter presenter)
        {
            _request = request;
            _schemaRepository = schemaGateway;
            _presenter = presenter;
        }

        public async Task Invoke()
        {
            var response = new GetSchemaResponse();

            try
            {
                response.Schema = await _schemaRepository.GetSchemaByNameAsync(_request.SchemaName);
            }
            catch (SchemaNotFoundException)
            {
                _presenter.PresentSchemaNotFoundError();

                return;
            }
            catch (Exception e)
            {
                _presenter.PresentUnknownError();

                return;
            }

            _presenter.PresentSuccess(response);
        }
    }
}
