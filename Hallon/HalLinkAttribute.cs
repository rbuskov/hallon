using System;

namespace Hallon
{
    // Not implemented
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class HalLinkAttribute : Attribute
    {
        private readonly string hrefPattern;

        public HalLinkAttribute(string key, string hrefPattern)
        {
            this.hrefPattern = hrefPattern;
        }
    }
}