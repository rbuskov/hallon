namespace Hallon
{
    public class Link
    {
        public Link(string key, string href)
        {
            Key = key;
            Href = href;
        }

        public string Key { get; }
        public string Href { get; }    
    }
}