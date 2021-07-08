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
        private readonly Mock<ISchemaRepository> _schemaRepositoryMock;
        private readonly Mock<ICreateEmptySchemaPresenter> _createSchemaPresenterMock;

        public CreateEmptySchemaInteractorTest()
        {
            _schemaRepositoryMock = new Mock<ISchemaRepository>();
            _createSchemaPresenterMock = new Mock<ICreateEmptySchemaPresenter>();
        }

        [Fact]
        public async Task response_success_should_be_true_if_repository_doesnt_throw()
        {
            var createSchemaRequest = new CreateEmptySchemaRequest() { SchemaName = "Toto" };
            var interactor = new CreateEmptySchemaInteractor(createSchemaRequest, _schemaRepositoryMock.Object, _createSchemaPresenterMock.Object);

            await interactor.Invoke();

            _createSchemaPresenterMock.Verify(p => p.PresentSuccess(It.IsAny<CreateEmptySchemaResponse>()));
        }

        [Fact]
        public async Task present_schema_already_exists_error_should_be_called_if_repository_throws_entity_already_exists_exception()
        {
            _schemaRepositoryMock.Setup(repo => repo.CreateEmptySchema(It.IsAny<string>())).Throws<EntityAlreadyExistsException>();

            var createSchemaRequest = new CreateEmptySchemaRequest() { SchemaName = "Toto" };
            var interactor = new CreateEmptySchemaInteractor(createSchemaRequest, _schemaRepositoryMock.Object, _createSchemaPresenterMock.Object);

            await interactor.Invoke();

            _createSchemaPresenterMock.Verify(p => p.PresentSchemaAlreadyExistsError());
        }

        [Fact]
        public async Task response_error_should_be_unknown_if_repository_throws_generic_repository_exception()
        {
            _schemaRepositoryMock.Setup(repo => repo.CreateEmptySchema(It.IsAny<string>())).Throws<RepositoryException>();

            var createSchemaRequest = new CreateEmptySchemaRequest() { SchemaName = "Toto" };
            var interactor = new CreateEmptySchemaInteractor(createSchemaRequest, _schemaRepositoryMock.Object, _createSchemaPresenterMock.Object);

            await interactor.Invoke();

            _createSchemaPresenterMock.Verify(p => p.PresentUnknownError());
        }
    }
}
