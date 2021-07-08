using Apeyai.Core.Exceptions;

namespace Apeyai.Core.Entities.Attributes
{
    public abstract class BaseAttribute
    {
        public string Name { get; set; }

        public bool IsRequired { get; set; }

        public bool IsCollection { get; set; }

        public bool IsUnique { get; set; }

        public virtual void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new AttributeNameIsNullOrWhitespacesException();
        }
    }
}
