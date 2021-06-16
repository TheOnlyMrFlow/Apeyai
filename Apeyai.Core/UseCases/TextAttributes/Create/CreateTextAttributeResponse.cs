using Apeyai.Core.Common.UseCases;

namespace Apeyai.Core.UseCases.TextAttributes.Create
{
    public class CreateTextAttributeResponse : IUseCaseResponse
    {
        public enum ECreateTextAttributeError
        {
            AlreadyExists,
            MinLengthGreaterThanMaxLength,
            MinLengthLowerThanZero,
            Unkown
        }

        public int? AttributeId { get; set; }

        public ECreateTextAttributeError? Error { get; set; }
    }
}
