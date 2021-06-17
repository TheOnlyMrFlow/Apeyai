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

        public async Task<RemoveAttributeFromSchemaResponse> Invoke()
        {
            var response = new RemoveAttributeFromSchemaResponse();

            try
            {
                await _schemaRepository.RemoveAttributeFromSchema("Toto", "User");
            } catch (SchemaNotFoundException)
            {
                response.Error = RemoveAttributeFromSchemaResponse.ERemoveAttributeFromSchemaError.SchemaNotFound;
            } catch (AttributeNotFoundException)
            {
                response.Error = RemoveAttributeFromSchemaResponse.ERemoveAttributeFromSchemaError.AttributeNotFound;
            } catch (Exception)
            {
                response.Error = RemoveAttributeFromSchemaResponse.ERemoveAttributeFromSchemaError.Unknown;
            }

            await _presenter.Present(response);

            return response;
        }
    }
}
