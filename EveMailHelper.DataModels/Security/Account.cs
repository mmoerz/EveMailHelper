namespace EveMailHelper.DataModels.Security
{
    public class Account
    {
        public Account()
        {
            Characters = new HashSet<Character>();
            EveAccounts= new HashSet<EveAccount>();
            Roles = new HashSet<Role>();
        }

        public Guid Id { get; set; }
        public string NickName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Description { get; set; } = null!;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public ICollection<Character> Characters { get; set; }
        public ICollection<EveAccount> EveAccounts { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
