using System;
using System.Collections.Generic;

namespace Hallon.Demo.Data
{
    public static class Repository
    {
        public static List<Customer> Customers { get; } = new List<Customer>();

        public static List<Product> Products { get; }  = new List<Product>();

        public static List<Order> Orders { get; } = new List<Order>();

        public static List<OrderLine> OrderLines { get; } = new List<OrderLine>();

        static Repository()
        {
            Customers.Add(new Customer { Id = 1, Name = "Acme, Inc"});
            Customers.Add(new Customer { Id = 2, Name = "Secondhand Submaries Ltd." });

            Products.Add(new Product { Id = 1, Name = "Blue Widget", Price = 34.95M});
            Products.Add(new Product { Id = 1, Name = "Red Widget", Price = 29.95M });
            Products.Add(new Product { Id = 1, Name = "Yellow Widget", Price = 33.95M });

            Orders.Add(new Order { Id = 1, Customer = Customers[0], Date = new DateTime(2017, 1, 10)});
            Orders.Add(new Order { Id = 2, Customer = Customers[0], Date = new DateTime(2017, 2, 10) });
            Orders.Add(new Order { Id = 3, Customer = Customers[1], Date = new DateTime(2017, 3, 10) });

            Customers[0].Orders.Add(Orders[0]);
            Customers[0].Orders.Add(Orders[1]);
            Customers[1].Orders.Add(Orders[2]);

            OrderLines.Add(new OrderLine { Id = 1, Order = Orders[0], Product = Products[0], Quantity = 1 });
            OrderLines.Add(new OrderLine { Id = 2, Order = Orders[0], Product = Products[1], Quantity = 2 });
            OrderLines.Add(new OrderLine { Id = 3, Order = Orders[1], Product = Products[2], Quantity = 3 });
            OrderLines.Add(new OrderLine { Id = 4, Order = Orders[1], Product = Products[0], Quantity = 4 });
            OrderLines.Add(new OrderLine { Id = 5, Order = Orders[2], Product = Products[1], Quantity = 5 });
            OrderLines.Add(new OrderLine { Id = 6, Order = Orders[2], Product = Products[2], Quantity = 6 });
            OrderLines.Add(new OrderLine { Id = 7, Order = Orders[2], Product = Products[0], Quantity = 7 });

            Orders[0].Lines.Add(OrderLines[0]);
            Orders[0].Lines.Add(OrderLines[1]);
            Orders[1].Lines.Add(OrderLines[2]);
            Orders[1].Lines.Add(OrderLines[3]);
            Orders[2].Lines.Add(OrderLines[4]);
            Orders[2].Lines.Add(OrderLines[5]);
            Orders[2].Lines.Add(OrderLines[6]);
        }
    }
}
