using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.ServiceLayer.Models
{
    public class ProductionPlan
    {
        // hmm, ideen>
        // 'filter' oder 'limitierung' fuer bestimmte 'activities'
        public ProductionPlan() { }

        public string BlueprintName 
        { get { return Blueprint.Type.TypeName; } }
        public IndustryBlueprint Blueprint { get; set; } = null!;

        public EveType Product { get; set; } = null!;
        public int ProductQuantity { get; set; }
        public double ProductPrice { get; set; }
        public double ProductPriceSum 
        { get { return ProductPrice * ProductQuantity; } }
        public double ComponentBestPriceSum
        { get { return BlueprintComponents.BestPriceSum(); } }

        public BlueprintComponents BlueprintComponents { get; set; } = new BlueprintComponents();

        /// <summary>
        /// gets the price sum for the components for the given 'depth' in the tree of components
        /// </summary>
        /// <remarks>
        /// a depth 1 only gets the price for immediate components, depth 2 uses other blueprints
        /// in order to create the components of first tree level, and so on.
        /// </remarks>
        /// <param name="level"></param>
        /// <returns></returns>
        public double ComponentPriceSumDepth(int depth)
        {
            return BlueprintComponents.BestPriceSumWithDepthLimit(depth);
        }

        

    }
}
