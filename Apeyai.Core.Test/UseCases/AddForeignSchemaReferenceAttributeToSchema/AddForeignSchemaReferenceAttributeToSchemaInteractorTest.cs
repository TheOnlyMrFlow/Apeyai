using System.Threading.Tasks;
using Apeyai.Core.Entities.Attributes;
using Apeyai.Core.Infra.Persistence.Exceptions.RepositoryExceptions;
using Apeyai.Core.Infra.Persistence.Ports;
using Apeyai.Core.UseCases.AddForeignSchemaReferenceAttributeToSchema;
using Moq;
using Xunit;

namespace Apeyai.Core.Test.UseCases.AddForeignSchemaReferenceAttributeToSchema
{
    public class AddForeignSchemaReferenceAttributeToSchemaInteractorTest
    {
        private readonly Mock<ISchemaRepository> _schemaRepositoryMock;
        private readonly Mock<IAddForeignSchemaReferenceAttributeToSchemaPresenter> _addForeignSchemaReferenceAttributeToSchemaPresenterMock;

        public AddForeignSchemaReferenceAttributeToSchemaInteractorTest()
        {
            _schemaRepositoryMock = new Mock<ISchemaRepository>();
            _schemaRepositoryMock
                .Setup(repo =>
                    repo.SchemaHasAttributeAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(false);
            _schemaRepositoryMock
                .Setup(repo =>
                    repo.SchemaExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            _addForeignSchemaReferenceAttributeToSchemaPresenterMock = new Mock<IAddForeignSchemaReferenceAttributeToSchemaPresenter>();
        }

        [Fact]
        public async Task present_success_should_be_called_if_repository_doesnt_throw()
        {
            var createForeignSchemaRefAttributeRequest = new AddForeignSchemaReferenceAttributeToSchemaRequest()
            {
                AttributeName = "JeanBobAttribute",
                SchemaName = "TotoSchema",
                ForeignSchemaName = "TutuSchema",
                IsRequired = true
            };

            var interactor = new AddForeignSchemaReferenceAttributeToSchemaInteractor(createForeignSchemaRefAttributeRequest, _schemaRepositoryMock.Object, _addForeignSchemaReferenceAttributeToSchemaPresenterMock.Object);

            await interactor.Invoke();

            _addForeignSchemaReferenceAttributeToSchemaPresenterMock.Verify(p => p.PresentSuccess(It.IsAny<AddForeignSchemaReferenceAttributeToSchemaResponse>()));
        }

        [Fact]
        public async Task present_schema_not_found_error_should_be_called_if_repository_throws_schema_not_found_exception()
        {
            _schemaRepositoryMock
                .Setup(repo => repo
                    .AddForeignSchemaReferenceAttributeToSchemaAsync("TotoSchema", It.IsAny<ForeignSchemaReferenceAttribute>()))
                .Throws<SchemaNotFoundException>();

            var createForeignSchemaRefAttributeRequest = new AddForeignSchemaReferenceAttributeToSchemaRequest()
            {
                AttributeName = "JeanBobAttribute",
                SchemaName = "TotoSchema",
                ForeignSchemaName = "TutuSchema",
                IsRequired = true
            };

            var interactor = new AddForeignSchemaReferenceAttributeToSchemaInteractor(
                createForeignSchemaRefAttributeRequest,
                _schemaRepositoryMock.Object,
                _addForeignSchemaReferenceAttributeToSchemaPresenterMock.Object);

            await interactor.Invoke();

            _addForeignSchemaReferenceAttributeToSchemaPresenterMock
                .Verify(p => p.PresentSchemaNotFoundError());
        }

        [Fact]
        public async Task present_foreign_schema_not_found_error_should_be_called_if_foreign_schema_doesnt_exist()
        {
            _schemaRepositoryMock
                .Setup(repo => repo.SchemaExistsAsync("TutuSchema"))
                .ReturnsAsync(false);

            var createForeignSchemaRefAttributeRequest = new AddForeignSchemaReferenceAttributeToSchemaRequest()
            {
                AttributeName = "JeanBobAttribute",
                SchemaName = "TotoSchema",
                ForeignSchemaName = "TutuSchema",
                IsRequired = true
            };

            var interactor = new AddForeignSchemaReferenceAttributeToSchemaInteractor(
                createForeignSchemaRefAttributeRequest,
                _schemaRepositoryMock.Object,
                _addForeignSchemaReferenceAttributeToSchemaPresenterMock.Object);

            await interactor.Invoke();

            _addForeignSchemaReferenceAttributeToSchemaPresenterMock.Verify(p => p.PresentForeignSchemaNotFoundError());
        }

        [Fact]
        public async Task present_attribute_already_exists_error_should_be_called_if_attribute_with_same_name_already_exists_in_schema()
        {
            _schemaRepositoryMock
                .Setup(repo => repo.SchemaHasAttributeAsync("TotoSchema", "JeanBobAttribute"))
                .ReturnsAsync(true);

            var createForeignSchemaRefAttributeRequest = new AddForeignSchemaReferenceAttributeToSchemaRequest()
            {
                AttributeName = "JeanBobAttribute",
                SchemaName = "TotoSchema",
                ForeignSchemaName = "TutuSchema",
                IsRequired = true
            };

            var interactor = new AddForeignSchemaReferenceAttributeToSchemaInteractor(
                createForeignSchemaRefAttributeRequest,
                _schemaRepositoryMock.Object,
                _addForeignSchemaReferenceAttributeToSchemaPresenterMock.Object);

            await interactor.Invoke();

            _addForeignSchemaReferenceAttributeToSchemaPresenterMock.Verify(p => p.PresentAttributeAlreadyExistsError());
        }
    }
}
