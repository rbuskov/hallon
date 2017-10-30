using Xunit;

namespace Hallon.UnitTests.LinkTableTests
{
    public class TryGet
    {
        private readonly LinkTable sut = new LinkTable();

        [Fact]
        public void KeyNotFound_ReturnFalseAndNullLink()
        {
            Assert.False(sut.TryGet("key", out var link));
            Assert.Null(link);
        }
    }
}
