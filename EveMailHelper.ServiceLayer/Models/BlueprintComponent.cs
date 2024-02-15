using System.Collections;

using EveMailHelper.DataModels.Sde;

using EVEStandard.Models;

using MudBlazor.Services;

using static MudBlazor.Icons;

namespace EveMailHelper.ServiceLayer.Models
{
    
    public class BlueprintComponent : IBlueprintComponentTree
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
                int depth = 1;
                if (Parent != null)
                    depth = Parent.ProductionDepth + 1;

                return depth;
            }
        }
        
        // killed for good because, they are modified by the facilities material modifier
        // and therefore useless here
        //public double PriceSum
        //{ get { return Quantity * PricePerUnit; } }
        //public double VolumeSum
        //{ get { return Volume * Quantity; } }
        
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
    }
}
