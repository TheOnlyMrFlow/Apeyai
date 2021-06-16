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

        public int? SchemaId { get; internal set; }

        public ECreateSchemaError? Error { get; internal set; }

        public bool Success => Error == null;
    }
}
