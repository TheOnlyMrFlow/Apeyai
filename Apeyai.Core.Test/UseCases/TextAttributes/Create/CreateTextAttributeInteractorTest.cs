using System.Threading.Tasks;
using Apeyai.Core.Entities;
using Apeyai.Core.Infra.Persistence.Exceptions.RepositoryExceptions;
using Apeyai.Core.Infra.Persistence.Ports;
using Apeyai.Core.UseCases.TextAttributes.Create;
using FluentAssertions;
using Moq;
using Xunit;
using static Apeyai.Core.UseCases.Schemas.Create.CreateSchemaResponse;

namespace Apeyai.Core.Test.UseCases.TextAttributes.Create
{
    public class CreateTextAttributeInteractorTest
    {
        public Mock<ITextAttributeRepository> _textAttributeRepositoryMock;
        public Mock<CreateTextAttributePresenter> _createTextAttributePresenterMock;

        public CreateTextAttributeInteractorTest()
        {
            _textAttributeRepositoryMock = new Mock<ITextAttributeRepository>();
            _createTextAttributePresenterMock = new Mock<CreateTextAttributePresenter>();
        }

        [Fact]
        public async void response_text_attribute_id_should_be_the_id_returned_by_repository()
        {
            _textAttributeRepositoryMock.Setup(repo => repo.CreateTextAttribute(It.IsAny<int>(), It.IsAny<TextAttribute>())).Returns(Task.FromResult(9));

            var createTextAttributeRequest = new CreateTextAttributeRequest() { SchemaId = 12, MinLength = 1, Maxlength = 15, IsRequired = true, AttributeName = "UserId"};
            var interactor = new CreateTextAttributeInteractor(createTextAttributeRequest, _textAttributeRepositoryMock.Object, _createTextAttributePresenterMock.Object);

            var response = await interactor.Invoke();

            response.AttributeId.Should().Be(9);
        }

        [Fact]
        public async void response_error_should_be_already_exists_if_repository_throws_entity_already_exists_exception()
        {
            _textAttributeRepositoryMock.Setup(repo => repo.CreateTextAttribute(It.IsAny<int>(), It.IsAny<TextAttribute>())).Throws<EntityAlreadyExistsException>();

            var createTextAttributeRequest = new CreateTextAttributeRequest() { SchemaId = 12, MinLength = 1, Maxlength = 15, IsRequired = true, AttributeName = "UserId" };
            var interactor = new CreateTextAttributeInteractor(createTextAttributeRequest, _textAttributeRepositoryMock.Object, _createTextAttributePresenterMock.Object);

            var response = await interactor.Invoke();

            response.Error.Should().Be(CreateTextAttributeResponse.ECreateTextAttributeError.AlreadyExists);
        }

        [Fact]
        public async void response_error_should_be_unknown_if_repository_throws_generic_repository_exception()
        {
            _textAttributeRepositoryMock.Setup(repo => repo.CreateTextAttribute(It.IsAny<int>(), It.IsAny<TextAttribute>())).Throws<RepositoryException>();

            var createTextAttributeRequest = new CreateTextAttributeRequest() { SchemaId = 12, MinLength = 1, Maxlength = 15, IsRequired = true, AttributeName = "UserId" };
            var interactor = new CreateTextAttributeInteractor(createTextAttributeRequest, _textAttributeRepositoryMock.Object, _createTextAttributePresenterMock.Object);

            var response = await interactor.Invoke();

            response.Error.Should().Be(CreateTextAttributeResponse.ECreateTextAttributeError.Unkown);
        }

        [Fact]
        public async void response_error_should_be_min_length_greater_than_max_length_if_text_attribute_max_length_is_greater_than_min_length()
        {
            var createTextAttributeRequest = new CreateTextAttributeRequest() { SchemaId = 12, MinLength = 20, Maxlength = 15, IsRequired = true, AttributeName = "UserId" };
            var interactor = new CreateTextAttributeInteractor(createTextAttributeRequest, _textAttributeRepositoryMock.Object, _createTextAttributePresenterMock.Object);

            var response = await interactor.Invoke();

            response.Error.Should().Be(CreateTextAttributeResponse.ECreateTextAttributeError.MinLengthGreaterThanMaxLength);
        }

        [Fact]
        public async void response_error_should_be_min_length_lower_than_zero_if_text_attribute_min_length_is_lower_than_zero()
        {
            var createTextAttributeRequest = new CreateTextAttributeRequest() { SchemaId = 12, MinLength = -1, Maxlength = 15, IsRequired = true, AttributeName = "UserId" };
            var interactor = new CreateTextAttributeInteractor(createTextAttributeRequest, _textAttributeRepositoryMock.Object, _createTextAttributePresenterMock.Object);

            var response = await interactor.Invoke();

            response.Error.Should().Be(CreateTextAttributeResponse.ECreateTextAttributeError.MinLengthLowerThanZero);
        }
    }
}
