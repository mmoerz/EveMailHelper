namespace EveMailHelper.DataModels
{
    public partial class EveMail
    {
        public EveMail()
        {
            SentTo = new HashSet<EveMailSentTo>();
            Recipients = new HashSet<EveMailRecipient>();
            Labels = new HashSet<EveMailLabel>();
            CreatedDate = DateTime.Now;
        }

        public Guid Id { get; set; }
        public Guid FromId { get; set; }

        /// <summary>
        /// this id comes from EvE (Rest API)
        /// </summary>
        public long? EveMailId { get; set; }

        public bool IsRead { get; set; }

        public string Subject { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedDate { get; set; }

        public Guid? EveMailTemplateId { get; set; }
        public virtual EveMailTemplate? EveMailTemplate { get; set; } = null!;

        public Character From { get; set; } = null!;
        public virtual ICollection<EveMailSentTo> SentTo { get; set; }
        public virtual ICollection<EveMailRecipient> Recipients { get; set; }
        public virtual ICollection<EveMailLabel> Labels { get; set; }
    }
}
