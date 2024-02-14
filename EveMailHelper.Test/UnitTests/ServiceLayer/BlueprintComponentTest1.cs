using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EveMailHelper.ServiceLayer.Models;
using EveMailHelper.Test.UnitTests.DataGenerators;

using Shouldly;

using Xunit;

namespace EveMailHelper.Test.UnitTests.ServiceLayer
{
    public class BlueprintComponentTest1
    {
        [Theory]
        [MemberData(nameof(BlueprintComponentDataGenerator.GetComponentsProductionDepth), MemberType = typeof(BlueprintComponentDataGenerator))]
        public void TestProductionDepth(BlueprintComponent sut, int expected, string message)
        {
            sut.ProductionDepth.ShouldBe(expected, message);
        }

        [Theory]
        [MemberData(nameof(BlueprintComponentDataGenerator.GetForcedMultiplier), MemberType = typeof(BlueprintComponentDataGenerator))]
        public void TestForcedMultiplier(BlueprintComponent sut, double expected, string message)
        {
            var result = sut.ForcedQuantityMultiplier;
            result.ShouldBeEquivalentTo(expected, message);
        }
    }
}
