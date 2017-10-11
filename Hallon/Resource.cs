using System.Collections.Generic;

namespace Hallon
{
    public class Resource : IResource
    {
        private readonly List<Link> links = new List<Link>();

        public IReadOnlyList<Link> Links => links.AsReadOnly();

        public void AddLink(string key, string href)
        {
            links.Add(new Link(key, href));
        }
    }
}
