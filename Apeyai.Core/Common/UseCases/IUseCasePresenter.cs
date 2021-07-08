namespace Apeyai.Core.Common.UseCases
{
    public interface IUseCasePresenter<in T> where T : IUseCaseResponse
    {
        void PresentSuccess(T response);

        void PresentUnknownError();
    }
}
