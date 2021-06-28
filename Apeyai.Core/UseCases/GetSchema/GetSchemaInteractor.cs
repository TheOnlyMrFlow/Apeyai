using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apeyai.Core.Infra.Persistence.Exceptions.RepositoryExceptions;
using Apeyai.Core.Infra.Persistence.Ports;
using Apeyai.Core.UseCases.CreateEmptySchema;

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
                response.Schema = await _schemaRepository.GetSchema(_request.SchemaName);
            }
            catch (SchemaNotFoundException)
            {
                _presenter.PresentSchemaNotFoundError();

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
