using System.Collections;
using System.ComponentModel;

using EveMailHelper.DataModels.Sde;

using EVEStandard.Models;

using MudBlazor.Services;

using static MudBlazor.Icons;

namespace EveMailHelper.ServiceLayer.Models
{
    
    //public class BlueprintComponentTreeNode : BlueprintComponent, IBlueprintComponentTreeNode, IEnumerable
    //{
    //    public BlueprintComponentTreeNode(BlueprintComponentTreeNode? parent, double materialConsumptionModifier)
    //    {
    //        this.parent = parent;
    //        if (parent == null)
    //        {
    //            productionDepth = 1;
    //        }
    //        else
    //        {
    //            productionDepth = parent.productionDepth +1;
    //            parent.SubComponents.Add(this);
    //        }
    //        this.materialConsumptionModifier = materialConsumptionModifier;
    //        SubComponents = new List<IBlueprintComponentTreeNode>();
    //    }

    //    /// <summary>
    //    /// depth in the hierachy of the product chain.
    //    /// </summary>
    //    /// <remarks>
    //    /// blueprint is zero, direct materials of blueprint are 1, ...
    //    /// </remarks>
    //    public readonly int productionDepth;
    //    public readonly double materialConsumptionModifier;
    //    public readonly BlueprintComponentTreeNode? parent;

    //    #region properties

    //    private bool _isIncludedInProdcutionPath = false;
    //    /// <summary>
    //    /// this node is within (true), or excluded from production. Deciding factor is the bestprice.
    //    /// </summary>
    //    public bool IsIncludedInProductionPath
    //    { get { return _isIncludedInProdcutionPath; } }

    //    public bool IsEndNode
    //    { get { return SubComponents.Count() == 0; } }
        
    //    public double ForcedQuantityMultiplier
    //    {
    //        get
    //        {
    //            if (Quantity == 0)
    //                throw new Exception("Quantity must not be 0.");
                
    //            if (SubComponents.Count() == 0)
    //                return 1.0;
                
    //            if (QuantityFromBlueprint == 0)
    //                throw new Exception("QuantityFromBlueprint must not be 0.");

    //            return (double)QuantityFromBlueprint / (double)Quantity;
    //        }
    //    }

    //    public double SubComponentCosts;

    //    #endregion

    //    public double ProductionCost()
    //    {
    //        return JobCost + SubComponentCosts;
    //    }

    //    public int ModifiedQuantity()
    //    {
    //        if (Quantity == 0)
    //            throw new Exception("Quantity must not be 0.");
    //        double modifier = Quantity / 100 * materialConsumptionModifier;
    //        return (int)Math.Ceiling(Quantity + modifier);
    //    }

    //    public double BuyPriceSum()
    //    {
    //        return ModifiedQuantity() * PricePerUnit;
    //    }

    //    public double VolumeSum()
    //    {
    //        return ModifiedQuantity() * Volume;
    //    }

    //    public double ComponentCost()
    //    {
    //        if (IsEndNode)
    //        {
    //            return BuyPriceSum();
    //        }
    //        if (SubComponentCosts == 0)
    //            throw new Exception("zero Componentcosts is invalid.");
    //        if (IsProduced()) // producing is cheaper
    //        {
    //            return ProductionCost();
    //        }
    //        return BuyPriceSum();
    //    }

    //    public bool IsProduced()
    //    {
    //        return (ProductionCost() < BuyPriceSum());
    //    }

    //    public void SetIncludedInProductionPath(bool value)
    //    {
    //        _isIncludedInProdcutionPath = value;
    //    }

    //    public void CopyDeep(BlueprintComponent source)
    //    {
    //        _ = source ?? throw new ArgumentNullException(nameof(source));

    //        this.EveType = source.EveType;
    //        this.Name = source.Name;
    //        this.Volume = source.Volume;
    //        this.Quantity = source.Quantity;
    //        this.PricePerUnit = source.PricePerUnit;
    //        this.QuantityFromBlueprint = source.QuantityFromBlueprint;
    //        this.JobCost = source.JobCost;

    //        foreach (var item in source.SubComponents)
    //        {
    //            var newNode = new BlueprintComponentTreeNode(this, materialConsumptionModifier);
    //            BlueprintComponent? sourceItem = item as BlueprintComponent;
    //            if (sourceItem != null) {
    //                newNode.CopyDeep(sourceItem);
    //            }
    //        }
    //    }

    //    public void InitializeTree()
    //    {
    //        if (parent != null)
    //            throw new Exception("can only initialize starting at the root node");

    //        ResetTreeNode(this);
    //        foreach (var node in this)
    //        {
    //            if (node.parent != null)
    //            {
    //                if (node.IsProduced())
    //                {
    //                    node.parent.SubComponentCosts += node.SubComponentCosts;
    //                    node.parent.SubJobCosts += node.SubJobCosts + JobCost;
    //            }

    //                node.parent.SubComponentCosts += node.BuyPriceSum();
    //            }
    //        }
    //    }

    //    protected void ResetTreeNode(BlueprintComponentTreeNode node)
    //    {
    //        node.SubComponentCosts = 0;
    //        foreach(var child in node.SubComponents)
    //        {
    //            var childNode = child as BlueprintComponentTreeNode;
    //            if (childNode == null)
    //                throw new Exception($"node found that is not of type {typeof(BlueprintComponentTreeNode)}");
    //            ResetTreeNode(childNode);
    //        }
    //    }

    //    #region Iterator
    //    public IEnumerator<BlueprintComponentTreeNode> GetEnumerator()
    //    {
    //        return new BlueprintComponentTreeIterator(this);
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return new BlueprintComponentTreeIterator(this);
    //    }
    //    #endregion
    //}
}
