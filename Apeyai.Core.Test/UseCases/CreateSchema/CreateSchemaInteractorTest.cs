using Apeyai.Core.Common.UseCases;
using Apeyai.Core.Ports.Persistence;
using Apeyai.Core.UseCases.CreateSchema;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Apeyai.Core.Test.UseCases.CreateSchema
{
    public class CreateSchemaInteractorTest
    {
        public Mock<ISchemaRepository> _schemaRepositoryMock;
        public Mock<UseCasePresenter<CreateSchemaResponse>> _schemaResponsePresenterMock;

        public CreateSchemaInteractorTest()
        {
            _schemaRepositoryMock = new Mock<ISchemaRepository>();
            _schemaResponsePresenterMock = new Mock<UseCasePresenter<CreateSchemaResponse>>();
        }

        [Fact]
        public async void response_schema_id_should_be_the_id_returned_by_repository()
        {
            _schemaRepositoryMock.Setup(repo => repo.CreateEmptySchema(It.IsAny<string>())).Returns(Task.FromResult(12));

            var createSchemaRequest = new CreateSchemaRequest() { SchemaName = "Toto" };
            var interactor = new CreateSchemaInteractor(createSchemaRequest, _schemaRepositoryMock.Object, _schemaResponsePresenterMock.Object);

            var response = await interactor.Invoke();
            response.SchemaId.Should().Be(12);
        }

    }
}
