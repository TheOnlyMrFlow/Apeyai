using Apeyai.Core.Common.UseCases;

namespace Apeyai.Core.UseCases.AddForeignSchemaReferenceAttributeToSchema
{
    public interface IAddForeignSchemaReferenceAttributeToSchemaPresenter: IUseCasePresenter<AddForeignSchemaReferenceAttributeToSchemaResponse>
    {
        void PresentForeignSchemaNotFoundError();
        void PresentAttributeAlreadyExistsError();
        void PresentSchemaNotFoundError();
    }
}
