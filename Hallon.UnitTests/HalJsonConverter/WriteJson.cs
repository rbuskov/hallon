using Newtonsoft.Json;
using Xunit;

namespace Hallon.UnitTests.HalJsonConverter
{
    public class WriteJson
    {
        private readonly Convert.HalJsonConverter sut;

        public WriteJson()
        {
            sut = new Convert.HalJsonConverter();
        }

        [Fact]
        public void ShouldHandleNull()
        {
            var json = JsonConvert.SerializeObject(null, Formatting.None, new Convert.HalJsonConverter());

            Assert.Equal("null", json);
        }
    }
}