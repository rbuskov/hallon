using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Hallon.Client;
using Hallon.Demo.Resources;

namespace Hallon.Demo.Client
{
    public class DemoClient
    {
        public DemoClient()
        {
            var client = new HalClient("http://example.org/api");

            // check status code (good idea when link is hardcoded)
            if (client.Get<Order>("/orders/1", out var order) == HttpStatusCode.OK)
            {
                // order. ...
            }

            // trust that the resource is there (will throw an exception if link is not found)
            var customer = client.Get<Customer>(order, "customer");
        }
    }
}
