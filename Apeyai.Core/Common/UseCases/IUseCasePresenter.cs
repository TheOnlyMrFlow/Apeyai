using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apeyai.Core.Common.UseCases
{
    public interface IUseCasePresenter<in T> where T : IUseCaseResponse
    {
        Task Present(T response);
    }
}
