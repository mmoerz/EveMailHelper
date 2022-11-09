namespace EveMailHelper.DataModels
{
    public partial class EveMailSentTo
    {
        public EveMailSentTo()
        {
            SentDate = DateTime.Now;
        }
        public Guid Id { get; set; }
        public Guid EveMailId { get; set; }
        public Guid CharacterId { get; set; }
        public DateTime SentDate { get; set; }

        public virtual EveMail EveMail { get; set; } = null!;
        public virtual Character Character { get; set; } = null!;

    }
}
