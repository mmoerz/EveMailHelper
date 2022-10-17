using EveMailHelper.ChatLogParser;

using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;

using Shouldly;

using System.Linq;

using Xunit;

namespace EveMailHelper.Test.ChatLogParser
{
    public partial class RegexChannelName
    {
        public RegexChannelName()
        {

        }

        [Theory]
        [InlineData("   Channel Name: /no", "/no")]
        [InlineData("          Channel Name:    /noman", "/noman")]
        public void Test1(string input, string expected)
        {
            var sut = ParserRegexes.channelName.Match(input);
            sut.Success.ShouldBeTrue();
            sut.Groups["channelname"].Value.ShouldBe(expected);
        }

        [Theory]
        [InlineData("Channel Name: /nono")]
        [InlineData("   Channel name: /nono")]
        public void Test2Fail(string input)
        {
            var sut = ParserRegexes.channelName.Match(input);
            sut.Success.ShouldBeFalse();
        }
    }
}
