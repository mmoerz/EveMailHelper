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
using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.ServiceLayer.Utilities;

namespace EveMailHelper.Test.ServiceLayer
{
    public partial class ProductionManagerTests1
    {
        protected IMarketManager _marketManager = null!;
        protected IBlueprintManager _blueprintManager = null!;
        protected IProductionManager ProductionManager { get; set; } = null!;

        public ProductionManagerTests1()
        {
             //ProductionManager = new ProductionManager(_marketManager, _blueprintManager, )
        }
        
        [Theory]
        [MemberData(nameof(ProductionPlanDataGenerator.GetPlanAndExpectedEnumerationDataGenerator), MemberType = typeof(ProductionPlanDataGenerator))]
        public void TestEnumeration1(ProductionPlan sut, List<BlueprintComponent> expectedItems)
        {
            int i = 0;
            foreach (BlueprintComponent item in sut) 
            {
                item.ShouldBeEquivalentTo(expectedItems[i]);
                i++;
            }
        }

                
        [Theory]
        [MemberData(nameof(ProductionPlanDataGenerator.GetMinimumNumberOfRuns), MemberType = typeof(ProductionPlanDataGenerator))]
        public void TestPriceSum(ProductionPlan sut, int expected)
        {
            ProductionPlanAnalyzer analyzer = new(sut);
            var result = analyzer.GetMinNumberOfRuns(false);
            result.ShouldBeEquivalentTo(expected);
        }
        /*
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
