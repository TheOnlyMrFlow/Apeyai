using Apeyai.Core.Common.UseCases;
using Apeyai.Core.Entities;

namespace Apeyai.Core.UseCases.GetSchema
{
    public class GetSchemaResponse : IUseCaseResponse
    {
        public enum EGetSchemaError
        {
            NotFound,
            Unknown
        }

        public Schema Schema { get; internal set; }

        public EGetSchemaError? Error { get; internal set; }

        public bool Success => Error == null;
    }
}
