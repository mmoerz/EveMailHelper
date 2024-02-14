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

        public static IEnumerable<object[]> GetMinimumNumberOfRuns()
        {
            var data = new List<ITheoryData>();

            var data1 = TestData2_Cheap[0];
            foreach (var item in BlueprintComponentDataGenerator.TestData3)
                data1.Add(item);
            ProductionPlanAnalyzer sut1 = new(data1);
            data.Add(TheoryData.Factory(sut1, 1, "only integers of forcedRunnumbers should be counted."));

            var data2 = TestData2_Expensive[0];
            foreach (var item in BlueprintComponentDataGenerator.TestData4)
                data2.Add(item);
            ProductionPlanAnalyzer sut2 = new(data2);
            data.Add(TheoryData.Factory(sut2, 4, "ignore forcedRunNumbers that are a multiple of bigger forcedRunNumbers"));

            var data3 = TestData2_Expensive[0];
            data3.Add(BlueprintComponentDataGenerator.TestData5[0]);
            ProductionPlanAnalyzer sut3 = new(data3);
            data.Add(TheoryData.Factory(sut3, 12, "ignore forcedRunNumbers that are a multiple of bigger forcedRunNumbers"));

            var data4 = TestData2_Expensive[0];
            foreach (var item in BlueprintComponentDataGenerator.TestData5)
                data4.Add(item);
            ProductionPlanAnalyzer sut4 = new(data4);
            data.Add(TheoryData.Factory(sut4, 12, "ignore forcedRunNumbers that are a multiple of bigger forcedRunNumbers"));
            
            return data.ConvertAll(d => d.ToParameterArray());
        }

        public static IEnumerable<object[]> GetMinimumNumberOfRunsBestPrice()
        {
            var data = new List<ITheoryData>();

            var data1 = TestData2_Cheap[0];
            foreach (var item in BlueprintComponentDataGenerator.TestData3)
                data1.Add(item);
            ProductionPlanAnalyzer sut1 = new(data1);
            data.Add(TheoryData.Factory(sut1, 1, "only integers of forcedRunnumbers should be counted."));

            var data2 = TestData2_Expensive[0];
            foreach (var item in BlueprintComponentDataGenerator.TestData4)
                data2.Add(item);
            ProductionPlanAnalyzer sut2 = new(data2);
            data.Add(TheoryData.Factory(sut2, 4, "ignore forcedRunNumbers that are a multiple of bigger forcedRunNumbers"));

            var data3 = TestData2_Expensive[0];
            data3.Add(BlueprintComponentDataGenerator.TestData5[0]);
            ProductionPlanAnalyzer sut3 = new(data3);
            data.Add(TheoryData.Factory(sut3, 4, "ignore forcedRunNumbers that are a multiple of bigger forcedRunNumbers"));

            //var data4 = TestData2_Expensive[0];
            //foreach (var item in BlueprintComponentDataGenerator.TestData5)
            //    data4.Add(item);
            //ProductionPlanAnalyzer sut4 = new(data2);
            //data.Add(TheoryData.Factory(sut4, 12, "ignore forcedRunNumbers that are a multiple of bigger forcedRunNumbers"));

            return data.ConvertAll(d => d.ToParameterArray());
        }
    }
}
