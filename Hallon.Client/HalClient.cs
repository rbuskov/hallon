using System;
using System.Net;

namespace Hallon.Client
{
    public class HalClient
    {
        public bool CanResolve(Resource resource, string link)
        {
            return false;
        }

        public HalResponse<T> Delete<T>(Resource resource, string link)
        {
            return null;
        }

        public HttpStatusCode Delete(Resource resource, string link)
        {
            return HttpStatusCode.Accepted;
        }

        public bool Delete<T>(Resource resource, Enum link, out HalResponse<T> response)
        {
            response = null;
            return false;
        }

        public bool Delete<T>(Resource resource, string link, out HalResponse<T> response)
        {
            response = null;
            return false;
        }

        public bool Delete(Resource resource, string link, out HttpStatusCode status)
        {
            status = HttpStatusCode.Accepted;
            return false;
        }
    }

    public class HalResponse<T>
    {
        public HttpStatusCode StatusCode;
        public T Contents;
    }

    public enum OrderLink
    {
        Delete = 1
    }

    public class Order : Resource
    {
        public const string Delete = "delete";
    }

    public class ClientLink : Link
    {
        public ClientLink(string key, string href) : base(key, href)
        {
        }

        public ClientLink(Enum key, string href) : base(key.ToString(), href)
        {
        }

    }

    public class Example
    {
        public Example()
        {
            var order = new Order();
            var link = new ClientLink("","");
            var client = new HalClient();

            if (client.Delete<Order>(order, Order.Delete, out var response))
            {
                var newOrder = response.Contents;
            }

            var status = client.Delete(order, "");
        }
    }
}