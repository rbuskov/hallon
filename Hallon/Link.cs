namespace Hallon
{
    public class Link
    {
        public Link(string href)
        {
            Href = href;
        }

        public Link(string key, string href)
        {
            Href = href;
        }

        public string Href { get; }    
    }
}