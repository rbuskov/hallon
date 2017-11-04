using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xunit;

namespace Hallon.UnitTests.HalJsonConverterTests
{
    public class WriteJson
    {
        private readonly HalJsonConverter sut;

        public WriteJson()
        {
            sut = new HalJsonConverter();
        }

        [Fact]
        public void ShouldHandleNullValues()
        {
            var expected = "null";
            var actual = JsonConvert.SerializeObject(null, Formatting.None, sut);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldHandleIntValues()
        {
            var expected = "1";
            var actual = JsonConvert.SerializeObject(1, Formatting.None, sut);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldHandleStringValues()
        {
            var expected = "\"test\"";
            var actual = JsonConvert.SerializeObject("test", Formatting.None, sut);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldHandleDateTimeValues()
        {
            var expected = "\"1970-10-11T00:00:00\"";
            var actual = JsonConvert.SerializeObject(new DateTime(1970, 10, 11), Formatting.None, sut);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldHandlePocos()
        {
            var poco = new TestPoco { Name = "test" };

            var expected = "{\"Name\":\"test\"}";
            var actual = JsonConvert.SerializeObject(poco, Formatting.None, sut);

            Assert.Equal(expected, actual);
        }

        public void ShouldIgnoreNestedPocos()
        {
            var poco = new DeepPoco
            {
                Name = "test",
                Property = new TestPoco(),
                Collection = new List<TestPoco> { new TestPoco() }
            };

            var expected = "{\"Name\":\"test\"}";
            var actual = JsonConvert.SerializeObject(poco, Formatting.None, sut);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldHandleResources()
        {
            var resource = new TestResource() { Name = "test" };
            
            resource.Links.AddSelf("/self");
            resource.Links.Add("something", "/something");
            resource.Links.Insert(1, "middle", "/middle");

            var expected = "{\"Name\":\"test\",\"_links\":[{\"key\":\"self\",\"href\":\"/self\"},{\"key\":\"middle\",\"href\":\"/middle\"},{\"key\":\"something\",\"href\":\"/something\"}]}";
            var actual = JsonConvert.SerializeObject(resource, Formatting.None, sut);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldHandleCollectionOfResources()
        {
            var resources = new List<TestResource>
            {
                new TestResource { Name = "One" },
                new TestResource { Name = "Two" }
            };

            resources.ForEach(r => r.Links.AddSelf($"/{r.Name}"));

            var expected = "{\"_embedded\":{\"resources\":[{\"Name\":\"One\",\"_links\":[{\"key\":\"self\",\"href\":\"/One\"}]},{\"Name\":\"Two\",\"_links\":[{\"key\":\"self\",\"href\":\"/Two\"}]}]}}";
            var actual = JsonConvert.SerializeObject(resources, Formatting.None, sut);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldHandleNestedResources()
        {
            var resource = new DeepResource
            {
                Property = new TestResource(),
                Collection = new List<TestResource>
                {
                    new TestResource {Name = "One"},
                    new TestResource {Name = "Two"}
                }
            };

            resource.Collection.ForEach(r => r.Links.AddSelf($"/{r.Name}"));

            // Todo: Keys are missing in links
            var expected = "{\"Property\":{\"Name\":null,\"Links\":[]},\"Collection\":[{\"Name\":\"One\",\"Links\":[{\"Href\":\"/One\"}]},{\"Name\":\"Two\",\"Links\":[{\"Href\":\"/Two\"}]}],\"_links\":[]}";
            var actual = JsonConvert.SerializeObject(resource, Formatting.None, sut);

            Assert.Equal(expected, actual);
        }

        public class TestPoco
        {
            public string Name { get; set; }
        }

        public class DeepPoco
        {
            public string Name { get; set; }

            public TestPoco Property { get; set; }            

            public List<TestPoco> Collection { get; set; }
        }

        public class TestResource : Resource
        {
            public string Name { get; set; }
        }

        public class DeepResource : Resource
        {
            public TestResource Property { get; set; }

            public List<TestResource> Collection { get; set; }
        }

        // Todo: Deep poco
    }
}