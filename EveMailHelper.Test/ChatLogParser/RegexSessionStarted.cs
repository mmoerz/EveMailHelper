using EveMailHelper.ChatLogParser;

using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;

using Shouldly;

using System;
using System.Linq;

using Xunit;

namespace EveMailHelper.Test.ChatLogParser
{
    public partial class RegexSessionStarted
    {
        public RegexSessionStarted()
        {
        }

        [Theory]
        [InlineData("   Session started: 2022.06", "2022.06")]
        [InlineData("          Session started: 2022.06.02 17:31:48", "2022.06.02 17:31:48")]
        public void Test1(string input, string expected)
        {
            var sut = ParserRegexes.SessionStarted.Match(input);
            sut.Success.ShouldBeTrue();
            sut.Groups["sessionstart"].Value.ShouldBe(expected);
        }

        [Theory]
        [InlineData("Session started: 2022.06")]
        [InlineData("   Session started: +2022.06")]
        public void Test2Fail(string input)
        {
            var sut = ParserRegexes.SessionStarted.Match(input);
            sut.Success.ShouldBeFalse();
        }

        //[Theory]
        //[InlineData("          Session started: 2022.06.02 17:31:48", DateTime.Parse("2022.06.02 17:31:48"))]
        //public void Test3(string input, string expected)
        //{
        //    var sut = ParserRegexes.SessionStarted.Match(input);
        //    sut.Success.ShouldBeTrue();
        //    var date = DateTime.Parse(sut.Groups[1].Value);
                
        //    date.ShouldBe(expected);
        //}
    }
}
