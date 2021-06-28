using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apeyai.Core.Infra.Persistence.Exceptions.RepositoryExceptions;
using Apeyai.Core.Infra.Persistence.Ports;

namespace Apeyai.Core.UseCases.RemoveAttributeFromSchema
{
    public class RemoveAttributeFromSchemaInteractor
    {
        private readonly RemoveAttributeFromSchemaRequest _request;
        private readonly ISchemaRepository _schemaRepository;
        private readonly IRemoveAttributeFromSchemaPresenter _presenter;

        public RemoveAttributeFromSchemaInteractor(RemoveAttributeFromSchemaRequest request, ISchemaRepository schemaGateway, IRemoveAttributeFromSchemaPresenter presenter)
        {
            _request = request;
            _schemaRepository = schemaGateway;
            _presenter = presenter;
        }

        public async Task Invoke()
        {
            var response = new RemoveAttributeFromSchemaResponse();

            try
            { 
                await _schemaRepository.RemoveAttributeFromSchema(_request.SchemaName, _request.AttributeName);
            } catch (SchemaNotFoundException)
            {
                _presenter.PresentSchemaNotFoundError();

                return;
            } catch (AttributeNotFoundException)
            {
                _presenter.PresentAttributeNotFoundError();

                return;
            } catch (Exception)
            {
                _presenter.PresentUnknownError();

                return;
            }

            _presenter.PresentSuccess(response);
        }
    }
}
