using System.Collections;

using EveMailHelper.DataModels.Sde;

using EVEStandard.Models;

using MudBlazor.Services;

using static MudBlazor.Icons;

namespace EveMailHelper.ServiceLayer.Models
{
    
    public class BlueprintComponent : IEnumerable<BlueprintComponent>, IBlueprintComponentTree
    {
        public BlueprintComponent()
        {
            SubComponents = new List<BlueprintComponent>();
        }

        #region properties
        /// <summary>
        /// eveId for further querying the ESI API
        /// </summary>
        public EveType EveType { get; set; } = null!;
        public string Name { get; set; } = null!;
        public double Volume { get; set; }
        public int Quantity { get; set; }
        public double PricePerUnit { get; set; }
        public int QuantityFromBlueprint { get; set; }
        public double JobCost { get; set; }
        public IList<BlueprintComponent> SubComponents { get; set; }
        /// <summary>
        /// depth in the hierachy of the product chain.
        /// </summary>
        /// <remarks>
        /// blueprint is zero, direct materials of blueprint are 1, ...
        /// </remarks>
        public int ProductionDepth
        {
            get
            {
                int depth = 0;
                if (Parent != null)
                    depth = Parent.ProductionDepth + 1;

                return depth;
            }
        }
                
        public double PriceSum
        { get { return Quantity * PricePerUnit; } }
        public double VolumeSum
        { get { return Volume * Quantity; } }
        public bool IsBuyingBetter
        { get { return !IsProducingBetter; } }
        public bool IsProducingBetter
        { get { return BestPriceSum() < PriceSum; } }

        public double ForcedQuantityMultiplier
        {
            get
            {
                if (SubComponents.Count() == 0)
                    return 1.0;

                return Quantity != 0 ?
                    (double)QuantityFromBlueprint / (double)Quantity
                    : 1.0;
            }
        }

        protected IBlueprintComponentTree? _parent;
        public IBlueprintComponentTree? Parent
        { get { return _parent; } }
        #endregion

        public IEnumerator<BlueprintComponent> GetEnumerator()
        {
            return new BlueprintComponentIterator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new BlueprintComponentIterator(this);
        }

        public void SetParent(IBlueprintComponentTree component)
        {
            _parent = component;
        }

        public void Add(BlueprintComponent component)
        {
            if (SubComponents.Contains(component))
                return;

            component.SetParent(this);
            SubComponents.Add(component);
        }

        public double BestPriceSumWithDepthLimit(int depth)
        {
            double sum = 0;
            if (SubComponents.Count == 0)
                return PriceSum;

            if (depth > 1)
            {
                foreach (var component in SubComponents)
                {
                    sum += component.BestPriceSumWithDepthLimit(depth - 1);
                }
            }
            if (sum > PriceSum)
                sum = PriceSum;
            return sum;
        }

        public double BestPriceSum()
        {
            double sum = 0;
            // no subcomponents, so nothing to produce and no job costs
            if (SubComponents.Count == 0)
                return PriceSum;

            sum = JobCost;
            foreach (var component in SubComponents)
            {
                sum += component.BestPriceSum();
            }
            sum /= ForcedQuantityMultiplier;
            // not sure what is better in case of cheaper buying
            // to return a single 'batch' (or the full 2 batches)
            if (sum > PriceSum )
                sum = PriceSum;
            return sum;
        }

        public double BestPriceVolumeSum()
        {
            double volume = 0;
            if (IsProducingBetter)
            {
                foreach (var component in SubComponents)
                {
                    volume += component.BestPriceVolumeSum();
                }
            }
            else
                volume += VolumeSum;
            return volume;
        }

        public List<double> GetForcedMultipliers()
        {
            var result = new List<double>();
            if (IsProducingBetter)
                result.Add(ForcedQuantityMultiplier);

            foreach (var component in SubComponents)
            {
                var subresult = component.GetForcedMultipliers();
                result.AddRange(subresult);
            }
            return result.Distinct().ToList();
        }

        
    }
}
