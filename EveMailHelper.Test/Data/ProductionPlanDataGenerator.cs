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
                        QuantityFromBlueprint = 2,
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
            var i = 1 + 1;
            i += 2;

            yield return new object[] { _prodPlanData1[0], Enumerated1 };
        }
    }
}
