using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apeyai.API.UseCases.CreateEmptySchema;
using Apeyai.Core.Infra.Persistence.Ports;
using Apeyai.Core.UseCases.CreateEmptySchema;

namespace Apeyai.API.Controllers.Schemas
{
    [ApiController]
    [Route("[controller]")]
    public class SchemaController : ControllerBase
    { 
        private readonly ILogger<SchemaController> _logger;
        private readonly CreateEmptySchemaHttpPresenter _createEmptySchemaPresenter;
        private readonly ISchemaRepository _schemaRepository;

        public SchemaController(ILogger<SchemaController> logger, CreateEmptySchemaHttpPresenter createEmptySchemaPresenter, ISchemaRepository schemaRepository)
        {
            _logger = logger;
            _createEmptySchemaPresenter = createEmptySchemaPresenter;
            _schemaRepository = schemaRepository;
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
