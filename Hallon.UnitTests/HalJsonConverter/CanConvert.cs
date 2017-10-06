using System;
using Xunit;

namespace Hallon.UnitTests.HalJsonConverter
{
    public class CanConvert
    {
        private readonly Hallon.HalJsonConverter sut;

        public CanConvert()
        {
            sut = new Hallon.HalJsonConverter();
        }

        [Fact]
        public void ShouldHandleNull()
        {
            Assert.False(sut.CanConvert(null));
        }    
        
        [Fact]
        public void ShouldHandleObject()
        {
            Assert.True(sut.CanConvert(typeof(object)));
        }    
    }
}