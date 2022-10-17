using EveMailHelper.ChatLogParser;

using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;

using Shouldly;

using System.Linq;

using Xunit;

namespace EveMailHelper.Test.ChatLogParser
{
    public partial class RegexChannelId
    {
        public RegexChannelId()
        {

        }

        [Theory]
        [InlineData("   Channel ID: nono_4b3a3f3553ec", "nono_4b3a3f3553ec")]
        [InlineData("          Channel ID:      player_5bd7399ee27711ec80bc3640b5e703eb",
            "player_5bd7399ee27711ec80bc3640b5e703eb")]
        public void Test1(string input, string expected)
        {
            var sut = ParserRegexes.channelId.Match(input);
            sut.Success.ShouldBeTrue();
            sut.Groups["channelid"].Value.ShouldBe(expected);
        }

        [Theory]
        [InlineData("Channel ID: nono_4b3a3f3553ec")]
        [InlineData("   Channel id: nono_4b3a3f3553ec")]
        [InlineData("   Channel ID: nono_4g")]
        public void Test2Fail(string input)
        {
            var sut = ParserRegexes.channelId.Match(input);
            sut.Success.ShouldBeFalse();
        }
    }
}
