using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Apeyai.API.UseCases.CreateEmptySchema;
using Apeyai.API.UseCases.GetSchema;
using Apeyai.Core.Infra.Persistence.Ports;
using Apeyai.Core.UseCases.CreateEmptySchema;
using Apeyai.Core.UseCases.GetSchema;

namespace Apeyai.API.Controllers.Schemas
{
    [ApiController]
    [Route("schemas")]
    public class SchemaController : ControllerBase
    { 
        private readonly ILogger<SchemaController> _logger;
        private readonly GetSchemaHttpPresenter _getSchemaPresenter;
        private readonly CreateEmptySchemaHttpPresenter _createEmptySchemaPresenter;
        private readonly ISchemaRepository _schemaRepository;

        public SchemaController(ILogger<SchemaController> logger, CreateEmptySchemaHttpPresenter createEmptySchemaPresenter, ISchemaRepository schemaRepository, GetSchemaHttpPresenter getSchemaPresenter)
        {
            _logger = logger;
            _schemaRepository = schemaRepository;
            _createEmptySchemaPresenter = createEmptySchemaPresenter;
            _getSchemaPresenter = getSchemaPresenter;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetSchema(string name)
        {
            var request = new GetSchemaRequest() { SchemaName = name };
            var interactor = new GetSchemaInteractor(request, _schemaRepository, _getSchemaPresenter);
            
            await interactor.Invoke();

            return _getSchemaPresenter.Result;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmptySchema(CreateEmptySchemaHttpRequest httpRequest)
        {
            var request = new CreateEmptySchemaRequest() {SchemaName = httpRequest.Name};
            var interactor = new CreateEmptySchemaInteractor(request, _schemaRepository, _createEmptySchemaPresenter);
            
            await interactor.Invoke();

            return _createEmptySchemaPresenter.Result;
        }
    }
}
