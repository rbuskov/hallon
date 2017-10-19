using System;

namespace Hallon
{
    public class Resource : IResource
    {
        public LinkTable Links { get; } = new LinkTable();

        public void AddLink(string key, string href) 
            => Links[key] = new Link(href);
    }

    public class LinkTable
    {
        public Link this[string key]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public bool TryGet(string key, out Link link)
        {
            throw new NotImplementedException();
        }
    }
}
