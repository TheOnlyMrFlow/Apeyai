using Apeyai.Core.UseCases.AddForeignSchemaReferenceAttributeToSchema;
using Apeyai.Core.UseCases.AddTextAttributeToSchema;
using Microsoft.AspNetCore.Mvc;

namespace Apeyai.API.UseCases.AddForeignSchemaReferenceAttributeToSchema
{
    public class AddForeignSchemaReferenceAttributeToSchemaHttpRequest
    {
        public string SchemaName { get; set; }

        [FromBody] public string Name { get; set; }

        [FromBody] public bool IsRequired { get; set; }

        [FromBody] public string ForeignSchemaName { get; set; }


        public AddForeignSchemaReferenceAttributeToSchemaRequest ToBusinessRequest()
        {
            return new AddForeignSchemaReferenceAttributeToSchemaRequest()
            {
                AttributeName = Name,
                SchemaName = SchemaName,
                IsRequired = IsRequired,
                ForeignSchemaName = ForeignSchemaName
            };
        }
    }
}
