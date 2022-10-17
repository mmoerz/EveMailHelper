using EveMailHelper.ChatLogParser;

using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;

using Shouldly;

using System;
using System.Linq;

using Xunit;

using static MudBlazor.CategoryTypes;

namespace EveMailHelper.Test.ChatLogParser
{
    public partial class RegexMessage
    {
        public RegexMessage()
        {
        }

        [Theory]
        [InlineData("[ 2022.06.02 17:54:57 ] Nat Aaroon > Worm Abyssal TII v2",
            "2022.06.02 17:54:57", "Nat Aaroon", "Worm Abyssal TII v2")]
        [InlineData("[ 2022.06.02 17:54:40 ] Nat Aaroon > Worm Abyssal TII v2",
            "2022.06.02 17:54:40", "Nat Aaroon", "Worm Abyssal TII v2")]
        [InlineData("[ 2022.06.02 17:54:40 ] H.u'1`\"nta > Worm Abyssal TII v2",
            "2022.06.02 17:54:40", "H.u'1`\"nta", "Worm Abyssal TII v2")]
        public void Test1(string input, string expected1, string expected2, string expected3)
        {
            var sut = ParserRegexes.Message.Match(input);
            sut.Success.ShouldBeTrue();
            sut.Groups["timestamp"].Value.ShouldBe(expected1);
            sut.Groups[1].Value.ShouldBe(expected1);

            sut.Groups["author"].Value.ShouldBe(expected2);
            sut.Groups[2].Value.ShouldBe(expected2);

            sut.Groups["message"].Value.ShouldBe(expected3);
            sut.Groups[3].Value.ShouldBe(expected3);
        }

        [Theory]
        [InlineData("Session started: 2022.06")]
        [InlineData("   Session started: +2022.06")]
        public void Test2Fail(string input)
        {
            var sut = ParserRegexes.Message.Match(input);
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
