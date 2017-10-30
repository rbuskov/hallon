using Xunit;

namespace Hallon.UnitTests.LinkTableTests
{
    public class StringIndexer
    {
        private readonly LinkTable sut = new LinkTable();

        [Fact]
        public void EmptyTable_ShouldSetAndGetLink()
        {
            sut["key"] = new Link("href");
            Assert.Equal("href", sut["key"].Href);
        }

        [Fact]
        public void Get_KeyMissing_ThrowException()
        {
            Assert.Throws<KeyNotFoundException>(()=> sut["key"]);
        }

        [Fact]
        public void Set_KeyAlreadyExsists_OverwriteLink()
        {
            sut["key"] = new Link("first");
            sut["key"] = new Link("second");

            Assert.Equal("second", sut["key"].Href);        
        }
    }
}
