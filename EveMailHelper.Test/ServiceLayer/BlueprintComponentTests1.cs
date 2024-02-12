using EveMailHelper.ChatLogParser;
using EveMailHelper.ServiceLayer.Models;

using EVEStandard.Models;

using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;

using Shouldly;

using System.IO;
using System.Linq;
using System.Collections.Generic;

using Xunit;
using EveMailHelper.Test.Data;
using System;

namespace EveMailHelper.Test.ServiceLayer
{
    public partial class BlueprintComponentTests1
    {
        public BlueprintComponentTests1()
        {

        }

        [Theory]
        [MemberData(nameof(BlueprintComponentDataGenerator.GetComponentsProductionDepth), MemberType = typeof(BlueprintComponentDataGenerator))]
        public void TestProductionDepth(int expected, BlueprintComponentTree sut)
        {
            sut.ProductionDepth.ShouldBe(expected);
        }
                
        [Theory]
        [MemberData(nameof(BlueprintComponentDataGenerator.GetPriceSum), MemberType = typeof(BlueprintComponentDataGenerator))]
        public void TestPriceSum(BlueprintComponentTree sut, double expected)
        {
            var result = sut.PriceSum;
            result.ShouldBeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(nameof(BlueprintComponentDataGenerator.GetVolumeSum), MemberType = typeof(BlueprintComponentDataGenerator))]
        public void TestVolumeSum(BlueprintComponentTree sut, double expected)
        {
            var result = sut.VolumeSum;
            result.ShouldBeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(nameof(BlueprintComponentDataGenerator.GetBestPriceSum), MemberType = typeof(BlueprintComponentDataGenerator))]
        public void TestBestPriceSum(BlueprintComponentTree sut, double expected)
        {
            var result = sut.BestPriceSum();
            result.ShouldBeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(nameof(BlueprintComponentDataGenerator.GetIsProducing), MemberType = typeof(BlueprintComponentDataGenerator))]
        public void TestIsProducingBetter(BlueprintComponentTree sut, bool expected)
        {
            var result = sut.IsProducingBetter;
            result.ShouldBeEquivalentTo(expected);
        }
    }
}
