﻿using System;
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
    public class ProductionPlan : IEnumerable<BlueprintComponent>
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
        public int ProductQuantity {
            get { return Root.Quantity; } 
            set { 
                Root.Quantity = value;
                Root.QuantityFromBlueprint = value;
            }
        }
        public double ProductPricePerUnit { 
            get { return Root.PricePerUnit; }
            set { Root.PricePerUnit = value; } 
        }
        public double JobCost { 
            get { return Root.JobCost; }
            set { Root.JobCost = value; } 
        }
        public BlueprintComponent Root { get; set; } = new();
        #endregion

        public void ShallowCopy(ProductionPlan plan)
        {
            Blueprint = plan.Blueprint;
            Product = plan.Product;
            ProductQuantity = plan.ProductQuantity;
            ProductPricePerUnit = plan.ProductPricePerUnit;
            JobCost = plan.JobCost;
            Root = plan.Root;
        }      

        public void Add(BlueprintComponent component)
        {
            Root.Add(component);
        }

        public IEnumerator<BlueprintComponent> GetEnumerator()
        {
            return new ProductionPlanIterator(Root);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ProductionPlanIterator(Root);
        }
    }
}
