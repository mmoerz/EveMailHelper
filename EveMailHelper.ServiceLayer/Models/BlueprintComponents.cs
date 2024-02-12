using System.Collections;

using EveMailHelper.DataModels.Sde;

using static MudBlazor.Icons;

namespace EveMailHelper.ServiceLayer.Models
{
    public class BlueprintComponents : IEnumerable<BlueprintComponents>
    {
        public BlueprintComponents() 
        { 
            SubComponents = new List<BlueprintComponents>();
        }

        /// <summary>
        /// eveId for further querying the ESI API
        /// </summary>
        public int EveId { get; set; } = -1;
        /// <summary>
        /// depth in the hierachy of the product chain.
        /// </summary>
        /// <remarks>
        /// blueprint is zero, direct materials of blueprint are 1, ...
        /// </remarks>
        public int ProductionDepth { 
            get
            {
                int depth = 0;
                if (Parent != null)
                    depth = Parent.ProductionDepth + 1;
                
                return depth;
            } 
        }

        public string Name { get; set; } = null!;
        public double Volume { get; set; }
        public int Quantity { get; set; }
        public double PricePerUnit { get; set; }
        public double PriceSum 
        { get { return Quantity * PricePerUnit; } }

        public double VolumeSum
        { get { return Volume * Quantity; } }

        public int QuantityFromBlueprint { get; set; }

        public double ForcedQuantityMultiplier 
        {
            get
            {
                return Quantity != 0 ?
                    (double)QuantityFromBlueprint / (double)Quantity
                    : 0;
            }
        }

        protected BlueprintComponents? _parent; 
        public BlueprintComponents? Parent 
        { get { return _parent; } }

        public IList<BlueprintComponents> SubComponents { get; set; }

        public IEnumerator<BlueprintComponents> GetEnumerator()
        {
            return new BlueprintComponentIterator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new BlueprintComponentIterator(this);
        }

        protected void SetParent(BlueprintComponents component)
        {
            _parent = component;
        }

        public void Add(BlueprintComponents component)
        {
            component.SetParent(this);
            SubComponents.Add(component);
        }

        protected int RecursiveProductionDepth()
        {
            int depth = 0;
            if (Parent != null)
            {
                depth = Parent.RecursiveProductionDepth() + 1;
            }
            return depth;
        }

        public bool IsBuyingBetter
        { get { return BestPriceSum() >= PriceSum; } }

        public bool IsProducingBetter
        { get { return BestPriceSum() < PriceSum; } }

        public double BestPriceSum()
        {
            double sum = 0;
            foreach (var component in SubComponents)
            {
                sum += component.BestPriceSum();
            }
            if (sum < PriceSum)
                sum = PriceSum;
            return sum;
        }

        public double BestPriceSumWithDepthLimit(int depth)
        {

            double sum = 0;
            if (depth > 1)
            {
                foreach (var component in SubComponents)
                {
                    sum += component.BestPriceSumWithDepthLimit(depth -1);
                }
            }
            if (sum < PriceSum)
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
    }
}
