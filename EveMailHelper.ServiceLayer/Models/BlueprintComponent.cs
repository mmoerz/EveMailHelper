using EveMailHelper.DataModels.Sde;

using static MudBlazor.Icons;

namespace EveMailHelper.ServiceLayer.Models
{
    public class BlueprintComponent
    {
        public BlueprintComponent() 
        { 
            SubComponents = new List<BlueprintComponent>();
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
        public int ProductionDepth { get; set; }

        public string Name { get; set; } = null!;
        public double Volume { get; set; }
        public int Quantity { get; set; }
        public double PricePerUnit { get; set; }
        public double PriceSum { get; set; }

        public int QuantityFromBlueprint { get; set; }

        public double ForcedQuantityMultiplier 
        {
            get
            {
                return (double)QuantityFromBlueprint / (double)Quantity;
            }
        }

        public ICollection<BlueprintComponent> SubComponents { get; set; }
    }
}
