using Apeyai.Core.Infra.Persistence.Exceptions.RepositoryExceptions;
using Apeyai.Core.Infra.Persistence.Ports;
using Apeyai.Core.UseCases.RemoveAttributeFromSchema;
using FluentAssertions;
using Moq;
using Xunit;

namespace Apeyai.Core.Test.UseCases.CreateEmptySchema
{
    public class RemoveAttributeFromSchemaInteractorTest
    {
        public Mock<ISchemaRepository> _schemaRepositoryMock;
        public Mock<IRemoveAttributeFromSchemaPresenter> _removeAttributeFromSchemaPresenterMock;

        public RemoveAttributeFromSchemaInteractorTest()
        {
            _schemaRepositoryMock = new Mock<ISchemaRepository>();
            _removeAttributeFromSchemaPresenterMock = new Mock<IRemoveAttributeFromSchemaPresenter>();
        }

        [Fact]
        public async void repository_method_removeAttributeFroMSchema_should_be_called_with_appropriate_args()
        {
            var request = new RemoveAttributeFromSchemaRequest() { SchemaName = "Toto", AttributeName = "User" };
            var interactor = new RemoveAttributeFromSchemaInteractor(request, _schemaRepositoryMock.Object, _removeAttributeFromSchemaPresenterMock.Object);

            var response = await interactor.Invoke();

            _schemaRepositoryMock.Verify(repo => repo.RemoveAttributeFromSchema("Toto", "User"), Times.Once);
        }

        [Fact]
        public async void response_success_should_be_true_if_repository_does_not_throw()
        {
            var request = new RemoveAttributeFromSchemaRequest() { AttributeName = "User" };
            var interactor = new RemoveAttributeFromSchemaInteractor(request, _schemaRepositoryMock.Object, _removeAttributeFromSchemaPresenterMock.Object);

            var response = await interactor.Invoke();

            response.Success.Should().BeTrue();
        }

        [Fact]
        public async void response_error_should_be_schema_not_found_if_repository_throws_schema_not_found_exception()
        {
            _schemaRepositoryMock
                .Setup(repo => repo.RemoveAttributeFromSchema("Toto", "User"))
                .Throws<SchemaNotFoundException>();

            var request = new RemoveAttributeFromSchemaRequest() { SchemaName = "Toto", AttributeName = "User" };
            var interactor = new RemoveAttributeFromSchemaInteractor(request, _schemaRepositoryMock.Object, _removeAttributeFromSchemaPresenterMock.Object);

            var response = await interactor.Invoke();

            response.Success.Should().BeFalse();
            response.Error.Should().Be(RemoveAttributeFromSchemaResponse.ERemoveAttributeFromSchemaError.SchemaNotFound);
        }

        [Fact]
        public async void response_error_should_be_attribute_not_found_if_repository_throws_attribute_not_found_exception()
        {
            _schemaRepositoryMock
                .Setup(repo => repo.RemoveAttributeFromSchema("Toto", "User"))
                .Throws<AttributeNotFoundException>();

            var request = new RemoveAttributeFromSchemaRequest() { SchemaName = "Toto", AttributeName = "User" };
            var interactor = new RemoveAttributeFromSchemaInteractor(request, _schemaRepositoryMock.Object, _removeAttributeFromSchemaPresenterMock.Object);

            var response = await interactor.Invoke();

            response.Success.Should().BeFalse();
            response.Error.Should().Be(RemoveAttributeFromSchemaResponse.ERemoveAttributeFromSchemaError.AttributeNotFound);
        }

        [Fact]
        public async void response_error_should_be_unknown_if_repository_throws_generic_repository_exception()
        {
            _schemaRepositoryMock
                .Setup(repo => repo.RemoveAttributeFromSchema("Toto", "User"))
                .Throws<RepositoryException>();

            var request = new RemoveAttributeFromSchemaRequest() { SchemaName = "Toto", AttributeName = "User" };
            var interactor = new RemoveAttributeFromSchemaInteractor(request, _schemaRepositoryMock.Object, _removeAttributeFromSchemaPresenterMock.Object);

            var response = await interactor.Invoke();

            response.Success.Should().BeFalse();
            response.Error.Should().Be(RemoveAttributeFromSchemaResponse.ERemoveAttributeFromSchemaError.Unknown);
        }
    }
}
