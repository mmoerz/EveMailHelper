using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

using EveMailHelper.ServiceLayer.Models;
using EveMailHelper.Test.Tools;

namespace EveMailHelper.Test.UnitTests.DataGenerators
{
    public class ProductionPlanDataGenerator
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
                    new ProductionPlan {
                        ProductQuantity = 200,
                        ProductPricePerUnit = 300,
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
                    new ProductionPlan {
                        ProductQuantity = 200,
                        ProductPricePerUnit = 30000,
                        JobCost = 200,
                    }
                };
            }
        }

        public static List<ProductionPlan> TestData3
        {
            get
            {
                return new ()
                {
                    new ProductionPlan {
                        ProductQuantity = 100,
                        ProductPricePerUnit = 200,
                        JobCost = 300,
                    },
                    new ProductionPlan {
                        ProductQuantity = 400,
                        ProductPricePerUnit = 500,
                        JobCost = 600,
                    },
                    new ProductionPlan {
                        ProductQuantity = 700,
                        ProductPricePerUnit = 800,
                        JobCost = 900,
                    }
                };
            }
        }

        #region ProductionPlansWithBlueprintComponents
        

        #endregion

        public static IEnumerable<object[]> GetMinimumNumberOfRuns()
        {
            var data = new List<ITheoryData>();

            var data1 = TestData2_Cheap[0];
            foreach (var item in BlueprintComponentDataGenerator.TestData3)
                data1.Add(item);
            data.Add(TheoryData.Factory(data1, 1, "only integers of forcedRunnumbers should be counted."));

            var data2 = TestData2_Expensive[0];
            foreach (var item in BlueprintComponentDataGenerator.TestData4)
                data2.Add(item);
            data.Add(TheoryData.Factory(data2, 4, "ignore forcedRunNumbers that are a multiple of bigger forcedRunNumbers"));

            var data3 = TestData2_Expensive[0];
            data3.Add(BlueprintComponentDataGenerator.TestData5[0]);
            data.Add(TheoryData.Factory(data3, 12, "ignore forcedRunNumbers that are a multiple of bigger forcedRunNumbers"));

            var data4 = TestData2_Expensive[0];
            foreach (var item in BlueprintComponentDataGenerator.TestData5)
                data4.Add(item);
            data.Add(TheoryData.Factory(data4, 12, "ignore forcedRunNumbers that are a multiple of bigger forcedRunNumbers"));
            
            return data.ConvertAll(d => d.ToParameterArray());
        }

        public static IEnumerable<object[]> GetMinimumNumberOfRunsBestPrice()
        {
            var data = new List<ITheoryData>();

            var data1 = TestData2_Cheap[0];
            foreach (var item in BlueprintComponentDataGenerator.TestData3)
                data1.Add(item);
            data.Add(TheoryData.Factory(data1, 1, "only integers of forcedRunnumbers should be counted."));

            var data2 = TestData2_Expensive[0];
            foreach (var item in BlueprintComponentDataGenerator.TestData4)
                data2.Add(item);
            data.Add(TheoryData.Factory(data2, 4, "ignore forcedRunNumbers that are a multiple of bigger forcedRunNumbers"));

            var data3 = TestData2_Expensive[0];
            data3.Add(BlueprintComponentDataGenerator.TestData5[0]);
            data.Add(TheoryData.Factory(data3, 4, "ignore forcedRunNumbers that are a multiple of bigger forcedRunNumbers"));

            //var data4 = TestData2_Expensive[0];
            //foreach (var item in BlueprintComponentDataGenerator.TestData5)
            //    data4.Add(item);
            //data.Add(TheoryData.Factory(data4, 12, "ignore forcedRunNumbers that are a multiple of bigger forcedRunNumbers"));

            return data.ConvertAll(d => d.ToParameterArray());
        }

        //public static IEnumerable<object[]> GetMinimumNumberOfRuns()
        //{

        //}

            /*
            public static readonly List<BlueprintComponent> _componentData2 = new()
            {
                new()
                {
                    Name = "TopLevel",
                    Quantity = 200,
                    PricePerUnit = 300,
                    QuantityFromBlueprint = 200,
                }
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
                },
                        new()
                        {
                            Name = "L1 item1",
                            Quantity = 10,
                            PricePerUnit = 30,
                            Volume = 11,
                            QuantityFromBlueprint = 20,
                        },
                                new()
                                {
                                    Quantity = 1,
                                    PricePerUnit = 3,
                                    Volume = 111,
                                    QuantityFromBlueprint = 0,
                                    Name = "L2-1 item1"
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

            public static readonly List<BlueprintComponent> _componentData5 = new()
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



            public static readonly List<Tuple<int, BlueprintComponent>> selection1 = new()
            {
                new(1, _componentData1[0].SubComponents[0]),
                new(2, _componentData1[0].SubComponents[0].SubComponents[1]),
                new(0, _componentData1[0])
            };

            public static IEnumerable<object[]> GetComponentsAndExpectedEnumerationDataGenerator()
            {
                _componentData1[0].Add(_componentData1[1]);
                _componentData1[1].Add(_componentData1[2]);
                _componentData1[1].Add(_componentData1[3]);
                _componentData1[1].Add(_componentData1[4]);
                _componentData1[0].Add(_componentData1[5]);
                _componentData1[5].Add(_componentData1[6]);
                _componentData1[5].Add(_componentData1[7]);
                _componentData1[5].Add(_componentData1[8]);

                yield return new object[] { _componentData1, Enumerated1 };
            }

            public static IEnumerable<object[]> GetComponentsProductionDepth()
            {
                _componentData3[0].Add(_componentData3[1]);
                _componentData3[1].Add(_componentData3[2]);
                _componentData3[1].Add(_componentData3[3]);
                _componentData3[1].Add(_componentData3[4]);
                _componentData3[0].Add(_componentData3[5]);
                _componentData3[5].Add(_componentData3[6]);
                _componentData3[5].Add(_componentData3[7]);
                _componentData3[5].Add(_componentData3[8]);

                yield return new object[] { 0, _componentData3[0] };
                yield return new object[] { 1, _componentData3[1] };
                yield return new object[] { 2, _componentData3[2] };
                yield return new object[] { 2, _componentData3[3] };
                yield return new object[] { 2, _componentData3[4] };
                yield return new object[] { 1, _componentData3[5] };
                yield return new object[] { 2, _componentData3[6] };
                yield return new object[] { 2, _componentData3[7] };
                yield return new object[] { 2, _componentData3[8] };
            }

            public static IEnumerable<object[]> GetPriceSum()
            {
                yield return new object[] { _componentData3[0], 60000.0 };
                yield return new object[] { _componentData3[4], 15.0 };
            }

            public static IEnumerable<object[]> GetVolumeSum()
            {
                yield return new object[] { _componentData3[0], 2000.0 };
                yield return new object[] { _componentData3[4], 339.0 };
            }

            public static IEnumerable<object[]> GetForcedMultiplier()
            {
                _componentData3[0].Add(_componentData3[1]);
                _componentData3[1].Add(_componentData3[2]);
                _componentData3[1].Add(_componentData3[3]);
                _componentData3[1].Add(_componentData3[4]);
                _componentData3[0].Add(_componentData3[5]);
                _componentData3[5].Add(_componentData3[6]);
                _componentData3[5].Add(_componentData3[7]);
                _componentData3[5].Add(_componentData3[8]);

                // double output
                yield return new object[] { _componentData3[1], 2.0 };
                // no subcomponents therefore always 1
                yield return new object[] { _componentData3[2], 1.0 };
                yield return new object[] { _componentData3[2], 1.0 };
                yield return new object[] { _componentData3[3], 1.0 };
                // tripple output
                yield return new object[] { _componentData3[5], 2.0 };
                // half needed
                yield return new object[] { _componentData3[0], 0.5 };
            }

            public static IEnumerable<object[]> GetSimpleBestPriceSum()
            {
                _componentData3[0].Add(_componentData3[1]);
                _componentData3[1].Add(_componentData3[2]);
                _componentData3[1].Add(_componentData3[3]);
                _componentData3[1].Add(_componentData3[4]);
                _componentData3[0].Add(_componentData3[5]);
                _componentData3[5].Add(_componentData3[6]);
                _componentData3[5].Add(_componentData3[7]);
                _componentData3[5].Add(_componentData3[8]);

                yield return new object[] { _componentData3[1], 13.0 };
                yield return new object[] { _componentData3[5], 3.0 };
                yield return new object[] { _componentData3[0], 32.0 };
            }

            public static IEnumerable<object[]> GetBestPriceSum()
            {
                _componentData4[0].Add(_componentData4[1]);
                _componentData4[1].Add(_componentData4[2]);
                _componentData4[1].Add(_componentData4[3]);
                _componentData4[1].Add(_componentData4[4]);
                _componentData4[0].Add(_componentData4[5]);
                _componentData4[5].Add(_componentData4[6]);
                _componentData4[5].Add(_componentData4[7]);
                _componentData4[5].Add(_componentData4[8]);

                yield return new object[] { _componentData4[1], 113.0 };
                yield return new object[] { _componentData4[5], 3.0 };
                yield return new object[] { _componentData4[0], 432.0 };
            }

            public static IEnumerable<object[]> GetIsProducing()
            {
                _componentData3[0].Add(_componentData3[1]);
                _componentData3[1].Add(_componentData3[2]);
                _componentData3[1].Add(_componentData3[3]);
                _componentData3[1].Add(_componentData3[4]);
                _componentData3[0].Add(_componentData3[5]);
                _componentData3[5].Add(_componentData3[6]);
                _componentData3[5].Add(_componentData3[7]);
                _componentData3[5].Add(_componentData3[8]);

                yield return new object[] { _componentData3[1], true };
                yield return new object[] { _componentData3[5], false };
            }
            */
        }
}
