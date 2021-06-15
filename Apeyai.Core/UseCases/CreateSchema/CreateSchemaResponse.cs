using Apeyai.Core.Common.UseCases;
using Apeyai.Core.Ports.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apeyai.Core.UseCases.CreateSchema
{
    public class CreateSchemaResponse: IUseCaseResponse
    {
        public int SchemaId { get; internal set; }
    }
}
