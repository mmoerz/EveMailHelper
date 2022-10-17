using EveMailHelper.ChatLogParser;

using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;

using Shouldly;

using System.IO;

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
            var sut = new LogParser();
            sut.ParseFile(fileName);
        }
    }
}
