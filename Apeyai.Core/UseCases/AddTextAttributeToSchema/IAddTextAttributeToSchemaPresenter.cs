using Apeyai.Core.Common.UseCases;

namespace Apeyai.Core.UseCases.AddTextAttributeToSchema
{
    public interface IAddTextAttributeToSchemaPresenter : IUseCasePresenter<AddTextAttributeToSchemaResponse>
    {
        void PresentMinLengthGreaterThanMaxLengthError();
        void PresentMinLengthLowerThanZeroError();
        void PresentTextAttributeNameIsNullOrWhitespacesError();
        void PresentSchemaAlreadyExistsError();
        void PresentAttributeAlreadyExistsException();
    }
}
