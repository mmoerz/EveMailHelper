using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EveMailHelper.ServiceLayer.Models;
using EveMailHelper.ServiceLayer.Utilities;
using EveMailHelper.Test.UnitTests.DataGenerators;

using FluentAssertions;

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

        [Theory]
        [MemberData(nameof(BlueprintComponentDataGenerator.GetBlueprintFail1), MemberType = typeof(BlueprintComponentDataGenerator))]
        public void BlueprintQuantityZero1(BlueprintComponent sut, string expectederrmsg, string message)
        {
            Action action = () => { var x = sut.ForcedQuantityMultiplier; };
            action.Should().Throw<Exception>()
                .WithMessage(expectederrmsg, message);
        }
    }
}
