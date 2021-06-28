using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apeyai.Core.Exceptions;

namespace Apeyai.Core.Entities
{
    public class TextAttribute: BaseSchemaAttribute
    {
        public int MinLength { get; set; }
        public int MaxLength { get; set; }

        public override void AssertValidity()
        {
            base.AssertValidity();

            if (MinLength < 0)
                throw new TextAttributesMinLengthLowerThanZeroException();
            if (MinLength > MaxLength)
                throw new TextAttributesMinLengthHigherThanMaxLengthException();
        }
    }
}
