using System;

namespace Hallon.Demo.Models
{
    [HalLink("self", "api/orders/{id}")] // parse in Id property when rendering 
    public class Order : Resource
    {
        public int Id { get; set; }


        public DateTime Date { get; set; }
    }
}