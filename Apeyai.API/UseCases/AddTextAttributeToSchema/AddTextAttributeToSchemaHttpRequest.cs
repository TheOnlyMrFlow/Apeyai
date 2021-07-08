using Apeyai.Core.UseCases.AddTextAttributeToSchema;
using Microsoft.AspNetCore.Mvc;

namespace Apeyai.API.UseCases.AddTextAttributeToSchema
{
    public class AddTextAttributeToSchemaHttpRequest
    {
        public string SchemaName { get; set; }

        [FromBody] public string Name { get; set; }

        [FromBody] public int MinLength { get; set; }

        [FromBody] public int Maxlength { get; set; }

        [FromBody] public bool IsRequired { get; set; }

        public AddTextAttributeToSchemaRequest ToBusinessRequest()
        {
            return new AddTextAttributeToSchemaRequest()
            {
                AttributeName = Name,
                SchemaName = SchemaName,
                IsRequired = IsRequired,
                MinLength = MinLength,
                Maxlength = Maxlength
            };
        }
    }
}
