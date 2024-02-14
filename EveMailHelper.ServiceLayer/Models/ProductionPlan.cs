using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EveMailHelper.DataModels.Sde;
using EveMailHelper.ServiceLayer.Utilities;

using EVEStandard.Models;

using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace EveMailHelper.ServiceLayer.Models
{
    public class ProductionPlan : IEnumerable<BlueprintComponent>, IBlueprintComponentTree
    {
        // hmm, ideen>
        // todo: 'filter' oder 'limitierung' fuer bestimmte 'activities'
        public ProductionPlan() { }

        #region properties
        public string BlueprintName 
        { get { return Blueprint.Type.TypeName; } }
        public string ProductName
        { get { return Product?.TypeName ?? string.Empty; } }
        public double ProductVolume
        { get { return Product?.Volume ?? 0.0; } }
        public double ProductPriceSum
        { get { return ProductPricePerUnit * ProductQuantity; } }
        public int ProductionDepth
        { get { return 0; } }
        public double PriceSum
        { get { return ProductQuantity * ProductPricePerUnit; } }

        public IndustryBlueprint Blueprint { get; set; } = null!;
        public IndustryActivity Activity { get; set; } = null!;
        public EveType? Product { get; set; } = null;
        public int ProductQuantity { get; set; }
        public double ProductPricePerUnit { get; set; }
        public double JobCost { get; set; }
        public IList<BlueprintComponent> SubComponents { get; set; } = new List<BlueprintComponent>();
        #endregion

        public void ShallowCopy(ProductionPlan plan)
        {
            Blueprint = plan.Blueprint;
            Product = plan.Product;
            ProductQuantity = plan.ProductQuantity;
            ProductPricePerUnit = plan.ProductPricePerUnit;
            JobCost = plan.JobCost;
            SubComponents = plan.SubComponents;
        }      

        public void Add(BlueprintComponent component)
        {
            component.SetParent(this);
            SubComponents.Add(component);
        }

        public IEnumerator<BlueprintComponent> GetEnumerator()
        {
            return new ProductionPlanIterator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ProductionPlanIterator(this);
        }
    }
}
