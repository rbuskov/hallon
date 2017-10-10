using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hallon
{
    public class Link<T>
    {
        public string Href { get; set; }

        public Func<T, bool> Condition { get; set; }
    }

    public class DynamicLink<T>
    {

        public Func<T, string> Href { get; set; }

        public Func<T, bool> Condition { get; set; }
    }


    public class Resource<T>
    {
        private readonly Dictionary<string, Link<T>> links = new Dictionary<string, Link<T>>();

        public ReadOnlyDictionary<string, Link<T>> Links => new ReadOnlyDictionary<string, Link<T>>(links);

        public Resource<T> WithLink(string name, string href, Func<T, bool> condition = null)
        {
            return this;
        }

        public Resource<T> WithLink(string name, Func<T, string> href, Func<T, bool> condition = null)
        {
            return this;
        }
    }

    public class CollectionResource<T>
    {
        private readonly Dictionary<string, Link<T>> links = new Dictionary<string, Link<T>>();

        public ReadOnlyDictionary<string, Link<T>> Links => new ReadOnlyDictionary<string, Link<T>>(links);

        public CollectionResource<T> WithLink(string name, string href, Func<T, bool> condition = null)
        {
            return this;
        }

        public CollectionResource<T> WithProperty(string name, Func<IEnumerable<T>, object> value)
        {
            return this;
        }

        public CollectionResource<T> WithItemLink(string name, string href, Func<T, bool> condition = null)
        {
            return this;
        }

        public CollectionResource<T> WithItemLink(string name, Func<T, string> href, Func<T, bool> condition = null)
        {
            return this;
        }

        public CollectionResource<T> WithItemProperty(string name, Func<T, object> value)
        {
            return this;
        }
    }
}
