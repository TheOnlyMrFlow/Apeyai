using System;
using Apeyai.API.Common;
using Apeyai.API.ViewModels;
using Apeyai.Core.UseCases.CreateEmptySchema;
using Apeyai.Core.UseCases.GetSchema;
using Microsoft.AspNetCore.Mvc;

namespace Apeyai.API.UseCases.GetSchema
{
    public class GetSchemaHttpPresenter : BaseHttpPresenter<GetSchemaResponse>, IGetSchemaPresenter
    {
        public override void PresentSuccess(GetSchemaResponse response)
            => Result = new OkObjectResult(new GetSchemaHttpResponse() { Schema = response.Schema.ToViewModel() });

        public void PresentSchemaNotFoundError() => Result = new NotFoundResult();
    }
}
