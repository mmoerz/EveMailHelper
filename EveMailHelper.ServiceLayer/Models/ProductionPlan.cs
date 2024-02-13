﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EveMailHelper.DataModels.Sde;

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
        { get { return ProductPrice * ProductQuantity; } }
        public int ProductionDepth
        { get { return 0; } }

        public IndustryBlueprint Blueprint { get; set; } = null!;
        public EveType? Product { get; set; } = null;
        public int ProductQuantity { get; set; }
        public double ProductPrice { get; set; }
        public double JobCost { get; set; }
        public IList<BlueprintComponent> SubComponents { get; set; } = new List<BlueprintComponent>();
        #endregion

        public void ShallowCopy(ProductionPlan plan)
        {
            Blueprint = plan.Blueprint;
            Product = plan.Product;
            ProductQuantity = plan.ProductQuantity;
            ProductPrice = plan.ProductPrice;
            JobCost = plan.JobCost;
            SubComponents = plan.SubComponents;
        }

        public double ComponentBestPriceSum
        { 
            get {
                double subBestPrice = 0.0;
                foreach (var component in SubComponents)
                {
                    subBestPrice += component.BestPriceSum();
                }
                return subBestPrice;
            }
        }
        public double BestPriceSum
        {
            get
            {
                if (ComponentBestPriceSum < ProductPriceSum)
                    return ComponentBestPriceSum;
                return ProductPriceSum;
            }
        }

        public void Add(BlueprintComponent component)
        {
            component.SetParent(this);
            SubComponents.Add(component);
        }

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
            throw new NotImplementedException();
            //return BlueprintComponents.BestPriceSumWithDepthLimit(depth);
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
