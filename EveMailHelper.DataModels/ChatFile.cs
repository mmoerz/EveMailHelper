namespace EveMailHelper.DataModels
{
    public partial class ChatFile
    {
        //public ChatFile()
        //{

        //}

        public Guid Id { get; set; }
       
        public byte[] Content { get; set; } = null!;

        public Chat Chat { get; set; } = null!;
    }
}
