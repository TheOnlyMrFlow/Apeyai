using System.Threading.Tasks;
using Apeyai.Core.Infra.Persistence.Exceptions.RepositoryExceptions;
using Apeyai.Core.Infra.Persistence.Ports;
using Apeyai.Core.UseCases.RemoveAttributeFromSchema;
using Moq;
using Xunit;

namespace Apeyai.Core.Test.UseCases.RemoveAttributeFromSchema
{
    public class RemoveAttributeFromSchemaInteractorTest
    {
        private readonly Mock<ISchemaRepository> _schemaRepositoryMock;
        private readonly Mock<IRemoveAttributeFromSchemaPresenter> _removeAttributeFromSchemaPresenterMock;

        public RemoveAttributeFromSchemaInteractorTest()
        {
            _schemaRepositoryMock = new Mock<ISchemaRepository>();
            _removeAttributeFromSchemaPresenterMock = new Mock<IRemoveAttributeFromSchemaPresenter>();
        }

        [Fact]
        public async Task repository_method_removeAttributeFroMSchema_should_be_called_with_appropriate_args()
        {
            var request = new RemoveAttributeFromSchemaRequest() { SchemaName = "Toto", AttributeName = "User" };
            var interactor = new RemoveAttributeFromSchemaInteractor(request, _schemaRepositoryMock.Object, _removeAttributeFromSchemaPresenterMock.Object);

            await interactor.Invoke();

            _schemaRepositoryMock.Verify(repo => repo.RemoveAttributeFromSchemaAsync("Toto", "User"), Times.Once);
        }

        [Fact]
        public async Task present_success_should_be_called_if_repository_does_not_throw()
        {
            var request = new RemoveAttributeFromSchemaRequest() { AttributeName = "User" };
            var interactor = new RemoveAttributeFromSchemaInteractor(request, _schemaRepositoryMock.Object, _removeAttributeFromSchemaPresenterMock.Object);

            await interactor.Invoke();

            _removeAttributeFromSchemaPresenterMock.Verify(p => p.PresentSuccess(It.IsAny<RemoveAttributeFromSchemaResponse>()));
        }

        [Fact]
        public async Task present_schema_not_found_error_should_be_called_if_repository_throws_schema_not_found_exception()
        {
            _schemaRepositoryMock
                .Setup(repo => repo.RemoveAttributeFromSchemaAsync("Toto", "User"))
                .Throws<SchemaNotFoundException>();

            var request = new RemoveAttributeFromSchemaRequest() { SchemaName = "Toto", AttributeName = "User" };
            var interactor = new RemoveAttributeFromSchemaInteractor(request, _schemaRepositoryMock.Object, _removeAttributeFromSchemaPresenterMock.Object);

            await interactor.Invoke();

            _removeAttributeFromSchemaPresenterMock.Verify(p => p.PresentSchemaNotFoundError());
        }

        [Fact]
        public async Task present_attribute_not_found_error_should_be_called_if_repository_throws_attribute_not_found_exception()
        {
            _schemaRepositoryMock
                .Setup(repo => repo.RemoveAttributeFromSchemaAsync("Toto", "User"))
                .Throws<AttributeNotFoundException>();

            var request = new RemoveAttributeFromSchemaRequest() { SchemaName = "Toto", AttributeName = "User" };
            var interactor = new RemoveAttributeFromSchemaInteractor(request, _schemaRepositoryMock.Object, _removeAttributeFromSchemaPresenterMock.Object);

            await interactor.Invoke();

            _removeAttributeFromSchemaPresenterMock.Verify(p => p.PresentAttributeNotFoundError());
        }

        [Fact]
        public async Task present_unknown_error_should_be_called_if_repository_throws_generic_repository_exception()
        {
            _schemaRepositoryMock
                .Setup(repo => repo.RemoveAttributeFromSchemaAsync("Toto", "User"))
                .Throws<RepositoryException>();

            var request = new RemoveAttributeFromSchemaRequest() { SchemaName = "Toto", AttributeName = "User" };
            var interactor = new RemoveAttributeFromSchemaInteractor(request, _schemaRepositoryMock.Object, _removeAttributeFromSchemaPresenterMock.Object);

            await interactor.Invoke();

            _removeAttributeFromSchemaPresenterMock.Verify(p => p.PresentUnknownError());
        }
    }
}
