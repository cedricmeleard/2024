using FluentAssertions;
using Xunit;

namespace EID.Tests
{
    public class EID_Should
    {
        [Theory]
        [InlineData(null, "it is null or whitespace")]
        [InlineData("", "it is null or whitespace")]
        [InlineData("        ", "it is null or whitespace")]
        [InlineData("1234g567", "it is not composed of digits only")]
        [InlineData("123456789", "it doesn't match the required size")]
        [InlineData("1234567", "it doesn't match the required size")]
        [InlineData("00001648", "it doesn't start with 1-3")]
        [InlineData("10000064", "SN can't be 000")]
        [InlineData("12345670", "Validation isn't correct")]
        public void Not_Be_Valid(string? eid, string because)
        {
            var notAnEid = EID.Parse(eid);
            
            notAnEid.IsLeft.Should().BeTrue();
            notAnEid.IfLeft(x => x.Message.Should().Be(because));

        }
        
        [Theory]
        [InlineData("12345625")]
        [InlineData("30000120")]
        [InlineData("19800767")]
        [InlineData("19845606")]
        [InlineData("30600233")]
        [InlineData("29999922")]
        [InlineData("11111151")]
        public void Be_Valid(string? eid)
        {
            var aValidEid = EID.Parse(eid);
            
            aValidEid.IsRight.Should().BeTrue();
            aValidEid.IfRight(x => x.Should().Be(eid));
        }
    }
}