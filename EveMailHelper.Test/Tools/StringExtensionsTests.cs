using Shouldly;

using Xunit;
using EveMailHelper.BusinessLibrary.Tools;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace EveMailHelper.Test.Tools
{
    public partial class StringExtensionsTests
    {
        public StringExtensionsTests()
        {

        }

        public static IEnumerable<object[]> GetNameDataAndExpected()
        {
            yield return new object[] { ',', "name1, name2", new string[] { "name1", "name2" } };
            yield return new object[] { ',', "firstname1 lastname1 , name2", new string[] { "firstname1 lastname1", "name2" } };
            yield return new object[] { ',', " firstname1 lastname1, name2   last2", new string[] { "firstname1 lastname1", "name2   last2" } };
            yield return new object[] { ',', "firstname1, firstname2 lastname2", new string[] { "firstname1", "firstname2 lastname2" } };
            yield return new object[] { '.', "  firstname1  . first2 middle2 last2 . first3 last3  ", 
                                        new string[] { "firstname1", "first2 middle2 last2", "first3 last3" } };
            // test case ending with colon
            yield return new object[] { ',', "firstname1 lastname1, firstname2 lastname2,",
                                        new string[] { "firstname1 lastname1", "firstname2 lastname2"} };

        }

        [Theory]
        [MemberData(nameof(GetNameDataAndExpected))]
        public void SplitStringOfCharactersTest1(char delimiter, string sot, string[] expected)
        {
            var result = sot.SplitStringOfCharacters(delimiter);
            result.ShouldBe(expected);
        }

    }
}
