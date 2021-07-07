using System;
using Apeyai.API.Common;
using Apeyai.Core.UseCases.AddTextAttributeToSchema;
using Apeyai.Core.UseCases.CreateEmptySchema;
using Microsoft.AspNetCore.Mvc;

namespace Apeyai.API.UseCases.AddTextAttributeToSchema
{
    public class AddTextAttributeToSchemaHttpPresenter : BaseHttpPresenter<AddTextAttributeToSchemaResponse>, IAddTextAttributeToSchemaPresenter
    {
        public override void PresentSuccess(AddTextAttributeToSchemaResponse response)
            => Result = new StatusCodeResult(201);

        public void PresentMinLengthGreaterThanMaxLengthError()
            => Result = new BadRequestObjectResult("Min length cannot be greater than max length.");

        public void PresentMinLengthLowerThanZeroError()
            => Result = new BadRequestObjectResult("Min length cannot be lower than zero.");

        public void PresentTextAttributeNameIsNullOrWhitespacesError()
            => Result = new BadRequestObjectResult("Attribute name must not be empty.");

        public void PresentAttributeAlreadyExistsException()
            => Result = new ConflictObjectResult("An attribute with this name already exists in the schema.");

        public void PresentSchemaNotFoundException()
            => Result = new NotFoundObjectResult("Schema not found.");
    }
}
