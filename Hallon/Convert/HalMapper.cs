using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Hallon.Convert
{
    public class HalMapper
    {
        private bool mappingRoot = true; // Hack

        public HalResource Map(object value)
        {
            if (value == null)
                throw new NotImplementedException("Can't handle null yet.");

            if (value.GetType().IsPrimitive)
                throw new NotImplementedException("Can't handle primitives yet.");

            if (value is string)
                throw new NotImplementedException("Can't handle strings yet.");

            var halResource = new HalResource();

            if (value is IEnumerable collection)
            {
                List<HalResource> embedded = new List<HalResource>();

                foreach (var item in collection)
                {
                    mappingRoot = false;
                    embedded.Add(Map(item));
                }

                var key = HttpContext.Current?.Request.Url.LocalPath.Split('/').LastOrDefault() ?? "items";

                halResource.Embedded.Add(key, embedded);
                mappingRoot = true;
                MapRootSelfLink(halResource);
            }
            else
            {
                MapProperties(halResource, value);
                MapLinks(halResource, value);
                MapRootSelfLink(halResource);
            }

            return halResource;
        }

        private void MapLinks(HalResource halResource, object value)
        {
            if (value is Resource resource)
            {
                foreach (var link in resource.Links)
                    halResource.Links.Add(new HalLink { Key = link.Key, Href = link.Href});
            }
        }

        private void MapRootSelfLink(HalResource root)
        {
            if (mappingRoot && root.Links.All(l => l.Key.ToLower() != "self"))
            {
                var uri = HttpContext.Current?.Request.Url.LocalPath;
                var selfLink = new HalLink { Key = "self", Href = uri };

                root.Links.Insert(0, selfLink);
            }
        }

        private void MapProperties(HalResource resource, object obj)
        {
            foreach (var propertyInfo in obj.GetType().GetProperties())
            {
                if (TryMapPropetyValue(propertyInfo, obj, out var value))
                {
                    var halProperty = new HalProperty
                    {
                        Key = propertyInfo.Name,
                        Value = value
                    };

                    resource.Properties.Add(halProperty);
                }
            }
        }

        private bool TryMapPropetyValue(PropertyInfo propertyInfo, object obj, out string value)
        {
            switch (propertyInfo.GetValue(obj))
            {
                case string s:
                    value = s;
                    return true;
                case DateTime dt:
                    value = dt.ToString(CultureInfo.InvariantCulture.DateTimeFormat);
                    return true;
                case TimeSpan ts:
                    value = ts.ToString();
                    return true;
                case Guid g:
                    value = g.ToString();
                    return true;
                case var p when propertyInfo.PropertyType.IsPrimitive:
                    value = p.ToString();
                    return true;
                default:
                    value = null;
                    return false;
            }            
        }
    }
}
