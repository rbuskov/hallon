using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Hallon
{
    public class HalMediaTypeFormatter : JsonMediaTypeFormatter
    {
        public const string HalMediaType = "application/hal+json";

        public HalMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(HalMediaType));

            SerializerSettings.Converters.Add(new HalJsonConverter());

            SerializerSettings.Formatting = Formatting.Indented;
            SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
        }

        public override bool CanReadType(Type type) 
           => true;

        public override bool CanWriteType(Type type)
            => true;
    }
}