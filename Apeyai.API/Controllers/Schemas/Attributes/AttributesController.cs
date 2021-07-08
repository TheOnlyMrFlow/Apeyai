using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Apeyai.API.UseCases.AddForeignSchemaReferenceAttributeToSchema;
using Apeyai.API.UseCases.AddTextAttributeToSchema;
using Apeyai.API.UseCases.CreateEmptySchema;
using Apeyai.API.UseCases.GetSchema;
using Apeyai.Core.Infra.Persistence.Ports;
using Apeyai.Core.UseCases.AddForeignSchemaReferenceAttributeToSchema;
using Apeyai.Core.UseCases.AddTextAttributeToSchema;
using Apeyai.Core.UseCases.CreateEmptySchema;
using Apeyai.Core.UseCases.GetSchema;

namespace Apeyai.API.Controllers.Schemas.Attributes
{
    [ApiController]
    [Route("schemas/{schemaName}/attributes")]
    public class AttributesController : ControllerBase
    { 
        private readonly ILogger<AttributesController> _logger;
        private readonly AddTextAttributeToSchemaHttpPresenter _addTextAttributeToSchemaPresenter;
        private readonly AddForeignSchemaReferenceAttributeToSchemaHttpPresenter _addForeignSchemaReferenceAttributeToSchemaPresenter;
        private readonly ISchemaRepository _schemaRepository;

        public AttributesController(
            ILogger<AttributesController> logger,
            AddTextAttributeToSchemaHttpPresenter addTextAttributeToSchemaPresenter,
            ISchemaRepository schemaRepository,
            AddForeignSchemaReferenceAttributeToSchemaHttpPresenter addForeignSchemaReferenceAttributeToSchemaPresenter)
        {
            _logger = logger;
            _schemaRepository = schemaRepository;
            _addForeignSchemaReferenceAttributeToSchemaPresenter = addForeignSchemaReferenceAttributeToSchemaPresenter;
            _addTextAttributeToSchemaPresenter = addTextAttributeToSchemaPresenter;
        }


        [HttpPost("text")]
        public async Task<IActionResult> AddTextAttributeToSchema([FromRoute] string schemaName, AddTextAttributeToSchemaHttpRequest httpRequest)
        {
            httpRequest.SchemaName = schemaName;
            var request = httpRequest.ToBusinessRequest();
            var interactor = new AddTextAttributeToSchemaInteractor(request, _schemaRepository, _addTextAttributeToSchemaPresenter);
            
            await interactor.Invoke();

            return _addTextAttributeToSchemaPresenter.Result;
        }

        [HttpPost("ref")]
        public async Task<IActionResult> AddForeignSchemaReferenceAttributeToSchema([FromRoute] string schemaName, AddForeignSchemaReferenceAttributeToSchemaHttpRequest httpRequest)
        {
            httpRequest.SchemaName = schemaName;
            var request = httpRequest.ToBusinessRequest();
            var interactor = new AddForeignSchemaReferenceAttributeToSchemaInteractor(request, _schemaRepository, _addForeignSchemaReferenceAttributeToSchemaPresenter);

            await interactor.Invoke();

            return _addForeignSchemaReferenceAttributeToSchemaPresenter.Result;
        }
    }
}
