using System.Threading.Tasks;
using Apeyai.Core.Entities;
using Apeyai.Core.Infra.Persistence.Exceptions.RepositoryExceptions;
using Apeyai.Core.Infra.Persistence.Ports;
using Apeyai.Core.UseCases.AddTextAttributeToSchema;
using FluentAssertions;
using Moq;
using Xunit;

namespace Apeyai.Core.Test.UseCases.AddTextAttributeToSchema
{
    public class AddTextAttributeToSchemaInteractorTest
    {
        public Mock<ISchemaRepository> _schemaRepositoryMock;
        public Mock<IAddTextAttributeToSchemaPresenter> _createTextAttributePresenterMock;

        public AddTextAttributeToSchemaInteractorTest()
        {
            _schemaRepositoryMock = new Mock<ISchemaRepository>();
            _createTextAttributePresenterMock = new Mock<IAddTextAttributeToSchemaPresenter>();
        }

        [Fact]
        public async void response_text_attribute_id_should_be_the_id_returned_by_repository()
        {
            _schemaRepositoryMock.Setup(repo => repo.AddTextAttributeToSchema("Toto", It.IsAny<TextAttribute>())).Returns(Task.FromResult(9));

            var createTextAttributeRequest = new AddTextAttributeToSchemaRequest() { SchemaName = "Toto", MinLength = 1, Maxlength = 15, IsRequired = true, AttributeName = "UserId"};
            var interactor = new AddTextAttributeToSchemaInteractor(createTextAttributeRequest, _schemaRepositoryMock.Object, _createTextAttributePresenterMock.Object);

            var response = await interactor.Invoke();

            response.Success.Should().BeTrue();
        }

        [Fact]
        public async void response_error_should_be_already_exists_if_repository_throws_entity_already_exists_exception()
        {
            _schemaRepositoryMock.Setup(repo => repo.AddTextAttributeToSchema("Toto", It.IsAny<TextAttribute>())).Throws<EntityAlreadyExistsException>();

            var createTextAttributeRequest = new AddTextAttributeToSchemaRequest() { SchemaName = "Toto", MinLength = 1, Maxlength = 15, IsRequired = true, AttributeName = "UserId" };
            var interactor = new AddTextAttributeToSchemaInteractor(createTextAttributeRequest, _schemaRepositoryMock.Object, _createTextAttributePresenterMock.Object);

            var response = await interactor.Invoke();

            response.Success.Should().BeFalse();
            response.Error.Should().Be(AddTextAttributeToSchemaResponse.ECreateTextAttributeError.AlreadyExists);
        }

        [Fact]
        public async void response_error_should_be_unknown_if_repository_throws_generic_repository_exception()
        {
            _schemaRepositoryMock.Setup(repo => repo.AddTextAttributeToSchema("Toto", It.IsAny<TextAttribute>())).Throws<RepositoryException>();

            var createTextAttributeRequest = new AddTextAttributeToSchemaRequest() { SchemaName = "Toto", MinLength = 1, Maxlength = 15, IsRequired = true, AttributeName = "UserId" };
            var interactor = new AddTextAttributeToSchemaInteractor(createTextAttributeRequest, _schemaRepositoryMock.Object, _createTextAttributePresenterMock.Object);

            var response = await interactor.Invoke();

            response.Success.Should().BeFalse();
            response.Error.Should().Be(AddTextAttributeToSchemaResponse.ECreateTextAttributeError.Unknown);
        }

        [Fact]
        public async void response_error_should_be_min_length_greater_than_max_length_if_text_attribute_max_length_is_greater_than_min_length()
        {
            var createTextAttributeRequest = new AddTextAttributeToSchemaRequest() { SchemaName = "Toto", MinLength = 20, Maxlength = 15, IsRequired = true, AttributeName = "UserId" };
            var interactor = new AddTextAttributeToSchemaInteractor(createTextAttributeRequest, _schemaRepositoryMock.Object, _createTextAttributePresenterMock.Object);

            var response = await interactor.Invoke();

            response.Success.Should().BeFalse();
            response.Error.Should().Be(AddTextAttributeToSchemaResponse.ECreateTextAttributeError.MinLengthGreaterThanMaxLength);
        }

        [Fact]
        public async void response_error_should_be_min_length_lower_than_zero_if_text_attribute_min_length_is_lower_than_zero()
        {
            var createTextAttributeRequest = new AddTextAttributeToSchemaRequest() { SchemaName = "toto", MinLength = -1, Maxlength = 15, IsRequired = true, AttributeName = "UserId" };
            var interactor = new AddTextAttributeToSchemaInteractor(createTextAttributeRequest, _schemaRepositoryMock.Object, _createTextAttributePresenterMock.Object);

            var response = await interactor.Invoke();

            response.Success.Should().BeFalse();
            response.Error.Should().Be(AddTextAttributeToSchemaResponse.ECreateTextAttributeError.MinLengthLowerThanZero);
        }
    }
}
