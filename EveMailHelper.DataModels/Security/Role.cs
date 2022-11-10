namespace EveMailHelper.DataModels.Security
{
    public class Role
    {
        public Role()
        {
            Permissions = new HashSet<Permission>();
            Accounts = new HashSet<Account>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public ICollection<Permission> Permissions { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
