using System.Collections.Generic;

namespace Hallon.Convert
{
    public class HalResource
    {
        public List<HalLink> Links { get; } = new List<HalLink>();

        public List<HalProperty> Properties { get; } = new List<HalProperty>();

        public Dictionary<string, List<HalResource>> Embedded { get; } = new Dictionary<string, List<HalResource>>();
    }
}
