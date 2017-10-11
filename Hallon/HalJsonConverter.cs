using System;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hallon
{
    public class HalJsonConverter : JsonConverter
    {
        public override bool CanRead => false;
        public override bool CanWrite => true;

        public override bool CanConvert(Type type) => type != null;

        /*  Scenarios, based on the type of value passed to the converter:   
         * 
         *  - POCO: Do what you can...
         *  - Resource: Include links, embedded resources
         *  - Enumerable of POCO: Do what you can...
         *  - Enumerable of Resource: Include links, embedded resources
         */

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            switch (JToken.FromObject(value))
            {
                case JObject obj:
                    WriteObject(obj);
                    break;
                case JArray array:
                    WriteArray(array);
                    break;
                default:
                    throw new InvalidOperationException($"HalJsonConverter can't serialize '{value}' to JSON.");
            }

            void WriteObject(JObject source)
            {
                var target = new JObject();

                AddLinks(target);

                foreach (var property in source.Properties())
                {
                    if (property.Value.Type != JTokenType.Object && property.Value.Type != JTokenType.Array)
                        target.Add(property);
                }

                target.WriteTo(writer);
            }

            void WriteArray(JArray array)
            {
                var resource = new JObject();
                var embedded = new JObject();
                var items = new JArray();
                var name = HttpContext.Current.Request.Url.LocalPath.Split('/').Last();

                AddLinks(resource);

                foreach (var source in array)
                {
                    if (source.Type == JTokenType.Object)
                    {
                        var target = new JObject();

                        foreach (var property in ((JObject)source).Properties())
                        {
                            if (property.Value.Type != JTokenType.Object && property.Value.Type != JTokenType.Array)
                                target.Add(property);
                        }
                        items.Add(target);
                    }
                }
                embedded.Add(name, items);
                resource.Add("_embedded", embedded);
                resource.WriteTo(writer);
            }

            void AddLinks(JObject resource)
            {
                var links = new JObject();
                var self = new JObject();

                self.Add("href", HttpContext.Current.Request.Url.PathAndQuery);
                links.Add("self", self);
                resource.Add("_links", links);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new InvalidOperationException("HalJsonConverter cannot read, only write.");
        }
    }
}