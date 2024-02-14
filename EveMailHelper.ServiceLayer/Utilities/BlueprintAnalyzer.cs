using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EveMailHelper.ServiceLayer.Models;

namespace EveMailHelper.ServiceLayer.Utilities
{
    public class BlueprintAnalyzer
    {
        public static double BestPriceSum(BlueprintComponent component)
        {
            double sum = 0;
            // no subcomponents, so nothing to produce and no job costs
            if (component.SubComponents.Count == 0)
                return component.PriceSum;

            sum = component.JobCost;
            foreach (var subComponent in component.SubComponents)
            {
                sum += BestPriceSum(subComponent);
            }
            sum /= component.ForcedQuantityMultiplier;
            // not sure what is better in case of cheaper buying
            // to return a single 'batch' (or the full 2 batches)
            if (sum > component.PriceSum)
                sum = component.PriceSum;
            return sum;
        }

        public static bool IsProducingComponentBetter(BlueprintComponent component)
        {
            return BestPriceSum(component) < component.PriceSum;
        }

        public static bool IsBuyingComponentBetter(BlueprintComponent component)
        {
            return !IsProducingComponentBetter(component);
        }

        public static List<double> GetForcedMultipliers(
            BlueprintComponent component, bool onlyUseBestPricePath)
        {
            var result = new List<double>();
            if (!onlyUseBestPricePath || IsProducingComponentBetter(component))
            {
                result.Add(component.ForcedQuantityMultiplier);
                foreach (var subComponent in component.SubComponents)
                {
                    var subresult = GetForcedMultipliers(subComponent, onlyUseBestPricePath);
                    result.AddRange(subresult);
                }
            }
            return result.Distinct().ToList();
        }

        
    }
}
