namespace EveMailHelper.DataModels
{
    public partial class Character
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
        public string Name { get; set; } = null!;
        public int? Age { get; set; } = null!;
        public int? ReallifeAge { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public bool IsExcluded { get; set; } = false;
        public bool IsInRecruitment { get; set; } = false;

        public CharacterStatus Status { get; set; } = CharacterStatus.None;
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<EveMailSentTo> EveMailReceived { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
}
