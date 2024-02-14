using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

using EveMailHelper.ServiceLayer.Models;
using EveMailHelper.ServiceLayer.Utilities;
using EveMailHelper.Test.Tools;

namespace EveMailHelper.Test.UnitTests.DataGenerators
{
    public class BlueprintComponentAnalyzerDataGenerator
    {
        public static List<BlueprintComponent> TestData1
        {
            get
            {
                return new List<BlueprintComponent>()
                {
                };
            }
        }
        public static List<BlueprintComponent> TestData2
        {
            get
            {
                return new List<BlueprintComponent>()
                {
                    new BlueprintComponent {
                        Name = "TopLevel",
                        Quantity = 100,
                        PricePerUnit = 300,
                        Volume = 100,
                        QuantityFromBlueprint = 200,
                    }
                };
            }
        }

        public static List<BlueprintComponent> TestData3
        {
            get
            {
                return new ()
                {
                    new BlueprintComponent {
                        Name = "TopLevel",
                        Quantity = 100,
                        PricePerUnit = 20,
                        Volume = 100,
                        QuantityFromBlueprint = 30,
                    },
                    new BlueprintComponent {
                        Name = "TopLevel",
                        Quantity = 200,
                        PricePerUnit = 50,
                        Volume = 200,
                        QuantityFromBlueprint = 8,
                    },
                    new BlueprintComponent {
                        Name = "TopLevel",
                        Quantity = 1000,
                        PricePerUnit = 80,
                        Volume = 300,
                        QuantityFromBlueprint = 25,
                    },
                    new BlueprintComponent {
                        Name = "TopLevel",
                        Quantity = 3000,
                        PricePerUnit = 80,
                        Volume = 400,
                        QuantityFromBlueprint = 25,
                    },
                    new BlueprintComponent {
                        Name = "TopLevel",
                        Quantity = 5000,
                        PricePerUnit = 80,
                        Volume = 500,
                        QuantityFromBlueprint = 25,
                    }
                    ,
                    new BlueprintComponent {
                        Name = "TopLevel",
                        Quantity = 10000,
                        PricePerUnit = 80,
                        Volume = 600,
                        QuantityFromBlueprint = 25,
                    }
                };
            }
        }

        public static List<BlueprintComponent> TestData4
        {
            get
            {
                var result = new List<BlueprintComponent>()
                {
                    new BlueprintComponent {
                        Name = "TopLevel",
                        Quantity = 200,
                        PricePerUnit = 300,
                        QuantityFromBlueprint = 200,
                    },
                    new()
                    {
                        Name = "L1 item1",
                        Quantity = 10,
                        PricePerUnit = 30,
                        QuantityFromBlueprint = 20,
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = 3,
                        QuantityFromBlueprint = 2,
                        Name = "L2-1 item1"
                    },
                    new()
                    {
                        Quantity = 2,
                        PricePerUnit = 4,
                        QuantityFromBlueprint = 4,
                        Name = "L2-1 item2"
                    },
                    new()
                    {
                        Quantity = 3,
                        PricePerUnit = 5,
                        QuantityFromBlueprint = 6,
                        Name = "L2-1 item3"
                    },
                    new()
                    {
                        Name = "L1 item2",
                        Quantity = 1,
                        PricePerUnit = 3,
                        QuantityFromBlueprint = 4, // well this makes producing an item feasible
                                                   // (4 output at the price of 3 (9)
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = 3,
                        QuantityFromBlueprint = 2,
                        Name = "L2-2 item1"
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = 3,
                        QuantityFromBlueprint = 2,
                        Name = "L2-2 item2"
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = 3,
                        QuantityFromBlueprint = 2,
                        Name = "L2-2 item3"
                    }
                };
                result[0].Add(result[1]);
                result[0].Add(result[5]);
                result[1].Add(result[2]);
                result[1].Add(result[3]);
                result[1].Add(result[4]);
                result[5].Add(result[6]);
                result[5].Add(result[7]);
                result[5].Add(result[8]);
                return result;
            }
        }

        public static List<BlueprintComponent> TestData5
        {
            get
            {
                var result = new List<BlueprintComponent>()
                {
                    new BlueprintComponent {
                        Name = "TopLevel",
                        Quantity = 200,
                        PricePerUnit = 300,
                        QuantityFromBlueprint = 200,
                    },
                    new()
                    {
                        Name = "L1 item1",
                        Quantity = 10,
                        PricePerUnit = 30,
                        QuantityFromBlueprint = 20,
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = 3,
                        QuantityFromBlueprint = 2,
                        Name = "L2-1 item1"
                    },
                    new()
                    {
                        Quantity = 2,
                        PricePerUnit = 4,
                        QuantityFromBlueprint = 4,
                        Name = "L2-1 item2"
                    },
                    new()
                    {
                        Quantity = 3,
                        PricePerUnit = 5,
                        QuantityFromBlueprint = 6,
                        Name = "L2-1 item3"
                    },
                    new()
                    {
                        Name = "L1 item2",
                        Quantity = 1,
                        PricePerUnit = 3,
                        QuantityFromBlueprint = 3,
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = 3,
                        QuantityFromBlueprint = 1,
                        Name = "L2-2 item1"
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = 3,
                        QuantityFromBlueprint = 2,
                        Name = "L2-2 item2"
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = 3,
                        QuantityFromBlueprint = 2,
                        Name = "L2-2 item3"
                    } ,
                    new()
                    {
                        Name = "L1 item3",
                        Quantity = 1,
                        PricePerUnit = 300,
                        QuantityFromBlueprint = 4,
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = 3,
                        QuantityFromBlueprint = 2,
                        Name = "L2-3 item1"
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = 3,
                        QuantityFromBlueprint = 2,
                        Name = "L2-3 item2"
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = 3,
                        QuantityFromBlueprint = 2,
                        Name = "L2-3 item3"
                    }
                };
                result[0].Add(result[1]);
                result[0].Add(result[5]);
                result[0].Add(result[9]);
                result[1].Add(result[2]);
                result[1].Add(result[3]);
                result[1].Add(result[4]);
                result[5].Add(result[6]);
                result[5].Add(result[7]);
                result[5].Add(result[8]);
                result[9].Add(result[10]);
                result[9].Add(result[11]);
                result[9].Add(result[12]);
                return result;
            }
        }

        public static IEnumerable<object[]> ModifiedQuantity_2_64()
        {
            var data = new List<ITheoryData>();
            var mod = -2.64;

            BlueprintAnalyzer sut1 = new(TestData3[0], mod);
            data.Add(TheoryData.Factory(sut1, 98, "must reduce quantity correctly"));

            BlueprintAnalyzer sut3 = new(TestData3[1], mod);
            data.Add(TheoryData.Factory(sut3, 195, "must reduce quantity correctly"));

            BlueprintAnalyzer sut2 = new(TestData3[2], mod);
            data.Add(TheoryData.Factory(sut2, 974, "must reduce quantity correctly"));

            BlueprintAnalyzer sut4 = new(TestData3[3], mod);
            data.Add(TheoryData.Factory(sut4, 2921, "must reduce quantity correctly"));

            BlueprintAnalyzer sut5 = new(TestData3[4], mod);
            data.Add(TheoryData.Factory(sut5, 4868, "must reduce quantity correctly"));

            BlueprintAnalyzer sut6 = new(TestData3[5], mod);
            data.Add(TheoryData.Factory(sut6, 9736, "must reduce quantity correctly"));


            BlueprintAnalyzer sut7 = new(TestData3[0], 0);
            data.Add(TheoryData.Factory(sut7, 100, "no reduction"));

            BlueprintAnalyzer sut8 = new(TestData3[5], 0);
            data.Add(TheoryData.Factory(sut8, 10000, "no reduction"));

            return data.ConvertAll(d => d.ToParameterArray());
        }

        public static IEnumerable<object[]> GetPriceSum()
        {
            var data = new List<ITheoryData>();
            var mod = 0;

            BlueprintAnalyzer sut1 = new(TestData2[0], mod);
            data.Add(TheoryData.Factory(sut1, 30000.0, "price must be exact"));

            BlueprintAnalyzer sut2 = new(TestData3[1], mod);
            data.Add(TheoryData.Factory(sut2, 10000.0, "price must be exact"));

            return data.ConvertAll(d => d.ToParameterArray());
        }

        public static IEnumerable<object[]> GetVolumeSum()
        {
            var data = new List<ITheoryData>();
            var mod = 2.64;

            BlueprintAnalyzer sut1 = new(TestData2[0], mod);
            data.Add(TheoryData.Factory(sut1, 974, "volume must be exact"));

            BlueprintAnalyzer sut2 = new(TestData3[1], mod);
            data.Add(TheoryData.Factory(sut2, 300, "volume must be exact"));

            BlueprintAnalyzer sut3 = new(TestData2[0], 0);
            data.Add(TheoryData.Factory(sut3, 1000, "volume must be exact"));

            return data.ConvertAll(d => d.ToParameterArray());
        }

        public static IEnumerable<object[]> GetSimpleBestPriceSum()
        {
            var data = new List<ITheoryData>();
            var mod = 0;

            BlueprintAnalyzer sut1 = new(TestData4[1], mod);
            data.Add(TheoryData.Factory(sut1, 13, "best price of component with subcomponents"));

            BlueprintAnalyzer sut2 = new(TestData4[5], mod);
            data.Add(TheoryData.Factory(sut2, 2.25, "best price of component with subcomponents"));

            BlueprintAnalyzer sut3 = new(TestData4[0], mod);
            data.Add(TheoryData.Factory(sut3, 15.25, "best price of three levels"));

            return data.ConvertAll(d => d.ToParameterArray());
        }

        public static IEnumerable<object[]> GetBestPriceSum()
        {
            var data = new List<ITheoryData>();
            var mod = 0;

            BlueprintAnalyzer sut1 = new(TestData5[1], mod);
            data.Add(TheoryData.Factory(sut1, 974, "best price of component with subcomponents"));

            BlueprintAnalyzer sut2 = new(TestData5[5], mod);
            data.Add(TheoryData.Factory(sut2, 974, "best price of component with subcomponents"));

            BlueprintAnalyzer sut3 = new(TestData5[9], mod);
            data.Add(TheoryData.Factory(sut3, 974, "best price of component with subcomponents"));

            BlueprintAnalyzer sut4 = new(TestData5[0], mod);
            data.Add(TheoryData.Factory(sut4, 974, "best price of three levels"));

            return data.ConvertAll(d => d.ToParameterArray());
        }

        public static IEnumerable<object[]> GetIsProducing()
        {
            var data = new List<ITheoryData>();
            var mod = 0;

            BlueprintAnalyzer sut1 = new(TestData4[1], mod);
            data.Add(TheoryData.Factory(sut1, true, "must produce because cheaper than buying"));

            BlueprintAnalyzer sut2 = new(TestData4[5], mod);
            data.Add(TheoryData.Factory(sut2, false, "buying is cheaper"));

            BlueprintAnalyzer sut3 = new(TestData4[0], mod);
            data.Add(TheoryData.Factory(sut3, true, "producing is cheaper"));

            return data.ConvertAll(d => d.ToParameterArray());
        }

    }
}
