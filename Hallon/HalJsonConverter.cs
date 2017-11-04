using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hallon
{
    public class HalJsonConverter : JsonConverter
    {
        public override bool CanRead
            => true;

        public override bool CanWrite
            => true;

        public override bool CanConvert(Type type)
            => true;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) 
            => GenerateJson(value).WriteTo(writer);

        private JToken GenerateJson(object value)
        {
            switch (value)
            {
                case IResource resource:
                    return GenerateResourceJson(resource);
                case IEnumerable<Resource> resources:
                    return GenerateResourceCollectionJson(resources);
                default:
                    return JToken.FromObject(value);
            }
        }

        private JObject GenerateResourceJson(IResource resource)
        {
            var resourceObject = JObject.FromObject(resource);

            resourceObject.Remove("Links");
            resourceObject.Add("_links", GenerateLinksJson(resource));

            return resourceObject;
        }

        private JObject GenerateResourceCollectionJson(IEnumerable<IResource> resources)
        {
            var resourceArray = new JArray();

            foreach (var resource in resources)
                resourceArray.Add(GenerateResourceJson(resource));

            return new JObject
            {
                {"_embedded", new JObject {{"resources", resourceArray}}}
            };
        }

        private JArray GenerateLinksJson(IResource resource)
        {
            var linkArray = new JArray();

            foreach (var key in resource.Links.Keys)
            {
                linkArray.Add(new JObject
                {
                    {"key", JToken.FromObject(key)},
                    {"href", JToken.FromObject(resource.Links[key].Href)}
                });
            }

            return linkArray;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}