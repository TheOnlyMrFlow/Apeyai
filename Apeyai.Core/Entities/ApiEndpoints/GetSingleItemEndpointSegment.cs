using System;
using System.Linq;
using Apeyai.Core.Entities.Attributes;

namespace Apeyai.Core.Entities.ApiEndpoints
{
    public abstract class GetSingleItemEndpointSegment
    {
        public GetSingleItemEndpointSegment Next { get; set; }
        public bool HasNext => Next is not null;
        public BaseAttribute IdentifierAttribute { get; set; }
        public abstract Schema Schema { get; }
        public abstract string SegmentName { get; }

        public virtual void Validate()
        {
            if (! Schema.AllAttributes.Contains(IdentifierAttribute))
                throw new Exception("Ton identifier attribute existe pas sorry frr");

            if (! IdentifierAttribute.IsUnique)
                throw new Exception("Identifier attribute must be unique");
        }

        public override string ToString() => $"/{SegmentName}/{{{IdentifierAttribute.Name}}}";
    }
}