namespace Apeyai.Core.UseCases.TextAttributes.Create
{
    public class CreateTextAttributeRequest
    {
        public int SchemaId { get; set; }

        public string AttributeName { get; set; }

        public bool IsRequired { get; set; }

        public int MinLength { get; set; }

        public int Maxlength { get; set; }
    }
}
