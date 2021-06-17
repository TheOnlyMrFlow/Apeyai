using Apeyai.Core.Common;

namespace Apeyai.Core.Entities
{
    public class Schema
    {
        public string Name { get; set; }
        
        public TextAttribute[] TextAttributes { get; set; }

        public BooleanAttribute[] BooleanAttributes { get; set; }
    }
}
