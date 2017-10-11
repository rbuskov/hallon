using System.Collections.Generic;

namespace Hallon
{
    public interface IResource
    {
        IReadOnlyList<Link> Links { get; }

        void AddLink(string key, string href);
    }
}