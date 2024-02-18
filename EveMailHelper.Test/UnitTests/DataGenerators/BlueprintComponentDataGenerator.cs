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
    public class BlueprintComponentDataGenerator
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
                        Quantity = 200,
                        PricePerUnit = new() { SellPrice = 300, BuyPrice = 274 },
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
                        Quantity = 10,
                        PricePerUnit = new() { SellPrice = 20, BuyPrice = 19 },
                        QuantityFromBlueprint = 30,
                    },
                    new BlueprintComponent {
                        Name = "TopLevel",
                        Quantity = 4,
                        PricePerUnit = new() { SellPrice = 50, BuyPrice = 45 },
                        QuantityFromBlueprint = 8,
                    },
                    new BlueprintComponent {
                        Name = "TopLevel",
                        Quantity = 50,
                        PricePerUnit = new() { SellPrice = 80, BuyPrice = 72 },
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
                        PricePerUnit = new() { SellPrice = 300, BuyPrice = 256 },
                        QuantityFromBlueprint = 200,
                    },
                    new()
                    {
                        Name = "L1 item1",
                        Quantity = 10,
                        PricePerUnit = new() { SellPrice = 30, BuyPrice = 2 },
                        QuantityFromBlueprint = 20,
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = new() { SellPrice = 3, BuyPrice = 2 },
                        QuantityFromBlueprint = 2,
                        Name = "L2-1 item1"
                    },
                    new()
                    {
                        Quantity = 2,
                        PricePerUnit = new() { SellPrice = 4, BuyPrice = 2 },
                        QuantityFromBlueprint = 4,
                        Name = "L2-1 item2"
                    },
                    new()
                    {
                        Quantity = 3,
                        PricePerUnit = new() { SellPrice = 5, BuyPrice = 3 },
                        QuantityFromBlueprint = 6,
                        Name = "L2-1 item3"
                    },
                    new()
                    {
                        Name = "L1 item2",
                        Quantity = 1,
                        PricePerUnit = new() { SellPrice = 3, BuyPrice = 2 },
                        QuantityFromBlueprint = 4, // well this makes producing an item feasible
                                                   // (4 output at the price of 3 (9)
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = new() { SellPrice = 3, BuyPrice = 2 },
                        QuantityFromBlueprint = 2,
                        Name = "L2-2 item1"
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = new() { SellPrice = 3, BuyPrice = 2 },
                        QuantityFromBlueprint = 2,
                        Name = "L2-2 item2"
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = new() { SellPrice = 3, BuyPrice = 2 },
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
                        PricePerUnit = new() { SellPrice = 300, BuyPrice = 285 },
                        QuantityFromBlueprint = 200,
                    },
                    new()
                    {
                        Name = "L1 item1",
                        Quantity = 10,
                        PricePerUnit = new() { SellPrice = 30, BuyPrice = 25 },
                        QuantityFromBlueprint = 20,
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = new() { SellPrice = 3, BuyPrice = 2 },
                        QuantityFromBlueprint = 2,
                        Name = "L2-1 item1"
                    },
                    new()
                    {
                        Quantity = 2,
                        PricePerUnit = new() { SellPrice = 4, BuyPrice = 3 },
                        QuantityFromBlueprint = 4,
                        Name = "L2-1 item2"
                    },
                    new()
                    {
                        Quantity = 3,
                        PricePerUnit = new() { SellPrice = 5, BuyPrice = 4 },
                        QuantityFromBlueprint = 6,
                        Name = "L2-1 item3"
                    },
                    new()
                    {
                        Name = "L1 item2",
                        Quantity = 6,
                        PricePerUnit = new() { SellPrice = 300, BuyPrice = 272 },
                        QuantityFromBlueprint = 3,
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = new() { SellPrice = 3, BuyPrice = 2 },
                        QuantityFromBlueprint = 1,
                        Name = "L2-2 item1"
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = new() { SellPrice = 3, BuyPrice = 2 },
                        QuantityFromBlueprint = 2,
                        Name = "L2-2 item2"
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = new() { SellPrice = 3, BuyPrice = 2 },
                        QuantityFromBlueprint = 2,
                        Name = "L2-2 item3"
                    } ,
                    new()
                    {
                        Name = "L1 item3",
                        Quantity = 1,
                        PricePerUnit = new() { SellPrice = 3, BuyPrice = 2 },
                        QuantityFromBlueprint = 4,
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = new() { SellPrice = 3, BuyPrice = 2 },
                        QuantityFromBlueprint = 2,
                        Name = "L2-3 item1"
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = new() { SellPrice = 3, BuyPrice = 2 },
                        QuantityFromBlueprint = 2,
                        Name = "L2-3 item2"
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = new() { SellPrice = 3, BuyPrice = 2 },
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

        /// <summary>
        /// Fail data because L1 item1 has 0 Quantity
        /// </summary>
        public static List<BlueprintComponent> TestData6
        {
            get
            {
                var result = new List<BlueprintComponent>()
                {
                    new ()
                    {
                        Name = "L1 item1",
                        Quantity = 0,
                        PricePerUnit = new() { SellPrice = 30, BuyPrice = 34 },
                        QuantityFromBlueprint = 10,
                    },
                    new ()
                    {
                        Quantity = 1,
                        PricePerUnit = new() { SellPrice = 3, BuyPrice = 2 },
                        QuantityFromBlueprint = 2,
                        Name = "L2-1 item1"
                    },
                    new()
                    {
                        Quantity = 2,
                        PricePerUnit = new() { SellPrice = 4, BuyPrice = 3 },
                        QuantityFromBlueprint = 4,
                        Name = "L2-1 item2"
                    },
                    new()
                    {
                        Quantity = 3,
                        PricePerUnit = new() { SellPrice = 5, BuyPrice = 4 },
                        QuantityFromBlueprint = 6,
                        Name = "L2-1 item3"
                    }
                };
                result[0].Add(result[1]);
                result[0].Add(result[2]);
                result[0].Add(result[3]);

                return result;
            }
        }

        /// <summary>
        /// Fail data because L1 item1 has 0 QuantityFromBlueprint
        /// </summary>
        public static List<BlueprintComponent> TestData7
        {
            get
            {
                var result = new List<BlueprintComponent>()
                {
                    new ()
                    {
                        Name = "L1 item1",
                        Quantity = 10,
                        PricePerUnit = new() { SellPrice = 30, BuyPrice = 26 },
                        QuantityFromBlueprint = 0,
                    },
                    new ()
                    {
                        Quantity = 1,
                        PricePerUnit = new() { SellPrice = 3, BuyPrice = 2 },
                        QuantityFromBlueprint = 2,
                        Name = "L2-1 item1"
                    },
                    new()
                    {
                        Quantity = 2,
                        PricePerUnit = new() { SellPrice = 4, BuyPrice = 3 },
                        QuantityFromBlueprint = 4,
                        Name = "L2-1 item2"
                    },
                    new()
                    {
                        Quantity = 3,
                        PricePerUnit = new() { SellPrice = 5, BuyPrice = 4 },
                        QuantityFromBlueprint = 6,
                        Name = "L2-1 item3"
                    }
                };
                result[0].Add(result[1]);
                result[0].Add(result[2]);
                result[0].Add(result[3]);

                return result;
            }
        }

        public static IEnumerable<object[]> GetComponentsProductionDepth()
        {
            var data = new List<ITheoryData>();

            var data1 = TestData4;
            
            data.Add(TheoryData.Factory(TestData4[0], 1, "root node has depth 1"));
            data.Add(TheoryData.Factory(TestData4[1], 2, "subnode has depth 2"));
            data.Add(TheoryData.Factory(TestData4[2], 3, "sub sub node has depth 3"));
            data.Add(TheoryData.Factory(TestData4[3], 3, "sub sub node has depth 3"));
            data.Add(TheoryData.Factory(TestData4[4], 3, "sub sub node has depth 3"));
            data.Add(TheoryData.Factory(TestData4[5], 2, "subnode has depth 2"));
            data.Add(TheoryData.Factory(TestData4[6], 3, "sub sub node has depth 3"));
            data.Add(TheoryData.Factory(TestData4[7], 3, "sub sub node has depth 3"));
            data.Add(TheoryData.Factory(TestData4[8], 3, "sub sub node has depth 3"));

            return data.ConvertAll(d => d.ToParameterArray());
        }

        public static IEnumerable<object[]> GetForcedMultiplier()
        {
            var data = new List<ITheoryData>();

            data.Add(TheoryData.Factory(TestData5[1], 2.0, "doubled by the multiplier"));
            data.Add(TheoryData.Factory(TestData5[2], 1.0, "endnode must always be 1"));
            data.Add(TheoryData.Factory(TestData5[3], 1.0, "endnode must always be 1"));
            data.Add(TheoryData.Factory(TestData5[4], 1.0, "endnode must always be 1"));
            data.Add(TheoryData.Factory(TestData5[5], 0.5, "halfed by the multiplier"));
            data.Add(TheoryData.Factory(TestData5[9], 4.0, "quadruppel the multiplier"));

            return data.ConvertAll(d => d.ToParameterArray());
        }

        public static IEnumerable<object[]> GetBlueprintFail1()
        {
            var data = new List<ITheoryData>();

            data.Add(TheoryData.FailFactory2(TestData6[0], "Quantity must not be 0.", 
                "must fail because 0 Blueprintquantity for producing item"));
            data.Add(TheoryData.FailFactory2(TestData7[0], "QuantityFromBlueprint must not be 0.",
                "must fail because 0 Blueprintquantity for producing item"));

            return data.ConvertAll(d => d.ToParameterArray());
        }

        public static IEnumerable<object[]> GetBlueprintFail2()
        {
            var data = new List<ITheoryData>();
            
            data.Add(TheoryData.FailFactory(TestData7[0], "must fail because 0 Blueprintquantity for producing item"));

            return data.ConvertAll(d => d.ToParameterArray());
        }

    }
}
