using System;
using System.Threading.Tasks;
using Apeyai.Core.Entities;
using Apeyai.Core.Exceptions;
using Apeyai.Core.Infra.Persistence.Exceptions.RepositoryExceptions;
using Apeyai.Core.Infra.Persistence.Ports;

namespace Apeyai.Core.UseCases.AddTextAttributeToSchema
{
    public class AddTextAttributeToSchemaInteractor
    {
        private readonly AddTextAttributeToSchemaRequest _request;

        private readonly ISchemaRepository _schemaRepository;
        private readonly IAddTextAttributeToSchemaPresenter _presenter;
        public AddTextAttributeToSchemaInteractor(AddTextAttributeToSchemaRequest request, ISchemaRepository schemaRepository, IAddTextAttributeToSchemaPresenter presenter)
        {
            _request = request;
            _schemaRepository = schemaRepository;
            _presenter = presenter;
        }

        public async Task Invoke()
        {
            var textAttribute = new TextAttribute()
            {
                Name = _request.AttributeName,
                IsRequired = _request.IsRequired,
                MinLength = _request.MinLength,
                MaxLength = _request.Maxlength
            };

            var response = new AddTextAttributeToSchemaResponse();

            try
            {
                textAttribute.AssertValidity();
                await _schemaRepository.AddTextAttributeToSchema(_request.SchemaName, textAttribute);
            }
            catch (TextAttributesMinLengthHigherThanMaxLengthException)
            {
                _presenter.PresentMinLengthGreaterThanMaxLengthError();

                return;
            }
            catch (TextAttributesMinLengthLowerThanZeroException)
            {
                _presenter.PresentMinLengthLowerThanZeroError();

                return;
            }
            catch (TextAttributeNameIsNullOrWhitespacesException)
            {
                _presenter.PresentTextAttributeNameIsNullOrWhitespacesError();

                return;
            }
            catch (SchemaAlreadyExistsException)
            {
                _presenter.PresentSchemaAlreadyExistsError();

                return;
            }
            catch (AttributeAlreadyExistsException)
            {
                _presenter.PresentAttributeAlreadyExistsException();

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
