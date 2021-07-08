using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apeyai.Core.Common.UseCases;

namespace Apeyai.Core.UseCases.RemoveAttributeFromSchema
{
    public interface IRemoveAttributeFromSchemaPresenter : IUseCasePresenter<RemoveAttributeFromSchemaResponse>
    {
        void PresentSchemaNotFoundError();
        void PresentAttributeNotFoundError();
    }
}
