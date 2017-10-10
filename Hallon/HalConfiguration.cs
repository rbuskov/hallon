using System.Reflection;

namespace Hallon.Configuration
{
    public class HalConfiguration
    {

        public Resource<T> AddResource<T>()
        {
            return new Resource<T>();
        }

        public CollectionResource<T> AddCollectionResource<T>()
        {
            return new CollectionResource<T>();
        }
    }
}