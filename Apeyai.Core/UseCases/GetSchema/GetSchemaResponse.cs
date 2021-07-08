using Apeyai.Core.Common.UseCases;
using Apeyai.Core.Entities;

namespace Apeyai.Core.UseCases.GetSchema
{
    public class GetSchemaResponse : IUseCaseResponse
    {
        public Schema Schema { get; internal set; }
    }
}
