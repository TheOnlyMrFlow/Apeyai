using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Apeyai.Core.Entities.Attributes;

namespace Apeyai.Core.Entities.ApiEndpoints
{
    public class GetSingleItemEndpoint
    {
        public GetSingleItemEndpointRootSegment RootSegment { get; set; }

        private IEnumerable<GetSingleItemEndpointSegment> AllSegments
        {
            get
            {
                GetSingleItemEndpointSegment segment = RootSegment;
                yield return segment;

                while (segment.HasNext)
                {
                    segment = segment.Next;
                    yield return segment.Next;
                }

            }
        }

        public void Validate()
        {
            foreach (var segment in AllSegments)
                segment.Validate();
        }

        public override string ToString()
            => string.Join("", AllSegments.Select(segment => segment.ToString()));
    }
}
