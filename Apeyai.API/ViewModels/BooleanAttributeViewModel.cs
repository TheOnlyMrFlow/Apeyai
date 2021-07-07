using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apeyai.Core.Entities;

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
