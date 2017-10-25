using System.Collections.Generic;

namespace Hallon.Demo.Data
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Order> Orders { get; } = new List<Order>();
    }
}