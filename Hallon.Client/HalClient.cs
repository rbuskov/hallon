using System;
using System.Net;

namespace Hallon.Client
{
    public class HalClient
    {
        private string baseUrl;

        public HalClient(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public bool CanResolve(Resource resource, string link)
        {
            return false;
        }

        public HttpStatusCode Get<T>(string url, out T resource)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(Resource resource, string link)
        {
            throw new NotImplementedException();
        }

        
        public T Get<T>(string url)
        {
            throw new NotImplementedException();
        }
    }
}