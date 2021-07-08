using System;
using Apeyai.API.Common;
using Apeyai.Core.UseCases.CreateEmptySchema;
using Microsoft.AspNetCore.Mvc;

namespace Apeyai.API.UseCases.CreateEmptySchema
{
    public class CreateEmptySchemaHttpPresenter : BaseHttpPresenter<CreateEmptySchemaResponse>, ICreateEmptySchemaPresenter
    {
        public override void PresentSuccess(CreateEmptySchemaResponse response)
            => Result = new StatusCodeResult(201);

        public void PresentSchemaAlreadyExistsError() => Result = new ConflictResult();
    }
}
