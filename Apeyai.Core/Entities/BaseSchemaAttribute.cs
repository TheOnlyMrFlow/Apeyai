using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apeyai.Core.Exceptions;

namespace Apeyai.Core.Entities
{
    public abstract class BaseSchemaAttribute
    {
        public string Name { get; set; }

        public bool IsRequired { get; set; }

        public virtual void AssertValidity()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new TextAttributeNameIsNullOrWhitespacesException();
        }
    }
}
