using Apeyai.API.Common;
using Apeyai.Core.UseCases.AddForeignSchemaReferenceAttributeToSchema;
using Microsoft.AspNetCore.Mvc;

namespace Apeyai.API.UseCases.AddForeignSchemaReferenceAttributeToSchema
{
    public class AddForeignSchemaReferenceAttributeToSchemaHttpPresenter
        : BaseHttpPresenter<AddForeignSchemaReferenceAttributeToSchemaResponse>, IAddForeignSchemaReferenceAttributeToSchemaPresenter
    {
        public override void PresentSuccess(AddForeignSchemaReferenceAttributeToSchemaResponse response)
            => Result = new StatusCodeResult(201);

        public void PresentForeignSchemaNotFoundError()
            => Result = new NotFoundObjectResult("The foreign schema you are trying to reference does not exist.");

        public void PresentAttributeAlreadyExistsError()
            => Result = new ConflictObjectResult("This attribute already exists.");

        public void PresentSchemaNotFoundError()
            => Result = new NotFoundObjectResult("Cannot find the schema.");
    }
}
