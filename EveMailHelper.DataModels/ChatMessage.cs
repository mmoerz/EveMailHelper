namespace EveMailHelper.DataModels
{
    public class ChatMessage
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public Guid AuthorId { get; set; }
        public string Message { get; set; } = null!;
        public DateTime Created { get; set; }
        public Chat Chat { get; set; } = null!;
        public Character Author { get; set; } = null!;
    }
}
