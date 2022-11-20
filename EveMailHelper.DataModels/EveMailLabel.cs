namespace EveMailHelper.DataModels
{
    public partial class MailLabel
    {
        public Guid Id { get; set; }
        public Guid CharacterId { get; set; }

        public long? EveLabelId { get; set; } = null;
        public string Name { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;
        public int? UnreadCount { get; set; } = null;

        public Character Character { get; set; } = null!;

        public override string ToString()
        {
            return Name;
        }
    }
}
