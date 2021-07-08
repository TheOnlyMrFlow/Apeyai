using Apeyai.Core.Entities;
using Apeyai.Core.Entities.Attributes;

namespace Apeyai.API.ViewModels
{
    public class TextAttributeViewModel: BaseAttributeViewModel
    {
        public override EAttributeType AttributeType => EAttributeType.Text;
        public int MinLength { get; set; }
        public int MaxLength { get; set; }

        public static TextAttributeViewModel FromEntity(TextAttribute textAttributeEntity)
        {
            return new TextAttributeViewModel()
            {
                IsRequired = textAttributeEntity.IsRequired,
                Name = textAttributeEntity.Name,
                MinLength = textAttributeEntity.MinLength,
                MaxLength = textAttributeEntity.MaxLength
            };
        }

    }
}
