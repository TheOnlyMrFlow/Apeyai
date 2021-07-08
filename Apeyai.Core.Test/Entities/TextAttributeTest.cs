using Apeyai.Core.Entities;
using Apeyai.Core.Exceptions;
using FluentAssertions;
using Xunit;

namespace Apeyai.Core.Test.Entities
{
    public class TextAttributeTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("    ")]
        [InlineData(null)]
        public void assert_validity_should_throw_if_name_is_null_or_whitespaces(string name)
        {
            var textAttribute = new TextAttribute()
            {
                Name = name,
                IsRequired = true,
                MinLength = 5,
                MaxLength = 10
            };

            textAttribute
                .Invoking(textAttr => textAttribute.AssertValidity())
                .Should().Throw<TextAttributeNameIsNullOrWhitespacesException>();
        }

        [Fact]
        public void assert_validity_should_throw_if_min_length_lower_than_zero()
        {
            var textAttribute = new TextAttribute()
            {
                Name = "Toto",
                IsRequired = true,
                MinLength = -5,
                MaxLength = 10
            };

            textAttribute
                .Invoking(textAttr => textAttribute.AssertValidity())
                .Should().Throw<TextAttributesMinLengthLowerThanZeroException>();
        }

        [Fact]
        public void assert_validity_should_throw_if_max_length_lower_than_min_length()
        {
            var textAttribute = new TextAttribute()
            {
                Name = "Toto",
                IsRequired = true,
                MinLength = 15,
                MaxLength = 10
            };

            textAttribute
                .Invoking(textAttr => textAttr.AssertValidity())
                .Should().Throw<TextAttributesMinLengthHigherThanMaxLengthException>();
        }

        [Fact]
        public void assert_validity_should_not_throw_if_max_length_equals_min_length()
        {
            var textAttribute = new TextAttribute()
            {
                Name = "Toto",
                IsRequired = true,
                MinLength = 15,
                MaxLength = 15
            };

            textAttribute
                .Invoking(textAttr => textAttr.AssertValidity())
                .Should().NotThrow();
        }
    } 
}
