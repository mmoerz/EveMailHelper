using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EveMailHelper.ServiceLayer.Models;

namespace EveMailHelper.ServiceLayer.Utilities
{
    public class BlueprintAnalyzer
    {
        BlueprintComponent component;
        double materialModifier;
        public BlueprintAnalyzer(BlueprintComponent component, double materialModifier)
        {
            this.component = component;
            this.materialModifier = materialModifier;
        }

        public int ModifiedQuantity()
        {
            double modifier = component.Quantity / 100 * materialModifier;
            return (int)Math.Ceiling(component.Quantity + modifier);
        }

        public double PriceSum()
        {
            return ModifiedQuantity() * component.PricePerUnit;
        }

        public double VolumeSum()
        {
            return ModifiedQuantity() * component.Volume;
        }

        public double BestPriceSum()
        {
            double sum = 0;
            // no subcomponents, so nothing to produce and no job costs
            if (component.SubComponents.Count == 0)
                return PriceSum();

            sum = component.JobCost;
            foreach (var subComponent in component.SubComponents)
            {
                BlueprintAnalyzer blueprintAnalyzer = new(subComponent, materialModifier);
                sum += blueprintAnalyzer.BestPriceSum();
            }
            sum /= component.ForcedQuantityMultiplier;
            // not sure what is better in case of cheaper buying
            // to return a single 'batch' (or the full 2 batches)
            if (sum > PriceSum())
                sum = PriceSum();
            return sum;
        }

        public bool IsProducingComponentBetter()
        {
            return BestPriceSum() < PriceSum();
        }

        public bool IsBuyingComponentBetter()
        {
            return !IsProducingComponentBetter();
        }

        public List<double> GetForcedMultipliers(
            BlueprintComponent component, bool onlyUseBestPricePath)
        {
            var result = new List<double>();
            if (!onlyUseBestPricePath || IsProducingComponentBetter())
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
