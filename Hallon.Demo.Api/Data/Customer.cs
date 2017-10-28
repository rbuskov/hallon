using System.Collections.Generic;
using Hallon.Demo.Common;

namespace Hallon.Demo.Data
{
    public class Customer : DemoEntity
    {
        public string Name { get; set; }

        public Address Address { get; set; }

        public List<Order> Orders { get; } = new List<Order>();
    }
}