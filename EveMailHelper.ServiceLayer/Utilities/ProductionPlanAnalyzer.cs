using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EveMailHelper.ServiceLayer.Models;

namespace EveMailHelper.ServiceLayer.Utilities
{
    public class ProductionPlanAnalyzer
    {
        ProductionPlan _plan;

        public ProductionPlanAnalyzer(ProductionPlan plan) 
        { 
            _plan = plan;
        }

        public double ComponentBestPriceSum()
        {
            double subBestPrice = 0.0;
            foreach (var component in _plan.SubComponents)
            {
                subBestPrice += BlueprintAnalyzer.BestPriceSum(component);
            }
            return subBestPrice;
        }

        public double BestPriceSum()
        {
            var JobAndComponentPrice = _plan.JobCost + ComponentBestPriceSum();
            if (JobAndComponentPrice < _plan.ProductPriceSum)
                return JobAndComponentPrice;
            return _plan.ProductPriceSum;
        }

        public bool IsProducingBetter()
        {
            return BestPriceSum() < _plan.PriceSum;
        }

        public bool IsBuyingBetter() 
        {
            return !IsProducingBetter();
        }

        /// <summary>
        /// get the minimum number of runs from the component tree
        /// </summary>
        /// <param name="onlyUseBestPricePath">
        /// only account the best price path, otherwise everything is 'built'
        /// </param>
        /// <returns>minimum number of runs</returns>
        public int GetMinNumberOfRuns(bool onlyUseBestPricePath)
        {
            var ForcedMultiplierList = new List<double>();
            BlueprintAnalyzer analyzer = new BlueprintAnalyzer();

            if (!onlyUseBestPricePath || IsProducingBetter())
            {
                foreach (var component in _plan.SubComponents)
                {
                    ForcedMultiplierList.AddRange(
                        BlueprintAnalyzer.GetForcedMultipliers(component, onlyUseBestPricePath));
                }
            }

            return NormalizeNumberOfRuns(ForcedMultiplierList);
        }

        public int NormalizeNumberOfRuns(IList<double> ForcedMultiplierList)
        {
            int MinimumNumberOfRuns = 1;
            List<int> FullnumbersOnly = new();
            foreach (var item in ForcedMultiplierList)
            {
                if (item >= 1)
                    FullnumbersOnly.Add((int)item);
            }
            var distinct = FullnumbersOnly.Distinct().ToList();

            // try to find a bigger multiplier that contains the smaller number of runs
            // (can be divided without rest)
            int i = 0;
            bool canBeRemoved;
            while (i < distinct.Count())
            {
                var item = distinct[i];
                canBeRemoved = false;

                foreach (var biggerNumber in distinct.Where(y => y > item))
                {
                    if (biggerNumber % item == 0)
                    {
                        canBeRemoved = true;
                        // automatically 'move' to the next entry
                        break;
                    }
                }
                if (canBeRemoved)
                {
                    distinct.Remove(item);
                    continue;
                }
                i++;
            }

            foreach (var item in distinct)
            {
                MinimumNumberOfRuns *= item;
            }
            return MinimumNumberOfRuns;
        }

        public bool NumberOfRunsIsValid(
            int NumberOfRuns, bool onlyUseBestPricePath, out int minimumNumberOfRuns)
        {
            minimumNumberOfRuns = GetMinNumberOfRuns(onlyUseBestPricePath);
            var modulo = NumberOfRuns % minimumNumberOfRuns;
            if (modulo > 0)
                return false;
            return true;
        }
    }
}
