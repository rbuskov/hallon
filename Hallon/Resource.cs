using System.Collections.Generic;

namespace Hallon
{
    public class Link
    {
        public string Href { get; set; }    
    }

    public interface IEmbeddable
    {
    }

    public interface IResource : IEmbeddable
    {
        IReadOnlyDictionary<string, Link> Links { get; }

        IReadOnlyDictionary<string, IEmbeddable> Embedded { get; }

        void AddLink(string key, string href);

        void AddEmbedded(string key, IEmbeddable resourceObjectOrCollection);
    }

    public interface IResourceList<T> : IList<T>, IEmbeddable where T : Resource
    {
    }

    public class Resource : IResource
    {
        private readonly Dictionary<string, Link> links = new Dictionary<string, Link>();
        private readonly Dictionary<string, IEmbeddable> embedded = new Dictionary<string, IEmbeddable>();

        public IReadOnlyDictionary<string, Link> Links => links;

        public IReadOnlyDictionary<string, IEmbeddable> Embedded => embedded;

        public void AddLink(string key, string href)
        {
            links.Add(key, new Link {Href = href});
        }

        public void AddEmbedded(string key, IEmbeddable resourceObjectOrCollection)
        {
            embedded.Add(key, resourceObjectOrCollection);
        }
    }

    public class ResourceList<T> : List<T>, IResourceList<T> where T : Resource
    {
        public ResourceList()
        {
        }

        public ResourceList(IEnumerable<T> resources) : base(resources)
        {
        }
    }
}
