using Apeyai.Core.Common;

namespace Apeyai.Core.Entities
{
    public class Schema : IIdentifiable
    {
        public int Id { get; }
        
        public TextAttribute[] TextAttributes { get; set; }

        public BooleanAttribute[] BooleanAttributes { get; set; }
    }
}
