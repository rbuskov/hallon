using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Hallon.Configuration;
using Newtonsoft.Json;

namespace Hallon
{
    public class HalMediaTypeFormatter : JsonMediaTypeFormatter
    {
        private readonly HalConfiguration configuration;

        public const string HalMediaType = "application/hal+json";

        public HalMediaTypeFormatter(HalConfiguration configuration)
        {
            this.configuration = configuration;
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(HalMediaType));

            SerializerSettings.Converters.Add(new HalJsonConverter());

            SerializerSettings.Formatting = Formatting.Indented;
            SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            return type != null;
        }
    }
}