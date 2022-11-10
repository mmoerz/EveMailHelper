namespace EveMailHelper.DataModels.Security
{
    public class Permission
    {
        Permission() 
        {
            Roles = new HashSet<Role>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public HashSet<Role> Roles { get; set; } 
    }
}
