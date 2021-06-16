using System.Threading.Tasks;
using Apeyai.Core.Infra.Persistence.Exceptions.RepositoryExceptions;
using Apeyai.Core.Infra.Persistence.Ports;
using Apeyai.Core.UseCases.Schemas.Create;
using FluentAssertions;
using Moq;
using Xunit;
using static Apeyai.Core.UseCases.Schemas.Create.CreateSchemaResponse;

namespace Apeyai.Core.Test.UseCases.Schemas.Create
{
    public class CreateTextAttributeInteractorTest
    {
        public Mock<ISchemaRepository> _schemaRepositoryMock;
        public Mock<CreateSchemaPresenter> _createSchemaPresenterMock;

        public CreateTextAttributeInteractorTest()
        {
            _schemaRepositoryMock = new Mock<ISchemaRepository>();
            _createSchemaPresenterMock = new Mock<CreateSchemaPresenter>();
        }

        [Fact]
        public async void response_schema_id_should_be_the_id_returned_by_repository()
        {
            _schemaRepositoryMock.Setup(repo => repo.CreateEmptySchema(It.IsAny<string>())).Returns(Task.FromResult(12));

            var createSchemaRequest = new CreateSchemaRequest() { SchemaName = "Toto" };
            var interactor = new CreateSchemaInteractor(createSchemaRequest, _schemaRepositoryMock.Object, _createSchemaPresenterMock.Object);

            var response = await interactor.Invoke();
            response.SchemaId.Should().Be(12);
        }

        [Fact]
        public async void response_error_should_be_already_exists_if_repository_throws_entity_already_exists_exception()
        {
            _schemaRepositoryMock.Setup(repo => repo.CreateEmptySchema(It.IsAny<string>())).Throws<EntityAlreadyExistsException>();

            var createSchemaRequest = new CreateSchemaRequest() { SchemaName = "Toto" };
            var interactor = new CreateSchemaInteractor(createSchemaRequest, _schemaRepositoryMock.Object, _createSchemaPresenterMock.Object);

            var response = await interactor.Invoke();
            response.Error.Should().Be(ECreateSchemaError.AlreadyExists);
        }

        [Fact]
        public async void response_error_should_be_unknown_if_repository_throws_generic_repository_exception()
        {
            _schemaRepositoryMock.Setup(repo => repo.CreateEmptySchema(It.IsAny<string>())).Throws<RepositoryException>();

            var createSchemaRequest = new CreateSchemaRequest() { SchemaName = "Toto" };
            var interactor = new CreateSchemaInteractor(createSchemaRequest, _schemaRepositoryMock.Object, _createSchemaPresenterMock.Object);

            var response = await interactor.Invoke();
            response.Error.Should().Be(ECreateSchemaError.Unknown);
        }
    }
}
