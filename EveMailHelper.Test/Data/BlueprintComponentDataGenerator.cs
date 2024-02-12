using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

using EveMailHelper.ServiceLayer.Models;

namespace EveMailHelper.Test.Data
{
    public class BlueprintComponentDataGenerator
    {
        public static readonly List<BlueprintComponents> _componentData1 = new()
        {
            new()
            {
                Name = "TopLevel",
                Quantity = 200,
                PricePerUnit = 300,
                QuantityFromBlueprint = 200,
                SubComponents = new List<BlueprintComponents>
                {
                    new()
                    {
                        Name = "L1 item1",
                        Quantity = 10,
                        PricePerUnit = 30,
                        QuantityFromBlueprint = 20,
                        SubComponents = new List<BlueprintComponents>
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
                        QuantityFromBlueprint = 2,
                        SubComponents = new List<BlueprintComponents>
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

        public static readonly List<BlueprintComponents> _componentData2 = new()
        {
            new()
            {
                Name = "TopLevel",
                Quantity = 200,
                PricePerUnit = 300,
                QuantityFromBlueprint = 200,
            }
        };

        public static readonly List<BlueprintComponents> _componentData3 = new()
        {
            new()
            {
                Name = "TopLevel",
                Quantity = 200,
                PricePerUnit = 300,
                Volume = 10,
                QuantityFromBlueprint = 200,
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
                                QuantityFromBlueprint = 2,
                                Name = "L2-1 item1"
                            },
                            new()
                            {
                                Quantity = 2,
                                PricePerUnit = 4,
                                Volume = 112,
                                QuantityFromBlueprint = 4,
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

        public static readonly List<BlueprintComponents> Enumerated1 = new()
        {
            _componentData1[0].SubComponents[0],
            _componentData1[0].SubComponents[0].SubComponents[0],
            _componentData1[0].SubComponents[0].SubComponents[1],
            _componentData1[0].SubComponents[0].SubComponents[2],
            _componentData1[0].SubComponents[1],
            _componentData1[0].SubComponents[1].SubComponents[0],
            _componentData1[0].SubComponents[1].SubComponents[1],
            _componentData1[0].SubComponents[1].SubComponents[2],
        };

        public static readonly List<Tuple<int, BlueprintComponents>> selection1 = new()
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

        public static IEnumerable<object[]> GetBestPriceSum()
        {
            _componentData3[0].Add(_componentData3[1]);
            _componentData3[1].Add(_componentData3[2]);
            _componentData3[1].Add(_componentData3[3]);
            _componentData3[1].Add(_componentData3[4]);
            _componentData3[0].Add(_componentData3[5]);
            _componentData3[5].Add(_componentData3[6]);
            _componentData3[5].Add(_componentData3[7]);
            _componentData3[5].Add(_componentData3[8]);

            yield return new object[] { _componentData3[1], 26.0 };
            yield return new object[] { _componentData3[5], 3.0 };
            yield return new object[] { _componentData3[0], 29.0 };
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
    }
}
