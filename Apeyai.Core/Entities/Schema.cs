using System.Collections.Generic;
using Apeyai.Core.Common;
using Apeyai.Core.Exceptions;

namespace Apeyai.Core.Entities
{
    public class Schema
    {
        public string Name { get; set; }
        
        public List<TextAttribute> TextAttributes { get; set; }

        public List<BooleanAttribute> BooleanAttributes { get; set; }

        public void AssertValidity()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new SchemaNameIsNullOrWhitespacesException();
        }
    }
}
