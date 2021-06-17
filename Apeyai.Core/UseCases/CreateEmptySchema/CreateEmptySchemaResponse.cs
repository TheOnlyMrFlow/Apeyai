using Apeyai.Core.Common.UseCases;

namespace Apeyai.Core.UseCases.CreateEmptySchema
{
    public class CreateEmptySchemaResponse: IUseCaseResponse
    {
        public enum ECreateSchemaError
        {
            AlreadyExists,
            Unknown
        }

        public ECreateSchemaError? Error { get; internal set; }

        public bool Success => Error == null;
    }
}
