using System.Collections.Generic;

namespace Hallon
{
    public interface IResource
    {
        LinkTable Links { get; }

        void AddLink(string key, string href);
    }
}