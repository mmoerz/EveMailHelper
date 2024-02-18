using System.Collections;

using EveMailHelper.DataModels.Dto;
using EveMailHelper.DataModels.Sde;

using EVEStandard.Models;

using MudBlazor.Services;

using static MudBlazor.Icons;

namespace EveMailHelper.ServiceLayer.Models
{
    /// <summary>
    /// a simple tree representing the buildplan and subbuildplans for an activity of a blueprint
    /// </summary>
    public class BlueprintComponent 
    {
        public BlueprintComponent() 
        {
            SubComponents = new List<BlueprintComponent>();
        }

        public BlueprintComponent(BlueprintComponent? parent) 
        {
            SetParent(parent);
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
        public SellBuyPriceDTO PricePerUnit { get; set; }
        public int QuantityFromBlueprint { get; set; }
        public double JobCost { get; set; }
        public IList<BlueprintComponent> SubComponents { get; set; }

        private BlueprintComponent? _parent = null;
        public BlueprintComponent? Parent
        { get { return _parent; } }

        protected void SetParent(BlueprintComponent? parent)
        {
            _parent = parent;
        }

        /// <summary>
        /// depth in the hierachy of the product chain.
        /// </summary>
        /// <remarks>
        /// blueprint is zero, direct materials of blueprint are 1, ...
        /// </remarks>
        public virtual int ProductionDepth
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
                if (Quantity == 0)
                    throw new Exception("Quantity must not be 0.");

                if (SubComponents.Count() == 0)
                    return 1.0;

                if (QuantityFromBlueprint == 0)
                    throw new Exception("QuantityFromBlueprint must not be 0.");

                return (double)QuantityFromBlueprint / (double)Quantity;
            }
        }

        //protected BlueprintComponent? _parent;
        //public BlueprintComponent Parent
        //{ get { return _parent; } }
        #endregion


        //public void SetParent(BlueprintComponent component)
        //{
        //    _parent = component;
        //}

        public void Add(BlueprintComponent component)
        {
            var comp = component; //as IBlueprintComponentTreeNode;
            if (comp == null)
                throw new ArgumentException($"{nameof(component)} is not of type IBlueprintComponentTree");
            if (SubComponents.Contains(comp))
                return;

            comp._parent = this;
            SubComponents.Add(comp);
        }

        public void AddRange(IList<BlueprintComponent> components)
        {
            foreach(var item in components)
            {
                Add(item);
            }
        }
    }
}
