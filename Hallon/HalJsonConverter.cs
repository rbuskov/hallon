using System;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Hallon
{
    public class HalJsonConverter : JsonConverter
    {
        public override bool CanRead => false;
        public override bool CanWrite => true;

        public override bool CanConvert(Type type) => type != null;

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

                foreach (var property in source.Properties())
                {
                    if (property.Value.Type != JTokenType.Object && property.Value.Type != JTokenType.Array)
                        target.Add(property);
                }

                target.WriteTo(writer);
            }

            void WriteArray(JArray array)
            {
                throw new NotImplementedException();
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new InvalidOperationException("HalJsonConverter cannot read, only write.");
        }
    }
}