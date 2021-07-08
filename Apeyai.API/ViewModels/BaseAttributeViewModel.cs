using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apeyai.Core.Entities;

namespace Apeyai.API.ViewModels
{
    public enum EAttributeType
    {
        Boolean,
        Text
    }

    public abstract class BaseAttributeViewModel
    {
        public abstract EAttributeType AttributeType { get; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
    }
}
