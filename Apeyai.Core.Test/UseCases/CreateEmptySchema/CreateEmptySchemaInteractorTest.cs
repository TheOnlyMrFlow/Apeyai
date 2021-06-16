using System.Threading.Tasks;
using Apeyai.Core.Infra.Persistence.Exceptions.RepositoryExceptions;
using Apeyai.Core.Infra.Persistence.Ports;
using Apeyai.Core.UseCases.CreateEmptySchema;
using FluentAssertions;
using Moq;
using Xunit;
using static Apeyai.Core.UseCases.CreateEmptySchema.CreateEmptySchemaResponse;

namespace Apeyai.Core.Test.UseCases.CreateEmptySchema
{
    public class CreateEmptySchemaInteractorTest
    {
        public Mock<ISchemaRepository> _schemaRepositoryMock;
        public Mock<ICreateEmptySchemaPresenter> _createSchemaPresenterMock;

        public CreateEmptySchemaInteractorTest()
        {
            _schemaRepositoryMock = new Mock<ISchemaRepository>();
            _createSchemaPresenterMock = new Mock<ICreateEmptySchemaPresenter>();
        }

        [Fact]
        public async void response_schema_id_should_be_the_id_returned_by_repository()
        {
            _schemaRepositoryMock.Setup(repo => repo.CreateEmptySchema(It.IsAny<string>())).Returns(Task.FromResult(12));

            var createSchemaRequest = new CreateEmptySchemaRequest() { SchemaName = "Toto" };
            var interactor = new CreateEmptySchemaInteractor(createSchemaRequest, _schemaRepositoryMock.Object, _createSchemaPresenterMock.Object);

            var response = await interactor.Invoke();

            response.Success.Should().BeTrue();
            response.SchemaId.Should().Be(12);
        }

        [Fact]
        public async void response_error_should_be_already_exists_if_repository_throws_entity_already_exists_exception()
        {
            _schemaRepositoryMock.Setup(repo => repo.CreateEmptySchema(It.IsAny<string>())).Throws<EntityAlreadyExistsException>();

            var createSchemaRequest = new CreateEmptySchemaRequest() { SchemaName = "Toto" };
            var interactor = new CreateEmptySchemaInteractor(createSchemaRequest, _schemaRepositoryMock.Object, _createSchemaPresenterMock.Object);

            var response = await interactor.Invoke();

            response.Success.Should().BeFalse();
            response.SchemaId.Should().BeNull();
            response.Error.Should().Be(ECreateSchemaError.AlreadyExists);
        }

        [Fact]
        public async void response_error_should_be_unknown_if_repository_throws_generic_repository_exception()
        {
            _schemaRepositoryMock.Setup(repo => repo.CreateEmptySchema(It.IsAny<string>())).Throws<RepositoryException>();

            var createSchemaRequest = new CreateEmptySchemaRequest() { SchemaName = "Toto" };
            var interactor = new CreateEmptySchemaInteractor(createSchemaRequest, _schemaRepositoryMock.Object, _createSchemaPresenterMock.Object);

            var response = await interactor.Invoke();

            response.Success.Should().BeFalse();
            response.SchemaId.Should().BeNull();
            response.Error.Should().Be(ECreateSchemaError.Unknown);
        }
    }
}
