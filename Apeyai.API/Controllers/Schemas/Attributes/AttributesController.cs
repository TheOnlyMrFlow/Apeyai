using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Apeyai.API.UseCases.AddTextAttributeToSchema;
using Apeyai.API.UseCases.CreateEmptySchema;
using Apeyai.API.UseCases.GetSchema;
using Apeyai.Core.Infra.Persistence.Ports;
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
        private readonly ISchemaRepository _schemaRepository;

        public AttributesController(ILogger<AttributesController> logger, AddTextAttributeToSchemaHttpPresenter addTextAttributeToSchemaPresenter, ISchemaRepository schemaRepository)
        {
            _logger = logger;
            _schemaRepository = schemaRepository;
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
    }
}
