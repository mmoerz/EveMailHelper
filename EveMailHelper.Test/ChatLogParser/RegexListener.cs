using EveMailHelper.ChatLogParser;

using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;

using Shouldly;

using System.Linq;

using Xunit;

namespace EveMailHelper.Test.ChatLogParser
{
    public partial class RegexListener
    {
        public RegexListener()
        {

        }

        [Theory]
        [InlineData("   Listener: no", "no")]
        [InlineData("          Listener:        H'un'ta Huunt", "H'un'ta Huunt")]
        public void Test1(string input, string expected)
        {
            var sut = ParserRegexes.Listener.Match(input);
            sut.Success.ShouldBeTrue();
            sut.Groups["listener"].Value.ShouldBe(expected);
        }

        [Theory]
        [InlineData("Listener: nono")]
        [InlineData("   Listener: /nono")]
        public void Test2Fail(string input)
        {
            var sut = ParserRegexes.Listener.Match(input);
            sut.Success.ShouldBeFalse();
        }
    }
}
