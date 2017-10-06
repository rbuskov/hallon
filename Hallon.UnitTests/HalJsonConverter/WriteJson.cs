using System;
using Newtonsoft.Json;
using Xunit;

namespace Hallon.UnitTests.HalJsonConverter
{
    public class WriteJson
    {
        private readonly Hallon.HalJsonConverter sut;

        public WriteJson()
        {
            sut = new Hallon.HalJsonConverter();
        }

        [Fact]
        public void ShouldHandleNull()
        {
            var json = JsonConvert.SerializeObject(null, Formatting.None, new Hallon.HalJsonConverter());

            Assert.Equal("null", json);
        }

        [Fact]
        public void ShouldIncludeIntegralProperties()
        {
            var obj = new { Simple = "Value" }; 
            var json = JsonConvert.SerializeObject(obj, Formatting.None, new Hallon.HalJsonConverter());

            Assert.Equal("{\"Simple\":\"Value\"}", json);
        }

        [Fact]
        public void ShouldIgnoreObjectProperties()
        {
            var obj = new { Complex = new { Property = "Value" } }; 
            var json = JsonConvert.SerializeObject(obj, Formatting.None, new Hallon.HalJsonConverter());

            Assert.Equal("{}", json);
        }

        [Fact]
        public void ShouldIgnoreArrayProperties()
        {
            var obj = new { Complex = new object[0] }; 
            var json = JsonConvert.SerializeObject(obj, Formatting.None, new Hallon.HalJsonConverter());

            Assert.Equal("{}", json);
        }

    }
}