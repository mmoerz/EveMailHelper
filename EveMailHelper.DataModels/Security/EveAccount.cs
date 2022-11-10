namespace EveMailHelper.DataModels.Security
{
    public class EveAccount
    {
        public EveAccount()
        {
            Characters = new HashSet<Character>();
        }

        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public Account Account { get; set; } = null!;
        public ICollection<Character> Characters { get; set; }
    }
}
