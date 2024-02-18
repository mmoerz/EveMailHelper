using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EveMailHelper.DataModels.Market;
using EveMailHelper.DataModels.Sde;
using EveMailHelper.ServiceLayer.Models;
using EveMailHelper.Test.Tools;

using Moq;

namespace EveMailHelper.Test.UnitTests.DataGenerators
{
    public class ProductionPlanDataGenerator
    {
        public static List<ProductionPlan> TestData1
        {
            get
            {
                return new()
                {
                    new ()
                    {
                        Root = new() {
                            SubComponents = new List<BlueprintComponent>()
                        {
                            new()
                            {
                                Name = "L1 item1",
                                Quantity = 10,
                                PricePerUnit = new() { SellPrice = 30, BuyPrice = 26 },
                                QuantityFromBlueprint = 20,
                                SubComponents = new List<BlueprintComponent>
                                {
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
                                    }
                                }
                            },
                            new ()
                            {
                                Name = "L1 item2",
                                Quantity = 1,
                                PricePerUnit = new() { SellPrice = 3, BuyPrice = 2 },
                                QuantityFromBlueprint = 3,
                                SubComponents = new List<BlueprintComponent>
                                {
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
                                }
                            }
                        }
                    }
                }
                };
            }
        }

        public static IEnumerable<object[]> GetPlanAndExpectedEnumerationDataGenerator()
        {
            var data = new List<ITheoryData>();

            var sut1 = TestData1[0];
            List<BlueprintComponent> Enumerated1 = new()
            {
                sut1.Root.SubComponents[0],
                sut1.Root.SubComponents[0].SubComponents[0],
                sut1.Root.SubComponents[0].SubComponents[1],
                sut1.Root.SubComponents[0].SubComponents[2],
                sut1.Root.SubComponents[1],
                sut1.Root.SubComponents[1].SubComponents[0],
                sut1.Root.SubComponents[1].SubComponents[1],
                sut1.Root.SubComponents[1].SubComponents[2],
            };

            data.Add(TheoryData.Factory(sut1, Enumerated1, "a deterministic list of items"));

            return data.ConvertAll(d => d.ToParameterArray());
        }

        public static List<BlueprintComponent> componentData1
        {
            get
            {
                var type1 = new Mock<EveType>();
                type1.SetupSet(t => t.TypeName = "L1 item");

                var type2 = new Mock<EveType>();
                type2.SetupSet(t => t.TypeName = "L2-1 item");

                var type3 = new Mock<EveType>();
                type3.SetupSet(t => t.TypeName = "L2-2 item");

                var type4 = new Mock<EveType>();
                type4.SetupSet(t => t.TypeName = "L2-3 item");

                var result =  new List<BlueprintComponent>()
                {
                    new()
                    {
                        Quantity = 10,
                        PricePerUnit = new() { SellPrice = 300000, BuyPrice = 26726 },
                        Volume = 11,
                        QuantityFromBlueprint = 20,
                        JobCost = 200,
                        EveType = type1.Object,
                    },
                    new()
                    {
                        Quantity = 100,
                        PricePerUnit = new() { SellPrice = 3, BuyPrice = 2 },
                        Volume = 100,
                        QuantityFromBlueprint = 0,
                        Name = "L2-1 item1",
                        JobCost = 10000,
                        EveType = type2.Object,
                    },
                    new()
                    {
                        Quantity = 200,
                        PricePerUnit = new() { SellPrice = 4, BuyPrice = 2 },
                        Volume = 300,
                        Name = "L2-1 item2",
                        EveType = type3.Object
                    },
                    new()
                    {
                        Quantity = 400,
                        PricePerUnit = new() { SellPrice = 5, BuyPrice = 3 },
                        Volume = 0.5,
                        QuantityFromBlueprint = 6,
                        Name = "L2-1 item3",
                        EveType = type4.Object
                    }
                };
                result[0].Add(result[1]);
                result[0].Add(result[2]);
                result[0].Add(result[3]);

                return result;
            }
        }

        public static ProductionPlan SingleSimplePlan
        {
            get
            {
                var bprintTypeMock = new Mock<EveType>();
                var bprintMock = new Mock<IndustryBlueprint>();
                var productMock = new Mock<EveType>();

                bprintTypeMock.SetupSet(t => t.TypeName = "Mocked Blueprint");

                bprintMock.Setup(bp => bp.Type).Returns(bprintTypeMock.Object);

                productMock.SetupSet(c => {
                    c.TypeName = "Mockakaffee";
                    c.Volume = 10.0; 
                });
                
                var result = new ProductionPlan()
                {
                    Blueprint = bprintMock.Object,
                    Product = productMock.Object,
                    ProductQuantity = 10,
                    ProductPricePerUnit = new() { SellPrice = 30000, BuyPrice = 26263 },
                    JobCost = 200,
                    
                };
                result.Add(componentData1[0]);
                return result;
            }
        }

        public static BuyList GetExpectedBuyList1(int numberOfRuns)
        {
            int forcedMulti = 2;
            return new BuyList()
            {
                Name = "Mockakaffee",
                NumberOfRuns = numberOfRuns,
                ItemList = new[]
                {
                    new BuyListItem()
                    {
                        Price = 300 * numberOfRuns / forcedMulti,
                        Quantity = 100 * numberOfRuns / forcedMulti,
                        Volume = 10000 * numberOfRuns / forcedMulti
                    },
                    new BuyListItem()
                    {
                        Price = 800 * numberOfRuns / forcedMulti,
                        Quantity = 200 * numberOfRuns / forcedMulti,
                        Volume = 60000 * numberOfRuns / forcedMulti
                    },
                    new BuyListItem()
                    {
                        Price = 2000 * numberOfRuns / forcedMulti,
                        Quantity = 400 * numberOfRuns / forcedMulti,
                        Volume = 200 * numberOfRuns / forcedMulti
                    }
                }
            };
        }

        public static List<BlueprintComponent> componentData2
        {
            get
            {
                var type1 = new Mock<EveType>();
                type1.SetupSet(t => t.TypeName = "L1 item 1");

                var type2 = new Mock<EveType>();
                type2.SetupSet(t => t.TypeName = "L2-1 item 1");

                var type3 = new Mock<EveType>();
                type3.SetupSet(t => t.TypeName = "L2-1 item 2");

                var type4 = new Mock<EveType>();
                type4.SetupSet(t => t.TypeName = "L2-1 item 3");

                var type5 = new Mock<EveType>();
                type5.SetupSet(t => t.TypeName = "L1 item 2");

                var type6 = new Mock<EveType>();
                type6.SetupSet(t => t.TypeName = "L2-2 item 1");

                var type7 = new Mock<EveType>();
                type7.SetupSet(t => t.TypeName = "L2-2 item 2");

                var type8 = new Mock<EveType>();
                type8.SetupSet(t => t.TypeName = "L2-2 item 3");

                var type9 = new Mock<EveType>();
                type9.SetupSet(t => t.TypeName = "L1 item 3");

                var type10 = new Mock<EveType>();
                type10.SetupSet(t => t.TypeName = "L1 item 4");

                var type11 = new Mock<EveType>();
                type11.SetupSet(t => t.TypeName = "L2-4 item 1");

                var type12 = new Mock<EveType>();
                type12.SetupSet(t => t.TypeName = "L2-4 item 2");

                var result = new List<BlueprintComponent>()
                {
                    new()
                    {
                        Quantity = 10,
                        PricePerUnit = new() { SellPrice = 300000, BuyPrice = 16236 },
                        Volume = 11,
                        QuantityFromBlueprint = 20,
                        Name = "L1 item 1",
                        JobCost = 200,
                        EveType = type1.Object,
                    },
                    new()
                    {
                        Quantity = 100,
                        PricePerUnit = new() { SellPrice = 3, BuyPrice = 2 },
                        Volume = 100,
                        QuantityFromBlueprint = 0,
                        Name = "L2-1 item1",
                        JobCost = 10000,
                        EveType = type2.Object,
                    },
                    new()
                    {
                        Quantity = 200,
                        PricePerUnit = new() { SellPrice = 4, BuyPrice = 3 },
                        Volume = 300,
                        Name = "L2-1 item2",
                        EveType = type3.Object
                    },
                    new()
                    {
                        Quantity = 400,
                        PricePerUnit = new() { SellPrice = 5, BuyPrice = 3 },
                        Volume = 0.5,
                        QuantityFromBlueprint = 6,
                        Name = "L2-1 item3",
                        EveType = type4.Object
                    },
                    new()
                    {
                        Quantity = 10,
                        PricePerUnit = new() { SellPrice = 30, BuyPrice = 26 },
                        Volume = 90,
                        QuantityFromBlueprint = 30,
                        Name = "L1 item 2", // L1 item 2
                        JobCost = 400,
                        EveType = type5.Object,
                    },
                    new()
                    {
                        Quantity = 100,
                        PricePerUnit = new() { SellPrice = 300, BuyPrice = 252 },
                        Volume = 8,
                        QuantityFromBlueprint = 0,
                        Name = "L2-2 item1",
                        JobCost = 10000,
                        EveType = type6.Object,
                    },
                    new()
                    {
                        Quantity = 200,
                        PricePerUnit = new() { SellPrice = 4, BuyPrice = 3 },
                        Volume = 3,
                        Name = "L2-2 item2",
                        EveType = type7.Object
                    },
                    new()
                    {
                        Quantity = 400,
                        PricePerUnit = new() { SellPrice = 5, BuyPrice = 3 },
                        Volume = 1.5,
                        QuantityFromBlueprint = 6,
                        Name = "L2-2 item3",
                        EveType = type8.Object
                    },
                    new()
                    {
                        Quantity = 25,
                        PricePerUnit = new() { SellPrice = 30, BuyPrice = 26 },
                        Volume = 20,
                        QuantityFromBlueprint = 20,
                        Name = "L1 item 3", // - item 3
                        JobCost = 200,
                        EveType = type9.Object,
                    },
                    new()
                    {
                        Quantity = 40,
                        PricePerUnit = new() { SellPrice = 100000, BuyPrice = 7524 },
                        Volume = 15,
                        QuantityFromBlueprint = 10,
                        Name = "L1 item 4",
                        JobCost = 200,
                        EveType = type10.Object,
                    },
                    new()
                    {
                        Quantity = 200,
                        PricePerUnit = new() { SellPrice = 10, BuyPrice = 6 },
                        Volume = 10,
                        QuantityFromBlueprint = 0,
                        Name = "L2-4 item1",
                        JobCost = 10000,
                        EveType = type11.Object,
                    },
                    new()
                    {
                        Quantity = 300,
                        PricePerUnit = new() { SellPrice = 4, BuyPrice = 3 },
                        Volume = 2.5,
                        Name = "L2-4 item2",
                        EveType = type12.Object
                    },
                };
                result[0].Add(result[1]);
                result[0].Add(result[2]);
                result[0].Add(result[3]);
                result[4].Add(result[5]);
                result[4].Add(result[6]);
                result[4].Add(result[7]);

                result[9].Add(result[10]);
                result[9].Add(result[11]);

                return result;
            }
        }

        public static ProductionPlan SinglePlan
        {
            get
            {
                var bprintTypeMock = new Mock<EveType>();
                var bprintMock = new Mock<IndustryBlueprint>();
                var productMock = new Mock<EveType>();

                bprintTypeMock.SetupSet(t => t.TypeName = "Mocked Blueprint2");

                bprintMock.Setup(bp => bp.Type).Returns(bprintTypeMock.Object);

                productMock.SetupSet(c => {
                    c.TypeName = "Mocka";
                    c.Volume = 20.0;
                });

                var result = new ProductionPlan()
                {
                    Blueprint = bprintMock.Object,
                    Product = productMock.Object,
                    ProductQuantity = 400,
                    ProductPricePerUnit = new() { SellPrice = 50000, BuyPrice = 41252 },
                    JobCost = 1300,

                };
                var components = componentData2;

                result.Add(components[0]);
                result.Add(components[4]);
                result.Add(components[8]);
                result.Add(components[9]);
                return result;
            }
        }

        public static BuyList GetExpectedBuyList2(int numberOfRuns)
        {
            int forcedMulti = 2;
            double forcedMulti2 = 0.25;
            return new BuyList()
            {
                Name = "Mocka",
                NumberOfRuns = numberOfRuns,
                ItemList = new[]
                {
                    new BuyListItem()
                    {
                        Price = 300 * numberOfRuns / forcedMulti,
                        Quantity = 100 * numberOfRuns / forcedMulti,
                        Volume = 10000 * numberOfRuns / forcedMulti
                    },
                    new BuyListItem()
                    {
                        Price = 800 * numberOfRuns / forcedMulti,
                        Quantity = 200 * numberOfRuns / forcedMulti,
                        Volume = 60000 * numberOfRuns / forcedMulti
                    },
                    new BuyListItem()
                    {
                        Price = 2000 * numberOfRuns / forcedMulti,
                        Quantity = 400 * numberOfRuns / forcedMulti,
                        Volume = 200 * numberOfRuns / forcedMulti
                    },
                    new BuyListItem() // only L1 - item2 (directbuy)
                    {
                        Price = 300 * numberOfRuns,
                        Quantity = 10 * numberOfRuns,
                        Volume = 900 * numberOfRuns
                    },
                    new BuyListItem() // L1 - item3
                    {
                        Price = 750 * numberOfRuns,
                        Quantity = 25 * numberOfRuns,
                        Volume = 500 * numberOfRuns
                    },
                    new BuyListItem() // L2-4 item 1
                    {
                        Price = 2000 * numberOfRuns / forcedMulti2,
                        Quantity = 200 * numberOfRuns / forcedMulti2,
                        Volume = 2000 * numberOfRuns / forcedMulti2
                    },
                    new BuyListItem() // L2-4 item 2
                    {
                        Price = 1200 * numberOfRuns / forcedMulti2,
                        Quantity = 300 * numberOfRuns / forcedMulti2,
                        Volume = 300 * 2.5 * numberOfRuns / forcedMulti2
                    },
                }
            };
        }

        public static List<BlueprintComponent> componentData3
        {
            get
            {
                var type1 = new Mock<EveType>();
                type1.SetupSet(t => t.TypeName = "L1 item 1");

                var type2 = new Mock<EveType>();
                type2.SetupSet(t => t.TypeName = "L2-1 item 1");

                var type3 = new Mock<EveType>();
                type3.SetupSet(t => t.TypeName = "L2-1 item 2");

                var type4 = new Mock<EveType>();
                type4.SetupSet(t => t.TypeName = "L2-1 item 3");

                var type5 = new Mock<EveType>();
                type5.SetupSet(t => t.TypeName = "L1 item 2");

                var type6 = new Mock<EveType>();
                type6.SetupSet(t => t.TypeName = "L2-2 item 1");

                var type7 = new Mock<EveType>();
                type7.SetupSet(t => t.TypeName = "L2-2 item 2");

                var type8 = new Mock<EveType>();
                type8.SetupSet(t => t.TypeName = "L2-2 item 3");

                var type9 = new Mock<EveType>();
                type9.SetupSet(t => t.TypeName = "L1 item 3");

                var type10 = new Mock<EveType>();
                type10.SetupSet(t => t.TypeName = "L1 item 4");

                var type11 = new Mock<EveType>();
                type11.SetupSet(t => t.TypeName = "L2-4 item 1");

                var type12 = new Mock<EveType>();
                type12.SetupSet(t => t.TypeName = "L2-4 item 2");

                var result = new List<BlueprintComponent>()
                {
                    new()
                    {
                        Quantity = 5,
                        PricePerUnit = new() { SellPrice = 17600, BuyPrice = 16236 },
                        Volume = 5,
                        QuantityFromBlueprint = 0,
                        Name = "Helium Fuel Block",
                        JobCost = 0,
                        EveType = type1.Object,
                    },
                    new()
                    {
                        Quantity = 100,
                        PricePerUnit = new() { SellPrice = 3, BuyPrice = 2 },
                        Volume = 0.2,
                        QuantityFromBlueprint = 8457,
                        Name = "Crystallite Alloy", // L1 item 2
                        JobCost = 1000,
                        EveType = type2.Object,
                    },
                    new()
                    {
                        Quantity = 5,
                        PricePerUnit = new() { SellPrice = 17600, BuyPrice = 15783 },
                        Volume = 5,
                        Name = "Helium Fuel Block", // L1 item 3
                        EveType = type3.Object
                    },
                    new()
                    {
                        Quantity = 100,
                        PricePerUnit = new() { SellPrice = 4409, BuyPrice = 3462 },
                        Volume = 0.05,
                        QuantityFromBlueprint = 0,
                        Name = "Cobalt",
                        EveType = type4.Object
                    },
                    new()
                    {
                        Quantity = 100,
                        PricePerUnit = new() { SellPrice = 7990, BuyPrice = 7834 },
                        Volume = 0.05,
                        QuantityFromBlueprint = 0,
                        Name = "Cadmium", 
                        JobCost = 400,
                        EveType = type5.Object,
                    },
                    new()
                    {
                        Quantity = 100,
                        PricePerUnit = new() { SellPrice = 3282, BuyPrice = 2512 },
                        Volume = 0.2,
                        QuantityFromBlueprint = 0,
                        Name = "Carbon Polymers", // L1 item 3
                        JobCost = 1000,
                        EveType = type6.Object,
                    },
                    new()
                    {
                        Quantity = 5,
                        PricePerUnit = new() { SellPrice = 17600, BuyPrice = 14463},
                        Volume = 5,
                        Name = "Helium Fuel Block",
                        EveType = type7.Object
                    },
                    new()
                    {
                        Quantity = 100,
                        PricePerUnit = new() { SellPrice = 2150, BuyPrice = 1235 },
                        Volume = 0.05,
                        QuantityFromBlueprint = 0,
                        Name = "Hydrocarbons",
                        EveType = type8.Object
                    },
                    new()
                    {
                        Quantity = 100,
                        PricePerUnit = new() { SellPrice = 2150, BuyPrice = 2043 },
                        Volume = 0.05,
                        QuantityFromBlueprint = 0,
                        Name = "Silicates", 
                        JobCost = 200,
                        EveType = type9.Object,
                    },
                };
                result[2].Add(result[3]);
                result[2].Add(result[4]);
                result[2].Add(result[5]);
                result[6].Add(result[7]);
                result[6].Add(result[8]);
                result[6].Add(result[9]);

                return result;
            }
        }

        public static ProductionPlan SinglePlan2
        {
            get
            {
                var bprintTypeMock = new Mock<EveType>();
                var bprintMock = new Mock<IndustryBlueprint>();
                var productMock = new Mock<EveType>();

                bprintTypeMock.SetupSet(t => t.TypeName = "Crystalline Carbonite");

                bprintMock.Setup(bp => bp.Type).Returns(bprintTypeMock.Object);

                productMock.SetupSet(c => {
                    c.TypeName = "Mocka";
                    c.Volume = 20.0;
                });

                var result = new ProductionPlan()
                {
                    Blueprint = bprintMock.Object,
                    Product = productMock.Object,
                    ProductQuantity = 10000,
                    ProductPricePerUnit = new() { SellPrice = 1660000, BuyPrice = 1273000 },
                    JobCost = 300,

                };
                var components = componentData3;

                result.Add(components[0]);
                result.Add(components[1]);
                result.Add(components[2]);
                result.Add(components[6]);
                return result;
            }
        }

    }
}
