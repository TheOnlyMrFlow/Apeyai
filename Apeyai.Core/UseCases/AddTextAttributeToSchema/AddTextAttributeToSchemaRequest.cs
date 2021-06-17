namespace Apeyai.Core.UseCases.AddTextAttributeToSchema
{
    public class AddTextAttributeToSchemaRequest
    {
        public string SchemaName { get; set; }

        public string AttributeName { get; set; }

        public bool IsRequired { get; set; }

        public int MinLength { get; set; }

        public int Maxlength { get; set; }
    }
}
