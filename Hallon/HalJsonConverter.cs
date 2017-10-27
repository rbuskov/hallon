using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hallon
{
    public class HalJsonConverter : JsonConverter
    {
        public override bool CanRead => false;
        public override bool CanWrite => true;

        public override bool CanConvert(Type type) => type != null;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            /*
            var mapper = new HalMapper();
            var halResource = mapper.Map(value);
            var jObject = Convert(halResource);

            jObject.WriteTo(writer);
            */
        }

/*
private JObject Convert(HalResource halResource)
{
JObject jsonResource = new JObject();

if (halResource.Links.Count > 0)
{
    JObject jsonResourceLinks = new JObject();

    foreach (var halLink in halResource.Links)
    {
        var jsonLink = new JObject();
        jsonLink.Add("href", halLink.Href);

        jsonResourceLinks.Add(halLink.Key, jsonLink);
    }

    jsonResource.Add("_links", jsonResourceLinks);
}

if (halResource.Embedded.Count > 0)
{
    JObject jsonEmbedded = new JObject();

    foreach (var key in halResource.Embedded.Keys)
    {
        var jsonResourceArray = new JArray();

        foreach (var item in halResource.Embedded[key])
        {
            jsonResourceArray.Add(Convert(item));
        }

        jsonEmbedded.Add(key, jsonResourceArray);
    }

    jsonResource.Add("_embedded", jsonEmbedded);
}

foreach (var halProperty in halResource.Properties)
{
    jsonResource.Add(halProperty.Key, halProperty.Value);
}

return jsonResource;
}

    */

public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
{
throw new InvalidOperationException("HalJsonConverter cannot read, only write.");
}
}
}
