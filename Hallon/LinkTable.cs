using System;
using System.Collections.Generic;

namespace Hallon
{
    public class LinkTable
    {
        private readonly Dictionary<string, Link> innerDictionary = new Dictionary<string, Link>();

        public Link this[string key]
        {
            get => innerDictionary.ContainsKey(key) 
                ? innerDictionary[key] 
                : throw new KeyNotFoundException();

            set => innerDictionary[key] = value;
        }

        public bool TryGet(string key, out Link link)
        {
            link = null;
            return false;
        }

        public void Add(string key, string href)
            => this[key] = new Link(href);

        public void AddSelf(string s)
        {
            throw new NotImplementedException();
        }
    }

    public class KeyNotFoundException : Exception
    {
        public KeyNotFoundException()
            : base("The given was not present in the link table.")
        { }
    }
}