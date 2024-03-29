﻿using System;
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
    public class ProductionPlanAnalyzerDataGenerator
    {
        public static List<ProductionPlan> TestData1
        {
            get
            {
                return new List<ProductionPlan>()
                {
                };
            }
        }
        public static List<ProductionPlan> TestData2_Cheap
        {
            get
            {
                return new List<ProductionPlan>()
                {
                    new() {
                        ProductQuantity = 200,
                        ProductPricePerUnit = new() { SellPrice = 300, BuyPrice = 293 },
                        JobCost = 200,
                    }
                };
            }
        }

        public static List<ProductionPlan> TestData2_Expensive
        {
            get
            {
                return new List<ProductionPlan>()
                {
                    new() {
                        ProductQuantity = 200,
                        ProductPricePerUnit = new() { SellPrice = 30000, BuyPrice = 27425 },
                        JobCost = 200,
                    }
                };
            }
        }

        public static List<ProductionPlan> TestData3 => new()
                {
                    new ProductionPlan {
                        ProductQuantity = 100,
                        ProductPricePerUnit = new() { SellPrice = 200, BuyPrice = 197 },
                        JobCost = 300,
                    },
                    new ProductionPlan {
                        ProductQuantity = 400,
                        ProductPricePerUnit = new() { SellPrice = 500, BuyPrice = 478 },
                        JobCost = 600,
                    },
                    new ProductionPlan {
                        ProductQuantity = 700,
                        ProductPricePerUnit = new() { SellPrice = 800, BuyPrice = 654 },
                        JobCost = 900,
                    }
                };

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
                        PricePerUnit = new() { SellPrice = 30, BuyPrice = 28 },
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
                        Quantity = 1,
                        PricePerUnit = new() { SellPrice = 300, BuyPrice = 254 },
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
                        QuantityFromBlueprint = 5,
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = new() { SellPrice = 300, BuyPrice = 232 },
                        QuantityFromBlueprint = 2,
                        Name = "L2-3 item1"
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = new() { SellPrice = 300, BuyPrice = 253 },
                        QuantityFromBlueprint = 2,
                        Name = "L2-3 item2"
                    },
                    new()
                    {
                        Quantity = 1,
                        PricePerUnit = new() { SellPrice = 300, BuyPrice = 265 },
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

        public static List<BlueprintComponent> TestData6
        {
            get
            {
                var result = new List<BlueprintComponent>()
                {
                    new BlueprintComponent {
                        Name = "TopLevel",
                        Quantity = 200,
                        PricePerUnit = new() { SellPrice = 300, BuyPrice = 265 },
                        QuantityFromBlueprint = 100,
                    },
                    new()
                    {
                        Name = "L1 item1",
                        Quantity = 10,
                        PricePerUnit = new() { SellPrice = 30, BuyPrice = 23 },
                        QuantityFromBlueprint = 20,
                    },
                    new()
                    {
                        Name = "L1 item2",
                        Quantity = 1,
                        PricePerUnit = new() { SellPrice = 300, BuyPrice = 274 },
                        QuantityFromBlueprint = 3,
                    },
                    new()
                    {
                        Name = "L1 item3",
                        Quantity = 1,
                        PricePerUnit =  new() { SellPrice = 3, BuyPrice = 2 },
                        QuantityFromBlueprint = 5,
                    }
                };
                result[0].Add(result[1]);
                result[0].Add(result[2]);
                result[0].Add(result[3]);
                
                return result;
            }
        }

        public static List<BlueprintComponent> TestData7
        {
            get
            {
                var result = new List<BlueprintComponent>()
                {
                    new BlueprintComponent {
                        Name = "TopLevel",
                        Quantity = 200,
                        PricePerUnit = new() { SellPrice = 300, BuyPrice = 226 },
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
                        PricePerUnit = new() { SellPrice = 3, BuyPrice = 2 },
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

        public static IEnumerable<object[]> GetMinimumNumberOfRuns()
        {
            var data = new List<ITheoryData>();
            double mod = 0;

            var data1 = TestData2_Cheap[0];
            data1.Add(TestData6[0]);
            ProductionPlanAnalyzer sut1 = new(data1, 0);
            data.Add(TheoryData.Factory(sut1, 1, "only integers of forcedRunnumbers should be counted."));

            var data2 = TestData2_Expensive[0];
            foreach (var item in BlueprintComponentDataGenerator.TestData4)
                data2.Add(item);
            ProductionPlanAnalyzer sut2 = new(data2, 0);
            data.Add(TheoryData.Factory(sut2, 4, "ignore forcedRunNumbers that are a multiple of bigger forcedRunNumbers"));

            var data3 = TestData2_Expensive[0];
            data3.Add(TestData5[0]);
            ProductionPlanAnalyzer sut3 = new(data3, 0);
            data.Add(TheoryData.Factory(sut3, 30, "ignore forcedRunNumbers that are a multiple of bigger forcedRunNumbers"));

            var data4 = TestData2_Expensive[0];
            data4.Add(TestData7[0]);
            ProductionPlanAnalyzer sut4 = new(data4, 0);
            data.Add(TheoryData.Factory(sut4, 4, "ignore forcedRunNumbers that are a multiple of bigger forcedRunNumbers"));
            
            return data.ConvertAll(d => d.ToParameterArray());
        }

        public static IEnumerable<object[]> GetMinimumNumberOfRunsBestPrice()
        {
            var data = new List<ITheoryData>();

            var data1 = TestData2_Cheap[0];
            data1.Add(TestData6[0]);
            ProductionPlanAnalyzer sut1 = new(data1, 0);
            data.Add(TheoryData.Factory(sut1, 1, "only integers of forcedRunNumbers should be counted."));

            var data2 = TestData2_Expensive[0];
            foreach (var item in BlueprintComponentDataGenerator.TestData4)
                data2.Add(item);
            ProductionPlanAnalyzer sut2 = new(data2, 0);
            data.Add(TheoryData.Factory(sut2, 4, "ignore forcedRunNumbers that are a multiple of bigger forcedRunNumbers"));

            var data3 = TestData2_Expensive[0];
            data3.Add(TestData5[0]);
            ProductionPlanAnalyzer sut3 = new(data3, 0);
            data.Add(TheoryData.Factory(sut3, 6, "ignore item L1-3 5-multiplier"));

            var data4 = TestData2_Expensive[0];
            data4.Add(TestData7[0]);
            ProductionPlanAnalyzer sut4 = new(data4, 0);
            data.Add(TheoryData.Factory(sut4, 4, "ignore item L1-2 3-multiplier"));

            return data.ConvertAll(d => d.ToParameterArray());
        }
    }
}
