using System.Collections.Generic;
using System.Threading.Tasks;
using Apeyai.Core.Entities;
using Apeyai.Core.Infra.Persistence.Exceptions.RepositoryExceptions;
using Apeyai.Core.Infra.Persistence.Ports;
using Apeyai.Core.UseCases.GetSchema;
using FluentAssertions;
using Moq;
using Xunit;

namespace Apeyai.Core.Test.UseCases.GetSchema
{
    public class GetSchemaInteractorTest
    {
        private readonly Mock<ISchemaRepository> _schemaRepositoryMock;
        private readonly Mock<IGetSchemaPresenter> _getSchemaPresenterMock;

        public GetSchemaInteractorTest()
        {
            _schemaRepositoryMock = new Mock<ISchemaRepository>();
            _getSchemaPresenterMock = new Mock<IGetSchemaPresenter>();
        }

        [Fact]
        public async Task response_schema_should_be_the_one_returned_by_repository()
        {
            var schemaReturnedByRepo = new Schema()
            {
                BooleanAttributes = new List<BooleanAttribute>() {new() {IsRequired = true}},
                TextAttributes = new List<TextAttribute>(0)
            };
            _schemaRepositoryMock.Setup(repo => repo.GetSchema("Toto")).Returns(Task.FromResult(schemaReturnedByRepo));

            var getSchemaRequest = new GetSchemaRequest() { SchemaName = "Toto" };
            var interactor = new GetSchemaInteractor(getSchemaRequest, _schemaRepositoryMock.Object, _getSchemaPresenterMock.Object);

            await interactor.Invoke();

            _getSchemaPresenterMock.Verify(p => p.PresentSuccess(It.IsAny<GetSchemaResponse>()));
        }

        [Fact]
        public async Task response_error_should_be_not_found_if_repository_throws_not_found_exception()
        {
            _schemaRepositoryMock.Setup(repo => repo.GetSchema("Toto")).Throws<SchemaNotFoundException>();

            var getSchemaRequest = new GetSchemaRequest() { SchemaName = "Toto" };
            var interactor = new GetSchemaInteractor(getSchemaRequest, _schemaRepositoryMock.Object, _getSchemaPresenterMock.Object);

            await interactor.Invoke();

            _getSchemaPresenterMock.Verify(p => p.PresentSchemaNotFoundError());
        }

        [Fact]
        public async Task response_error_should_be_unknown_if_repository_throws_generic_repository_exception()
        {
            _schemaRepositoryMock.Setup(repo => repo.GetSchema("Toto")).Throws<RepositoryException>();

            var getSchemaRequest = new GetSchemaRequest() { SchemaName = "Toto" };
            var interactor = new GetSchemaInteractor(getSchemaRequest, _schemaRepositoryMock.Object, _getSchemaPresenterMock.Object);

            await interactor.Invoke();

            _getSchemaPresenterMock.Verify(p => p.PresentUnknownError());
        }
    }
}
