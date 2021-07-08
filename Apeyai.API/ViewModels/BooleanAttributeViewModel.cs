using Apeyai.Core.Entities.Attributes;

namespace Apeyai.API.ViewModels
{
    public class BooleanAttributeViewModel: BaseAttributeViewModel
    {
        public override EAttributeType AttributeType => EAttributeType.Boolean;

        public static BooleanAttributeViewModel FromEntity(BooleanAttribute boolAttributeEntity)
        {
            return new BooleanAttributeViewModel()
            {
                IsRequired = boolAttributeEntity.IsRequired,
                Name = boolAttributeEntity.Name
            };
        }

    }
}
