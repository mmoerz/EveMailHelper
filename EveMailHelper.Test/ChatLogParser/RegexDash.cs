using EveMailHelper.ChatLogParser;

using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;

using Shouldly;

using System.Linq;

using Xunit;

namespace EveMailHelper.Test.ChatLogParser
{
    public partial class RegexDash
    {
        public RegexDash()
        {

        }

        [Theory]
        [InlineData("   ----------", "----------")]
        [InlineData("        ---------------------------------------------------------------",
            "---------------------------------------------------------------")]
        public void Test1(string input, string expected)
        {
            var sut = ParserRegexes.dashLine.Match(input);
            sut.Success.ShouldBeTrue();
            sut.Groups["dashes"].Value.ShouldBe(expected);
        }

        [Theory]
        [InlineData("---")]
        [InlineData("   ---.---")]
        public void Test2Fail(string input)
        {
            var sut = ParserRegexes.dashLine.Match(input);
            sut.Success.ShouldBeFalse();
        }
    }
}
