using System;

namespace Hallon
{
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

        public void Add(string key, string href)
            => this[key] = new Link(href);

        public void AddSelf(string s)
        {
            throw new NotImplementedException();
        }
    }
}