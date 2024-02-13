using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EveMailHelper.ServiceLayer.Models;

namespace EveMailHelper.Test.Data
{
    public class ProductionPlanDataGenerator
    {
        public static readonly List<ProductionPlan> _prodPlanData1 = new()
        {
            new ()
            {
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
        };

        public static readonly List<BlueprintComponent> Enumerated1 = new()
        {
            _prodPlanData1[0].SubComponents[0],
            _prodPlanData1[0].SubComponents[0].SubComponents[0],
            _prodPlanData1[0].SubComponents[0].SubComponents[1],
            _prodPlanData1[0].SubComponents[0].SubComponents[2],
            _prodPlanData1[0].SubComponents[1],
            _prodPlanData1[0].SubComponents[1].SubComponents[0],
            _prodPlanData1[0].SubComponents[1].SubComponents[1],
            _prodPlanData1[0].SubComponents[1].SubComponents[2],
        };

        public static IEnumerable<object[]> GetPlanAndExpectedEnumerationDataGenerator()
        {
            yield return new object[] { _prodPlanData1[0], Enumerated1 };
        }

        public static readonly List<ProductionPlan> _prodPlanData2 = new()
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

            }
        };

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

        public static IEnumerable<object[]> GetMinimumNumberOfRuns()
        {
            _prodPlanData2[0].Add(_componentData2[1]);
            _componentData2[1].Add(_componentData2[2]);
            _componentData2[1].Add(_componentData2[3]);
            _componentData2[1].Add(_componentData2[4]);
            _prodPlanData2[0].Add(_componentData2[5]);
            _componentData2[5].Add(_componentData2[6]);
            _componentData2[5].Add(_componentData2[7]);
            _componentData2[5].Add(_componentData2[8]);

            _prodPlanData2[1].Add(_componentData3[1]);
            _componentData3[1].Add(_componentData3[2]);
            _componentData3[1].Add(_componentData3[3]);
            _componentData3[1].Add(_componentData3[4]);
            _prodPlanData2[1].Add(_componentData3[5]);
            _componentData3[5].Add(_componentData3[6]);
            _componentData3[5].Add(_componentData3[7]);
            _componentData3[5].Add(_componentData3[8]);

            yield return new object[] { _prodPlanData2[0], 2 };
            yield return new object[] { _prodPlanData2[1], 6 };
        }
    }
}
