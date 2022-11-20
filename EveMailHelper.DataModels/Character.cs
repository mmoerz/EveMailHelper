using EveMailHelper.DataModels.Interfaces;
using EveMailHelper.DataModels.Security;

namespace EveMailHelper.DataModels
{
    public partial class Character : IBaseEveId, IBaseEveObject
    {
        public Character()
        {
            EveMailReceived = new HashSet<EveMailSentTo>();
            Chats = new HashSet<Chat>();
            Notes = new HashSet<Note>();
            CreatedDate = DateTime.Now;
        }

        public Guid Id { get; set; }
        /// <summary>
        /// Ingame Id
        /// </summary>
        public int EveId { get; set; }
        public DateTime EveLastUpdated { get; set; }
        /// <summary>
        /// account of this tool
        /// </summary>
        public Guid AccountId { get; set; }
        /// <summary>
        /// assigned Eve account
        /// </summary>
        public Guid EveAccountId { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; } = null!;

        public DateTime Birthday { get; set; }
        public int? ReallifeAge { get; set; } = null!;

        public Guid BloodlineId { get; set; }

        public Guid? CorporationId { get; set; }
        public string? Description { get; set; } = null!;
        public Guid? FactionId { get; set; }
        // public GenderEnum Gender { get; set; }
        public int RaceId { get; set; }
        public string? RecruitmentNote { get; set; } = null!;
        public float? SecurityStatus { get; set; }
        public string Title { get; set; } = null!;
        public bool IsExcluded { get; set; } = false;
        public bool IsInRecruitment { get; set; } = false;
        public bool EveDeletedInGame { get; set; } = false;

        public CharacterStatus Status { get; set; } = CharacterStatus.None;
        public DateTime CreatedDate { get; set; }

        public Account Account { get; set; } = null!;
        public EveAccount EveAccount { get; set; } = null!;

        public virtual ICollection<EveMailSentTo> EveMailReceived { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
        public virtual ICollection<Note> Notes { get; set; }

        public virtual Corporation? Corporation { get; set; } = null!;
    }
}
