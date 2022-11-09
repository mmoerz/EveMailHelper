namespace EveMailHelper.DataModels
{
    public partial class Note
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public Guid AttachedToId { get; set; } 
        public Character AttachedTo { get; set; } = null!;
    }
}
