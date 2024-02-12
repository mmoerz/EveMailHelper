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
        public void TestBestPriceSum1(int expected, BlueprintComponents sut)
        {
            sut.ProductionDepth.ShouldBe(expected);
        }
                
        [Theory]
        [MemberData(nameof(BlueprintComponentDataGenerator.GetPriceSum), MemberType = typeof(BlueprintComponentDataGenerator))]
        public void TestPriceSum(BlueprintComponents sut, double expected)
        {
            var result = sut.PriceSum;
            result.ShouldBeEquivalentTo(expected);
        }
    }
}
