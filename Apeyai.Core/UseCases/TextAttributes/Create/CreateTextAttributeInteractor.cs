using System;
using System.Threading.Tasks;
using Apeyai.Core.Entities;
using Apeyai.Core.Exceptions;
using Apeyai.Core.Infra.Persistence.Exceptions.RepositoryExceptions;
using Apeyai.Core.Infra.Persistence.Ports;

namespace Apeyai.Core.UseCases.TextAttributes.Create
{
    public class CreateTextAttributeInteractor
    {
        private readonly CreateTextAttributeRequest _request;

        private readonly ITextAttributeRepository _textAttributeRepository;
        private readonly CreateTextAttributePresenter _presenter;
        public CreateTextAttributeInteractor(CreateTextAttributeRequest request, ITextAttributeRepository textAttributeGateway, CreateTextAttributePresenter presenter)
        {
            _request = request;
            _textAttributeRepository = textAttributeGateway;
            _presenter = presenter;
        }

        public async Task<CreateTextAttributeResponse> Invoke()
        {
            var textAttribute = new TextAttribute()
            {
                IsRequired = _request.IsRequired,
                MinLength = _request.MinLength,
                MaxLength = _request.Maxlength
            };

            var response = new CreateTextAttributeResponse();

            try
            {
                textAttribute.AssertValidity();
                response.AttributeId =
                    await _textAttributeRepository.CreateTextAttribute(_request.SchemaId, textAttribute);
            }
            catch (TextAttributesMinLengthHigherThanMaxLengthException)
            {
                response.Error = CreateTextAttributeResponse.ECreateTextAttributeError.MinLengthGreaterThanMaxLength;
            }
            catch (TextAttributesMinLengthLowerThanZeroException)
            {
                response.Error = CreateTextAttributeResponse.ECreateTextAttributeError.MinLengthLowerThanZero;
            }
            catch (EntityAlreadyExistsException)
            {
                response.Error = CreateTextAttributeResponse.ECreateTextAttributeError.AlreadyExists;
            }
            catch (Exception)
            {
                response.Error = CreateTextAttributeResponse.ECreateTextAttributeError.Unkown;
            }

            await _presenter.Present(response);

            return response;
        }
    }
}
