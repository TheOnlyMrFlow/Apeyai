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

        public async Task<AddTextAttributeToSchemaResponse> Invoke()
        {
            var textAttribute = new TextAttribute()
            {
                IsRequired = _request.IsRequired,
                MinLength = _request.MinLength,
                MaxLength = _request.Maxlength
            };

            var response = new AddTextAttributeToSchemaResponse();

            try
            {
                textAttribute.AssertValidity();
                response.AttributeId =
                    await _schemaRepository.AddTextAttributeToSchema(_request.SchemaId, textAttribute);
            }
            catch (TextAttributesMinLengthHigherThanMaxLengthException)
            {
                response.Error = AddTextAttributeToSchemaResponse.ECreateTextAttributeError.MinLengthGreaterThanMaxLength;
            }
            catch (TextAttributesMinLengthLowerThanZeroException)
            {
                response.Error = AddTextAttributeToSchemaResponse.ECreateTextAttributeError.MinLengthLowerThanZero;
            }
            catch (EntityAlreadyExistsException)
            {
                response.Error = AddTextAttributeToSchemaResponse.ECreateTextAttributeError.AlreadyExists;
            }
            catch (Exception)
            {
                response.Error = AddTextAttributeToSchemaResponse.ECreateTextAttributeError.Unknown;
            }

            await _presenter.Present(response);

            return response;
        }
    }
}
