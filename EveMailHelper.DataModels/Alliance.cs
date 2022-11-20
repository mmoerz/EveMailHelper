

using EveMailHelper.DataModels.Interfaces;

namespace EveMailHelper.DataModels
{
    public class Alliance : IBaseEveId
    {
        public Alliance()
        {
            Corporations = new HashSet<Corporation>();
        }

        public Guid Id { get; set; }
        public int EveId { get; set; }
        public string Name { get; set; } = null!;
        public Guid CreatorCorporationId { get; set; }
        public Guid CreatorId { get; set; }
        public DateTime? DateFounded { get; set; }
        public Guid? ExecutorCorporationId { get; set; }
        public string Ticker { get; set; } = null!;

        
        public virtual Corporation CreatorCorporation { get; set; } = null!;
        public virtual Character Creator { get; set; } = null!;
        public virtual Corporation ExecutorCorporation { get; set; } = null!;
        public virtual ICollection<Corporation> Corporations { get; set; } 
    }
}
