using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apeyai.Core.Common.UseCases
{
    public abstract class UseCasePresenter<T> where T : IUseCaseResponse
    {
        public abstract Task Present(T response);
    }
}
