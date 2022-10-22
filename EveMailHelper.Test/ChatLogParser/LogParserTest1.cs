using EveMailHelper.ChatLogParser;

using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;

using Shouldly;

using System.IO;
using System.Linq;

using Xunit;

namespace EveMailHelper.Test.ChatLogParser
{
    public partial class LogParserTest1
    {
        public LogParserTest1()
        {

        }

        [Theory]
        [InlineData("Data/ChatLogParser/onlyheader.txt")]
        public void Test1(string fileName)
        {
            var sut = new EveMailHelper.ChatLogParser.ChatLogParser();
            sut.ParseFile(fileName);
        }

        // Todo: check all entries of the file
        [Theory]
        [InlineData("Data/ChatLogParser/twoMessages.txt", 
            "Worm Abyssal TII v2", "Worm Abyssal TII v2")]
        public void Test2(string fileName, string expectedMsg1, string expectedMsg2)
        {
            var sut = new EveMailHelper.ChatLogParser.ChatLogParser();
            sut.ParseFile(fileName);
            sut.Chat.Messages.First().Message.ShouldBe(expectedMsg1);
            sut.Chat.Messages.Last().Message.ShouldBe(expectedMsg2);
        }
    }
}
