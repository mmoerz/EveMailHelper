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

    public class ProductionManagerTests1
    {
        [Theory]
        [AutoDomainData]
        public void FirstTest(
            ProductionManager sut
            //ProductionPlan plan1
        )
        {
            ProductionPlan plan = ProductionPlanDataGenerator.TestData2[0];
            /*
            plan.Add(bc1);
            plan.Add(bc2);
            bc1.Add(bc11);
            bc1.Add(bc12);
            bc1.Add(bc13);
            bc2.Add(bc21);
            bc2.Add(bc22);
            bc2.Add(bc23);
            */
            sut.DeriveBestPriceBuyListFromPlan(plan,10,0.0);

        }



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
