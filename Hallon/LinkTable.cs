using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Hallon
{
    public class LinkTable : IEnumerable<Link>
    {
        private readonly OrderedDictionary innerDictionary = new OrderedDictionary();

        public Link this[string key]
        {
            get => innerDictionary[key] as Link;
            set => innerDictionary[key] = value;
        }

        public Link this[int index]
            => innerDictionary[index] as Link;

        public bool ContainsKey(string key)
            => innerDictionary.Contains(key);

        public IEnumerable<string> Keys 
            => innerDictionary.Keys.Cast<string>();

        public IEnumerable<Link> Values
            => innerDictionary.Keys.Cast<Link>();

        public IEnumerator<Link> GetEnumerator()
            => innerDictionary.Values.Cast<Link>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public int Count => innerDictionary.Count;

        public bool TryGet(string key, out Link link)
        {
            link = ContainsKey(key)
                ? this[key]
                : null;

            return link != null;
        }

        public void Insert(int index, string key, Link link) 
            => innerDictionary.Insert(index, key, link);

        public void Insert(int index, string key, string href)
            => innerDictionary.Insert(index, key, new Link(href));

        public void Add(string key, Link link)
            => innerDictionary.Add(key, link);

        public void Add(string key, string href)
            => innerDictionary.Add(key, new Link(href));

        public void AddSelf(string href)
            => Insert(0, "self", new Link(href));

        public void Remove(string key)
            => innerDictionary.Remove(key);

        public void RemoveAt(int index)
            => innerDictionary.RemoveAt(index);

        public void Clear() 
            => innerDictionary.Clear();
    }
}