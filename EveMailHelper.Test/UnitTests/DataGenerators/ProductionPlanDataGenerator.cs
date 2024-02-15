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
                                PricePerUnit = 30,
                                QuantityFromBlueprint = 20,
                                SubComponents = new List<BlueprintComponent>
                                {
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
                                    }
                                }
                            },
                            new ()
                            {
                                Name = "L1 item2",
                                Quantity = 1,
                                PricePerUnit = 3,
                                QuantityFromBlueprint = 3,
                                SubComponents = new List<BlueprintComponent>
                                {
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
                        PricePerUnit = 300000,
                        Volume = 11,
                        QuantityFromBlueprint = 20,
                        JobCost = 200,
                        EveType = type1.Object,
                    },
                    new()
                    {
                        Quantity = 100,
                        PricePerUnit = 3,
                        Volume = 100,
                        QuantityFromBlueprint = 0,
                        Name = "L2-1 item1",
                        JobCost = 10000,
                        EveType = type2.Object,
                    },
                    new()
                    {
                        Quantity = 200,
                        PricePerUnit = 4,
                        Volume = 300,
                        Name = "L2-1 item2",
                        EveType = type3.Object
                    },
                    new()
                    {
                        Quantity = 400,
                        PricePerUnit = 5,
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
                    ProductPricePerUnit = 30000,
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

        public static List<ProductionPlan> TestData2
        {
            get
            {
                return new()
                {
                    new ()
                    {
                        ProductPricePerUnit = 10000,
                        ProductQuantity = 1000,

                    },
                    new ()
                    {
                        ProductPricePerUnit = 20000,
                        ProductQuantity = 2000,

                    },
                    new ()
                    {
                        ProductPricePerUnit = 20000,
                        ProductQuantity = 2000,

                    }
                };
            }
        }

        public static readonly List<BlueprintComponent> _componentData2 = new()
        {
            new()
            {
                Name = "TopLevel",
                Quantity = 200,
                PricePerUnit = 300,
                Volume = 10,
                QuantityFromBlueprint = 100,
                JobCost = 100,
            },
                    new()
                    {
                        Name = "L1 item1",
                        Quantity = 10,
                        PricePerUnit = 30,
                        Volume = 11,
                        QuantityFromBlueprint = 20,
                        JobCost = 200,
                    },
                            new()
                            {
                                Quantity = 1,
                                PricePerUnit = 3,
                                Volume = 111,
                                QuantityFromBlueprint = 0,
                                Name = "L2-1 item1",
                                JobCost = 10000,
                            },
                            new()
                            {
                                Quantity = 2,
                                PricePerUnit = 4,
                                Volume = 112,

                                Name = "L2-1 item2"
                            },
                            new()
                            {
                                Quantity = 3,
                                PricePerUnit = 5,
                                Volume = 113,
                                QuantityFromBlueprint = 6,
                                Name = "L2-1 item3"
                            },


                    new ()
                    {
                        Name = "L1 item2",
                        Quantity = 1,
                        PricePerUnit = 3,
                        Volume = 12,
                        QuantityFromBlueprint = 2,
                        JobCost = 300,
                    },
                            new()
                            {
                                Quantity = 1,
                                PricePerUnit = 3,
                                Volume = 121,
                                QuantityFromBlueprint = 2,
                                Name = "L2-2 item1"
                            },
                            new()
                            {
                                Quantity = 1,
                                PricePerUnit = 3,
                                Volume = 122,
                                QuantityFromBlueprint = 2,
                                Name = "L2-2 item2"
                            },
                            new()
                            {
                                Quantity = 1,
                                PricePerUnit = 3,
                                Volume = 123,
                                QuantityFromBlueprint = 2,
                                Name = "L2-2 item3"
                            },
        };

        public static readonly List<BlueprintComponent> _componentData3 = new()
        {
            new()
            {
                Name = "TopLevel",
                Quantity = 200,
                PricePerUnit = 300,
                Volume = 10,
                QuantityFromBlueprint = 100,
                JobCost = 100,
            },
                    new()
                    {
                        Name = "L1 item1",
                        Quantity = 10,
                        PricePerUnit = 30,
                        Volume = 11,
                        QuantityFromBlueprint = 20,
                        JobCost = 200,
                    },
                            new()
                            {
                                Quantity = 1,
                                PricePerUnit = 3,
                                Volume = 111,
                                QuantityFromBlueprint = 0,
                                Name = "L2-1 item1",
                                JobCost = 10000,
                            },
                            new()
                            {
                                Quantity = 2,
                                PricePerUnit = 4,
                                Volume = 112,

                                Name = "L2-1 item2"
                            },
                            new()
                            {
                                Quantity = 3,
                                PricePerUnit = 5,
                                Volume = 113,
                                QuantityFromBlueprint = 6,
                                Name = "L2-1 item3"
                            },


                    new ()
                    {
                        Name = "L1 item2",
                        Quantity = 1,
                        PricePerUnit = 3000,
                        Volume = 12,
                        QuantityFromBlueprint = 3,
                        JobCost = 300,
                    },
                            new()
                            {
                                Quantity = 1,
                                PricePerUnit = 3,
                                Volume = 121,
                                QuantityFromBlueprint = 2,
                                Name = "L2-2 item1"
                            },
                            new()
                            {
                                Quantity = 1,
                                PricePerUnit = 3,
                                Volume = 122,
                                QuantityFromBlueprint = 2,
                                Name = "L2-2 item2"
                            },
                            new()
                            {
                                Quantity = 1,
                                PricePerUnit = 3,
                                Volume = 123,
                                QuantityFromBlueprint = 2,
                                Name = "L2-2 item3"
                            },
        };

        public static readonly List<BlueprintComponent> _componentData4 = new()
        {
            new()
            {
                Name = "TopLevel",
                Quantity = 200,
                PricePerUnit = 300,
                Volume = 10,
                QuantityFromBlueprint = 100,
                JobCost = 100,
            },
                    new()
                    {
                        Name = "L1 item1",
                        Quantity = 10,
                        PricePerUnit = 30,
                        Volume = 11,
                        QuantityFromBlueprint = 20,
                        JobCost = 200,
                    },
                            new()
                            {
                                Quantity = 1,
                                PricePerUnit = 3,
                                Volume = 111,
                                QuantityFromBlueprint = 0,
                                Name = "L2-1 item1",
                                JobCost = 10000,
                            },
                            new()
                            {
                                Quantity = 2,
                                PricePerUnit = 4,
                                Volume = 112,

                                Name = "L2-1 item2"
                            },
                            new()
                            {
                                Quantity = 3,
                                PricePerUnit = 5,
                                Volume = 113,
                                QuantityFromBlueprint = 6,
                                Name = "L2-1 item3"
                            },


                    new ()
                    {
                        Name = "L1 item2",
                        Quantity = 1,
                        PricePerUnit = 3,
                        Volume = 12,
                        QuantityFromBlueprint = 2,
                        JobCost = 300,
                    },
                            new()
                            {
                                Quantity = 1,
                                PricePerUnit = 3,
                                Volume = 121,
                                QuantityFromBlueprint = 2,
                                Name = "L2-2 item1"
                            },
                            new()
                            {
                                Quantity = 1,
                                PricePerUnit = 3,
                                Volume = 122,
                                QuantityFromBlueprint = 2,
                                Name = "L2-2 item2"
                            },
                            new()
                            {
                                Quantity = 1,
                                PricePerUnit = 3,
                                Volume = 123,
                                QuantityFromBlueprint = 2,
                                Name = "L2-2 item3"
                            },
        };

        //public static IEnumerable<object[]> GetMinimumNumberOfRuns()
        //{
        //    TestData2[0].Add(_componentData2[1]);
        //    _componentData2[1].Add(_componentData2[2]);
        //    _componentData2[1].Add(_componentData2[3]);
        //    _componentData2[1].Add(_componentData2[4]);
        //    TestData2[0].Add(_componentData2[5]);
        //    _componentData2[5].Add(_componentData2[6]);
        //    _componentData2[5].Add(_componentData2[7]);
        //    _componentData2[5].Add(_componentData2[8]);

        //    TestData2[1].Add(_componentData3[1]);
        //    _componentData3[1].Add(_componentData3[2]);
        //    _componentData3[1].Add(_componentData3[3]);
        //    _componentData3[1].Add(_componentData3[4]);
        //    TestData2[1].Add(_componentData3[5]);
        //    _componentData3[5].Add(_componentData3[6]);
        //    _componentData3[5].Add(_componentData3[7]);
        //    _componentData3[5].Add(_componentData3[8]);

        //    yield return new object[] { _prodPlanData2[0], 2 };
        //    yield return new object[] { _prodPlanData2[1], 6 };
        //}
    }
}
