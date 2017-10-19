using System.Collections.Generic;
using Hallon.Demo.Resources;

namespace Hallon.Demo.Data
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Order> Orders { get; } = new List<Order>();

        public CustomerResource ToResource()
        {
            throw new System.NotImplementedException();
        }
    }
}