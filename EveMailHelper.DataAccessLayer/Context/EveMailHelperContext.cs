using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Security;
using EveMailHelper.DataModels.Sde;
using EveMailHelper.DataModels.Sde.Character;
using EveMailHelper.DataModels.Sde.Map;
using EveMailHelper.DataModels.Market;

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

        public virtual DbSet<Alliance> Alliances { get; set; } = null!;
        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<EveAccount> EveAccounts { get; set; } = null!;
        /// <summary>
        /// Eve Characters
        /// </summary>
        public virtual DbSet<Character> Characters { get; set; } = null!;
        /// <summary>
        /// SSO Authentication Information
        /// </summary>
        public virtual DbSet<CharacterAuthInfo> CharacterAuthInfos { get; set; } = null!;
        public virtual DbSet<Chat> Chats { get; set; } = null!;
        public virtual DbSet<ChatFile> ChatFiles { get; set; } = null!;
        public virtual DbSet<ChatMessage> ChatMessages { get; set; } = null!;
        public virtual DbSet<Corporation> Corporations { get; set; } = null!;
        
        public virtual DbSet<EveMailSentTo> EveMailSentTos { get; set; } = null!;
        public virtual DbSet<EveMailTemplate> EveMailTemplates { get; set; } = null!;

        public virtual DbSet<MailLabel> MailLabels { get; set; } = null!;
        public virtual DbSet<MailList> MailLists { get; set; } = null!;
        public virtual DbSet<MailRecipient> MailRecipients { get; set; } = null!;
        public virtual DbSet<MailRecipientCharacter> MailRecipientCharacters { get; set; } = null!;
        public virtual DbSet<Mail> Mails { get; set; } = null!;
        public virtual DbSet<Note> Notes { get; set; } = null!;

        #region EveSDE
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Race> Races { get; set; } = null!;
        public virtual DbSet<EveType> EveTypes { get; set; } = null !;
        public virtual DbSet<Graphic> Graphics { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<Icon> Icons { get; set; } = null!;
        public virtual DbSet<IndustryActivity> IndustryActivities { get; set; } = null!;
        public virtual DbSet<IndustryActivityMaterial> IndustryActivityMaterials { get; set; } = null!;
        public virtual DbSet<IndustryActivityProbability> IndustryActivityProbabilities { get; set; } = null!;
        public virtual DbSet<IndustryActivityProduct> IndustryActivityProducts { get; set; } = null!;
        public virtual DbSet<IndustryBlueprint> IndustryBlueprints { get; set; } = null!;
        public virtual DbSet<MarketGroup> MarketGroups { get; set; } = null!;
        public virtual DbSet<Region> Regions { get; set; } = null!;
        public virtual DbSet<Constellation> Constellations { get; set; } = null!;
        public virtual DbSet<SolarSystem> SolarSystems { get; set; } = null!;
        public virtual DbSet<Faction> Factions { get; set; } = null!;
        public virtual DbSet<NpcCorporation> NpcCorporations { get; set; } = null!;

        #endregion

        #region
        // now this is getting tricky here
        // we archive market data here
        public virtual DbSet<MarketOrder> MarketOrders { get; set; } = null!;
        public virtual DbSet<MarketPrice> MarketPrices { get; set; } = null!;
        public virtual DbSet<NormalizedProductionCost> NormalizeProductionCosts { get; set; } = null!;
        public virtual DbSet<BuyList> BuyLists { get; set; } = null!;

        #endregion

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
