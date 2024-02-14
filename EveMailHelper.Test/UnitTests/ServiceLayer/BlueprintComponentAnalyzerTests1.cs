using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoFixture.Xunit2;

using EveMailHelper.ServiceLayer.Managers;
using EveMailHelper.ServiceLayer.Models;
using EveMailHelper.ServiceLayer.Utilities;
using EveMailHelper.Test.Tools;
using EveMailHelper.Test.UnitTests.DataGenerators;

using Moq;

using Shouldly;

using Xunit;

namespace EveMailHelper.Test.UnitTests.ServiceLayer
{
    public class BlueprintComponentAnalyzerTests1
    {
        [Theory]
        [MemberData(nameof(BlueprintComponentAnalyzerDataGenerator.ModifiedQuantity_2_64), MemberType = typeof(BlueprintComponentAnalyzerDataGenerator))]
        public void TestGetMinimumNumberOfRuns(BlueprintAnalyzer sut, int expected, string message)
        {
            int result = sut.ModifiedQuantity();
            result.ShouldBeEquivalentTo(expected, message);
        }

        [Theory]
        [MemberData(nameof(BlueprintComponentAnalyzerDataGenerator.GetPriceSum), MemberType = typeof(BlueprintComponentAnalyzerDataGenerator))]
        public void TestPriceSum(BlueprintAnalyzer sut, double expected, string message)
        {
            double result = sut.PriceSum();
            result.ShouldBeEquivalentTo(expected, message);
        }

        [Theory]
        [MemberData(nameof(BlueprintComponentAnalyzerDataGenerator.GetVolumeSum), MemberType = typeof(BlueprintComponentAnalyzerDataGenerator))]
        public void TestVolumeSum(BlueprintAnalyzer sut, double expected)
        {
            double result = sut.VolumeSum();
            result.ShouldBeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(nameof(BlueprintComponentAnalyzerDataGenerator.GetSimpleBestPriceSum), MemberType = typeof(BlueprintComponentAnalyzerDataGenerator))]
        public void TestSimpleBestPriceSum(BlueprintAnalyzer sut, double expected)
        {
            var result = sut.BestPriceSum();
            result.ShouldBeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(nameof(BlueprintComponentAnalyzerDataGenerator.GetBestPriceSum), MemberType = typeof(BlueprintComponentAnalyzerDataGenerator))]
        public void TestBestPriceSum(BlueprintAnalyzer sut, double expected)
        {
            var result = sut.BestPriceSum();
            result.ShouldBeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(nameof(BlueprintComponentAnalyzerDataGenerator.GetIsProducing), MemberType = typeof(BlueprintComponentAnalyzerDataGenerator))]
        public void TestIsProducingBetter(BlueprintAnalyzer sut, bool expected)
        {
            var result = sut.IsProducingComponentBetter();
            result.ShouldBeEquivalentTo(expected);
        }
    }
}
