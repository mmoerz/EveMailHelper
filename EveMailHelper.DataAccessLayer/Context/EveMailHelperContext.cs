using Microsoft.EntityFrameworkCore;

using EveMailHelper.DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;

namespace EveMailHelper.DataAccessLayer.Context
{
    public partial class EveMailHelperContext : DbContext
    {
        private readonly IConfiguration Configuration = null!;

        public EveMailHelperContext() 
        {
            Configuration = SetupConfiguration();
        }

        //public EveMailHelperContext(IConfiguration config, DbContextOptions<EveMailHelperContext> options)
        //    : base(options)
        //{
        //    Configuration = config;
        //}

        public EveMailHelperContext(DbContextOptions<EveMailHelperContext> options)
           : base(options)
        {
            Configuration = SetupConfiguration();
        }

        /// <summary>
        /// safe and sane way to retrieve the runtime database user and password for the
        /// developmentenvironment.
        /// Production should not use this!
        /// Production should stick to a save storage!
        /// </summary>
        /// <returns></returns>
        public static IConfigurationRoot SetupConfiguration()
        {
            return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddUserSecrets<EveMailHelperContext>()
            .Build();
        }

        public virtual DbSet<Character> Characters { get; set; } = null!;
        public virtual DbSet<Chat> Chats { get; set; } = null!;
        public virtual DbSet<ChatMessage> ChatMessages { get; set; } = null!;
        public virtual DbSet<EveMail> EveMails { get; set; } = null!;
        public virtual DbSet<EveMailSentTo> EveMailSentTos { get; set; } = null!;
        public virtual DbSet<EveMailTemplate> EveMailTemplates { get; set; } = null!;
        public virtual DbSet<Note> Notes { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Default"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
