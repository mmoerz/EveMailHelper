namespace EveMailHelper.DataModels
{
    public partial class Chat
    {
        public Chat()
        {
            Messages = new HashSet<ChatMessage>();
        }

        public Guid Id { get; set; }
        public string ChannelName { get; set; } = null!;
        /// <summary>
        /// the recruitee that the chat was done with
        /// </summary>
        public Guid AttachedToId { get; set; }
        /// <summary>
        /// the character (recruitmentofficer) that conducted
        /// the chat
        /// </summary>
        public Guid ListenerId { get; set; }

        public Guid ChatFileId { get; set; }

        public DateTime SessionStarted { get; set; }

        public Character Listener { get; set; } = null!;
        public Character AttachedTo { get; set; } = null!;

        public ChatFile ChatFile { get; set; } = null!;

        public virtual ICollection<ChatMessage> Messages { get; set; } = null!;
    }
}
