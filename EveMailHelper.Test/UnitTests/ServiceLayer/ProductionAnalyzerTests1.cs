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
    public class ProductionAnalyzerTests1
    {
        [Theory]
        [MemberData(nameof(ProductionPlanAnalyzerDataGenerator.GetMinimumNumberOfRuns), MemberType = typeof(ProductionPlanAnalyzerDataGenerator))]
        public void TestGetMinimumNumberOfRuns(ProductionPlanAnalyzer sut, int expected, string message)
        {
            var result = sut.GetMinNumberOfRuns(false);
            result.ShouldBeEquivalentTo(expected, message);
        }

        [Theory]
        [MemberData(nameof(ProductionPlanAnalyzerDataGenerator.GetMinimumNumberOfRunsBestPrice), MemberType = typeof(ProductionPlanAnalyzerDataGenerator))]
        public void TestGetMinimumNumberOfRunsBestPrice(ProductionPlanAnalyzer sut, int expected, string message)
        {
            var result = sut.GetMinNumberOfRuns(true);
            result.ShouldBeEquivalentTo(expected, message);
        }
    }
}
