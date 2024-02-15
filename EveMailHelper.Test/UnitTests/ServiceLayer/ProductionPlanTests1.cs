using EveMailHelper.ChatLogParser;
using EveMailHelper.ServiceLayer.Models;

using EVEStandard.Models;

using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;

using Shouldly;

using System.IO;
using System.Linq;
using System.Collections.Generic;

using Xunit;
using EveMailHelper.Test.UnitTests.DataGenerators;
using System;

namespace EveMailHelper.Test.UnitTests.ServiceLayer
{
    public partial class ProductionPlanTests1
    {
        public ProductionPlanTests1()
        {

        }

        [Theory]
        [MemberData(nameof(ProductionPlanDataGenerator.GetPlanAndExpectedEnumerationDataGenerator), MemberType = typeof(ProductionPlanDataGenerator))]
        public void TestEnumeration1(ProductionPlan sut, List<BlueprintComponent> expectedItems, string message)
        {
            int i = 0;
            foreach (BlueprintComponent item in sut)
            {
                item.ShouldBeEquivalentTo(expectedItems[i], message);
                i++;
            }
        }
            /*        
            [Theory]
            [MemberData(nameof(BlueprintComponentDataGenerator.GetPriceSum), MemberType = typeof(BlueprintComponentDataGenerator))]
            public void TestPriceSum(BlueprintComponent sut, double expected)
            {
                var result = sut.PriceSum;
                result.ShouldBeEquivalentTo(expected);
            }

            [Theory]
            [MemberData(nameof(BlueprintComponentDataGenerator.GetVolumeSum), MemberType = typeof(BlueprintComponentDataGenerator))]
            public void TestVolumeSum(BlueprintComponent sut, double expected)
            {
                var result = sut.VolumeSum;
                result.ShouldBeEquivalentTo(expected);
            }

            [Theory]
            [MemberData(nameof(BlueprintComponentDataGenerator.GetBestPriceSum), MemberType = typeof(BlueprintComponentDataGenerator))]
            public void TestBestPriceSum(BlueprintComponent sut, double expected)
            {
                var result = sut.BestPriceSum();
                result.ShouldBeEquivalentTo(expected);
            }

            [Theory]
            [MemberData(nameof(BlueprintComponentDataGenerator.GetIsProducing), MemberType = typeof(BlueprintComponentDataGenerator))]
            public void TestIsProducingBetter(BlueprintComponent sut, bool expected)
            {
                var result = sut.IsProducingBetter;
                result.ShouldBeEquivalentTo(expected);
            }
            */
        }
    }
