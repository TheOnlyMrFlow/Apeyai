using System;
using System.Threading.Tasks;
using Apeyai.Core.Entities.Attributes;
using Apeyai.Core.Infra.Persistence.Exceptions.RepositoryExceptions;
using Apeyai.Core.Infra.Persistence.Ports;

namespace Apeyai.Core.UseCases.AddForeignSchemaReferenceAttributeToSchema
{
    public class AddForeignSchemaReferenceAttributeToSchemaInteractor
    {
        private readonly AddForeignSchemaReferenceAttributeToSchemaRequest _request;

        private readonly ISchemaRepository _schemaRepository;
        private readonly IAddForeignSchemaReferenceAttributeToSchemaPresenter _presenter;
        public AddForeignSchemaReferenceAttributeToSchemaInteractor
            (AddForeignSchemaReferenceAttributeToSchemaRequest request, ISchemaRepository schemaRepository, IAddForeignSchemaReferenceAttributeToSchemaPresenter presenter)
        {
            _request = request;
            _schemaRepository = schemaRepository;
            _presenter = presenter;
        }

        public async Task Invoke()
        {
            var response = new AddForeignSchemaReferenceAttributeToSchemaResponse();

            try
            {
                var schema = await _schemaRepository.GetSchemaByNameAsync(_request.ForeignSchemaName);

                if (schema == null)
                {
                    _presenter.PresentSchemaNotFoundError();

                    return;
                }

                if (await _schemaRepository.SchemaHasAttributeAsync(_request.SchemaName, _request.AttributeName))
                {
                    _presenter.PresentAttributeAlreadyExistsError();

                    return;
                }

                var foreignSchemaRefAttribute = new ForeignSchemaReferenceAttribute()
                {
                    Name = _request.AttributeName,
                    IsRequired = _request.IsRequired,
                    ForeignSchema = schema
                };

                foreignSchemaRefAttribute.Validate();

                

                await _schemaRepository.AddForeignSchemaReferenceAttributeToSchemaAsync(_request.SchemaName, foreignSchemaRefAttribute);
            }
            catch (AttributeAlreadyExistsException)
            {
                _presenter.PresentAttributeAlreadyExistsError();

                return;
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
