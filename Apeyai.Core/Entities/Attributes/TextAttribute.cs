using Apeyai.Core.Exceptions;

namespace Apeyai.Core.Entities.Attributes
{
    public class TextAttribute: BaseAttribute
    {
        public int MinLength { get; set; }
        public int MaxLength { get; set; }

        public override void Validate()
        {
            base.Validate();

            if (MinLength < 0)
                throw new TextAttributesMinLengthLowerThanZeroException();
            if (MinLength > MaxLength)
                throw new TextAttributesMinLengthHigherThanMaxLengthException();
        }
    }
}
