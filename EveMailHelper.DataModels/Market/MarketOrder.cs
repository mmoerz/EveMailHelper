using EveMailHelper.DataModels.Interfaces;
using EveMailHelper.DataModels.Sde;
using EveMailHelper.DataModels.Sde.Map;

namespace EveMailHelper.DataModels.Market
{
    public partial class MarketOrder
    {
        public long EveId { get; set; }
        public int Duration { get; set; }
        public bool IsBuyOrder { get; set; }
        public DateTime Issued { get; set; }
        public long LocationId { get; set; }
        public int MinVolume { get; set; }
        public double Price { get; set; }
        public string Range { get; set; } = string.Empty;
        public int SolarSystemId { get; set; }
        public int TypeId { get; set; }
        public int VolumeRemain { get; set; }
        public int VolumeTotal { get; set; }

        public DateTime lastUpdated { get; set; } = DateTime.Now;

        public virtual EveType EveType { get; set; } = null!;
        public virtual SolarSystem SolarSystem { get; set; } = null!;
        
    }
}
