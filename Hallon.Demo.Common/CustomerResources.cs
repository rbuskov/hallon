namespace Hallon.Demo.Common
{
    public class CustomerSummaryResource : Resource
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class CustomerResource : CustomerSummaryResource
    {
        public Address Address { get; set; }
    }
}