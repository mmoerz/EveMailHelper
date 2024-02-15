using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoFixture.Xunit2;

using EveMailHelper.DataModels.Sde;
using EveMailHelper.ServiceLayer.Managers;
using EveMailHelper.ServiceLayer.Models;
using EveMailHelper.ServiceLayer.Utilities;
using EveMailHelper.Test.Tools;
using EveMailHelper.Test.UnitTests.DataGenerators;

using FluentAssertions;

using Microsoft.Extensions.Options;

using Moq;

using Shouldly;

using Xunit;

namespace EveMailHelper.Test.UnitTests.ServiceLayer
{

    public class ProductionManagerTests1
    {
        [Theory]
        [AutoDomainData]
        public void DeriveBestPriceBuyListFromPlan1(
            ProductionManager sut
            //,EveType eveType
            //ProductionPlan plan1

        )
        {
            var numberOfRuns = 10;
            ProductionPlan plan = ProductionPlanDataGenerator.SingleSimplePlan;
            var expected = ProductionPlanDataGenerator.GetExpectedBuyList1(numberOfRuns);
            
            var result = sut.DeriveBestPriceBuyListFromPlan(plan, numberOfRuns, 0.0);
            result.Should().BeEquivalentTo(expected,
                Options => Options.Excluding(x => x.Id)
                                    .Excluding(x => x.CreateDate)
                                    .Excluding(x => x.ItemList)
                                    );
            result.ItemList.Should().BeEquivalentTo(expected.ItemList,
                Options => Options.Excluding(i => i.Id)
                .Excluding(i => i.BuyList)
                .Excluding(i => i.BuyListId)
                .Excluding(i => i.EveType)
                .Excluding(i => i.EveTypeId)
                );
        }

        //[Theory]
        //[MemberData(nameof(ProductionPlanDataGenerator.DeriveBestPriceBuyListFromPlan), MemberType = typeof(ProductionPlanDataGenerator))]
        //public void TestDeriveBestPriceBuyListFromPlan(BlueprintComponent sut, double expected)
        //{
        //    var result = sut.PriceSum;
        //    result.ShouldBeEquivalentTo(expected);
        //}

        //[Theory]
        //[MemberData(nameof(ProductionPlanDataGenerator.GetMinimumNumberOfRuns), MemberType = typeof(ProductionPlanDataGenerator))]
        //public void TestGetMinimumNumberOfRuns(ProductionPlan sut, int expected, string message)
        //{
        //    ProductionPlanAnalyzer analyzer = new(sut);
        //    var result = analyzer.GetMinNumberOfRuns(false);
        //    result.ShouldBeEquivalentTo(expected, message);
        //}

        //[Theory]
        //[MemberData(nameof(ProductionPlanDataGenerator.GetMinimumNumberOfRunsBestPrice), MemberType = typeof(ProductionPlanDataGenerator))]
        //public void TestGetMinimumNumberOfRunsBestPrice(ProductionPlan sut, int expected, string message)
        //{
        //    ProductionPlanAnalyzer analyzer = new(sut);
        //    var result = analyzer.GetMinNumberOfRuns(true);
        //    result.ShouldBeEquivalentTo(expected, message);
        //}
    }
}
