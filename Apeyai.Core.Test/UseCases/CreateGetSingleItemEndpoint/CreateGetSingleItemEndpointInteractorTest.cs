using System.Threading.Tasks;
using Apeyai.Core.Entities;
using Apeyai.Core.Infra.Persistence.Exceptions.RepositoryExceptions;
using Apeyai.Core.Infra.Persistence.Ports;
using Apeyai.Core.UseCases.AddTextAttributeToSchema;
using Apeyai.Core.UseCases.CreateGetSingleItemEndpoint;
using Moq;
using Xunit;

namespace Apeyai.Core.Test.UseCases.CreateGetSingleItemEndpoint
{
    public class CreateGetSingleItemEndpointInteractorTest
    {
        private readonly Mock<ISchemaRepository> _schemaRepositoryMock;
        private readonly Mock<IEndpointRepository> _endpointRepositoryMock;

        private readonly Mock<ICreateGetSingleItemEndpointPresenter> _createGetSingleItemEndpointPresenterMock;

        public CreateGetSingleItemEndpointInteractorTest()
        {
            _schemaRepositoryMock = new Mock<ISchemaRepository>();
            _endpointRepositoryMock = new Mock<IEndpointRepository>();
            _createGetSingleItemEndpointPresenterMock = new Mock<ICreateGetSingleItemEndpointPresenter>();
        }

        [Fact]
        public async Task present_success_should_be_called_with_appropriate_response()
        {
            var createGetSingleItemEndpointRequest = new CreateGetSingleItemEndpointRequest() { };

            var interactor = new CreateGetSingleItemEndpointInteractor(
                createGetSingleItemEndpointRequest,
                _schemaRepositoryMock.Object,
                _endpointRepositoryMock.Object,
                _createGetSingleItemEndpointPresenterMock.Object);

            await interactor.Invoke();

            _createGetSingleItemEndpointPresenterMock
                .Verify(p => p.PresentSuccess(It.IsAny<CreateGetSingleItemEndpointResponse>()));
        }

        //[Fact]
        //public async Task present_schema_not_found_error_should_be_called_if_repository_throws_schema_not_found_exception()
        //{
        //    _schemaRepositoryMock.Setup(repo => repo.AddTextAttributeToSchema("Toto", It.IsAny<TextAttribute>())).Throws<SchemaAlreadyExistsException>();

        //    var createTextAttributeRequest = new AddTextAttributeToSchemaRequest() { SchemaName = "Toto", MinLength = 1, Maxlength = 15, IsRequired = true, AttributeName = "UserId" };
        //    var interactor = new AddTextAttributeToSchemaInteractor(createTextAttributeRequest, _schemaRepositoryMock.Object, _createGetSingleItemEndpointPresenterMock.Object);

        //    await interactor.Invoke();

        //    _createGetSingleItemEndpointPresenterMock.Verify(p => p.PresentSchemaNotFoundException());
        //}
    }
}
