using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apeyai.Core.Common.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Apeyai.API.Common
{
    public abstract class BaseHttpPresenter<T>: IUseCasePresenter<T> where T : IUseCaseResponse
    {
        public IActionResult Result { get; set; }

        public abstract void PresentSuccess(T response);

        public void PresentUnknownError() => Result = new StatusCodeResult(500);
    }
}
