using Apeyai.Core.Common.UseCases;

namespace Apeyai.Core.UseCases.AddTextAttributeToSchema
{
    public class AddTextAttributeToSchemaResponse : IUseCaseResponse
    {
        public enum ECreateTextAttributeError
        {
            AlreadyExists,
            MinLengthGreaterThanMaxLength,
            MinLengthLowerThanZero,
            Unknown
        }

        public ECreateTextAttributeError? Error { get; set; }

        public bool Success => Error == null;
    }
}
