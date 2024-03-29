﻿using System;
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

using FluentAssertions;

using Moq;

using Shouldly;

using Xunit;

namespace EveMailHelper.Test.UnitTests.ServiceLayer
{
    public class BlueprintComponentAnalyzerTests1
    {
        [Theory]
        [MemberData(nameof(BlueprintComponentAnalyzerDataGenerator.ModifiedQuantity_2_64), MemberType = typeof(BlueprintComponentAnalyzerDataGenerator))]
        public void TestModifiedQuantity(BlueprintAnalyzer sut, int expected, string message)
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
        public void TestVolumeSum(BlueprintAnalyzer sut, double expected, string message)
        {
            double result = sut.VolumeSum();
            result.ShouldBeEquivalentTo(expected, message);
        }

        [Theory]
        [MemberData(nameof(BlueprintComponentAnalyzerDataGenerator.GetSimpleBestPriceSum), MemberType = typeof(BlueprintComponentAnalyzerDataGenerator))]
        public void TestSimpleBestPriceSum(BlueprintAnalyzer sut, double expected, string message)
        {
            var result = sut.BestPriceSum();
            result.ShouldBeEquivalentTo(expected, message);
        }

        [Theory]
        [MemberData(nameof(BlueprintComponentAnalyzerDataGenerator.GetBestPriceSum), MemberType = typeof(BlueprintComponentAnalyzerDataGenerator))]
        public void TestBestPriceSum(BlueprintAnalyzer sut, double expected, string message)
        {
            var result = sut.BestPriceSum();
            result.ShouldBeEquivalentTo(expected, message);
        }

        [Theory]
        [MemberData(nameof(BlueprintComponentAnalyzerDataGenerator.GetIsProducing), MemberType = typeof(BlueprintComponentAnalyzerDataGenerator))]
        public void TestIsProducingBetter(BlueprintAnalyzer sut, bool expected, string message)
        {
            var result = sut.IsProducingComponentBetter();
            result.ShouldBeEquivalentTo(expected, message);
        }

        [Theory]
        [MemberData(nameof(BlueprintComponentAnalyzerDataGenerator.GetBlueprintFail1), MemberType = typeof(BlueprintComponentAnalyzerDataGenerator))]
        public void BlueprintQuantityZero1(BlueprintAnalyzer sut, string expectederrmsg, string message)
        {
            Action action = () => { sut.BestPriceSum(); };
            action.Should().Throw<Exception>()
                .WithMessage(expectederrmsg, message);
        }

        [Theory]
        [MemberData(nameof(BlueprintComponentAnalyzerDataGenerator.GetBlueprintFail2), MemberType = typeof(BlueprintComponentAnalyzerDataGenerator))]
        public void BlueprintQuantityZero2(BlueprintAnalyzer sut, string expectederrmsg, string message)
        {
            Action action = () => { sut.PriceSum(); };
            action.Should().Throw<Exception>()
                .WithMessage(expectederrmsg, message);
        }
    }
}
