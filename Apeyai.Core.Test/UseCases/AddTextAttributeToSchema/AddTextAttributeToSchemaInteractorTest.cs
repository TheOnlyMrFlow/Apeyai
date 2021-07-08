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
        private readonly Mock<ISchemaRepository> _schemaRepositoryMock;
        private readonly Mock<IAddTextAttributeToSchemaPresenter> _addTextAttributeToSchemaPresenterMock;

        public AddTextAttributeToSchemaInteractorTest()
        {
            _schemaRepositoryMock = new Mock<ISchemaRepository>();
            _addTextAttributeToSchemaPresenterMock = new Mock<IAddTextAttributeToSchemaPresenter>();
        }

        [Fact]
        public async Task present_success_should_be_called_if_repository_doesnt_throw()
        {
            var createTextAttributeRequest = new AddTextAttributeToSchemaRequest() { SchemaName = "Toto", MinLength = 1, Maxlength = 15, IsRequired = true, AttributeName = "UserId"};
            var interactor = new AddTextAttributeToSchemaInteractor(createTextAttributeRequest, _schemaRepositoryMock.Object, _addTextAttributeToSchemaPresenterMock.Object);

            await interactor.Invoke();

            _addTextAttributeToSchemaPresenterMock.Verify(p => p.PresentSuccess(It.IsAny<AddTextAttributeToSchemaResponse>()));
        }

        [Fact]
        public async Task present_schema_not_found_error_should_be_called_if_repository_throws_schema_not_found_exception()
        {
            _schemaRepositoryMock.Setup(repo => repo.AddTextAttributeToSchema("Toto", It.IsAny<TextAttribute>())).Throws<SchemaNotFoundException>();

            var createTextAttributeRequest = new AddTextAttributeToSchemaRequest() { SchemaName = "Toto", MinLength = 1, Maxlength = 15, IsRequired = true, AttributeName = "UserId" };
            var interactor = new AddTextAttributeToSchemaInteractor(createTextAttributeRequest, _schemaRepositoryMock.Object, _addTextAttributeToSchemaPresenterMock.Object);

            await interactor.Invoke();

            _addTextAttributeToSchemaPresenterMock.Verify(p => p.PresentSchemaNotFoundException());
        }

        [Fact]
        public async Task present_schema_already_exists_error_should_be_called_if_repository_throws_attribute_already_exists_exception()
        {
            _schemaRepositoryMock.Setup(repo => repo.AddTextAttributeToSchema("Toto", It.IsAny<TextAttribute>())).Throws<AttributeAlreadyExistsException>();

            var createTextAttributeRequest = new AddTextAttributeToSchemaRequest() { SchemaName = "Toto", MinLength = 1, Maxlength = 15, IsRequired = true, AttributeName = "UserId" };
            var interactor = new AddTextAttributeToSchemaInteractor(createTextAttributeRequest, _schemaRepositoryMock.Object, _addTextAttributeToSchemaPresenterMock.Object);

            await interactor.Invoke();

            _addTextAttributeToSchemaPresenterMock.Verify(p => p.PresentAttributeAlreadyExistsException());
        }

        [Fact]
        public async Task present_unknown_error_should_be_called_if_repository_throws_generic_repository_exception()
        {
            _schemaRepositoryMock.Setup(repo => repo.AddTextAttributeToSchema("Toto", It.IsAny<TextAttribute>())).Throws<RepositoryException>();

            var createTextAttributeRequest = new AddTextAttributeToSchemaRequest() { SchemaName = "Toto", MinLength = 1, Maxlength = 15, IsRequired = true, AttributeName = "UserId" };
            var interactor = new AddTextAttributeToSchemaInteractor(createTextAttributeRequest, _schemaRepositoryMock.Object, _addTextAttributeToSchemaPresenterMock.Object);

            await interactor.Invoke();

            _addTextAttributeToSchemaPresenterMock.Verify(p => p.PresentUnknownError());
        }

        [Fact]
        public async Task present_min_length_min_length_greater_than_max_length_error_should_be_called_if_text_attribute_min_length_is_greater_than_max_length()
        {
            var createTextAttributeRequest = new AddTextAttributeToSchemaRequest() { SchemaName = "Toto", MinLength = 20, Maxlength = 15, IsRequired = true, AttributeName = "UserId" };
            var interactor = new AddTextAttributeToSchemaInteractor(createTextAttributeRequest, _schemaRepositoryMock.Object, _addTextAttributeToSchemaPresenterMock.Object);

            await interactor.Invoke();

            _addTextAttributeToSchemaPresenterMock.Verify(p => p.PresentMinLengthGreaterThanMaxLengthError());
        }

        [Fact]
        public async Task presennt_min_length_lower_than_zero_error_should_be_called_if_text_attribute_min_length_is_lower_than_zero()
        {
            var createTextAttributeRequest = new AddTextAttributeToSchemaRequest() { SchemaName = "toto", MinLength = -1, Maxlength = 15, IsRequired = true, AttributeName = "UserId" };
            var interactor = new AddTextAttributeToSchemaInteractor(createTextAttributeRequest, _schemaRepositoryMock.Object, _addTextAttributeToSchemaPresenterMock.Object);

            await interactor.Invoke();

            _addTextAttributeToSchemaPresenterMock.Verify(p => p.PresentMinLengthLowerThanZeroError());
        }
    }
}
