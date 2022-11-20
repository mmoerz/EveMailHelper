namespace EveMailHelper.DataModels
{
    public partial class EveMailTemplate
    {
        public EveMailTemplate()
        {
            EveMailsGenerated = new HashSet<Mail>();
            CreatedDate = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string Subject { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Mail> EveMailsGenerated { get; set; }
    }
}
