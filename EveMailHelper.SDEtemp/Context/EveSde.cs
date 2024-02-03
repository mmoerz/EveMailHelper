using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EveMailHelper.SDEtemp.Models;

namespace EveMailHelper.SDEtemp.Context
{
    public partial class EveSde : DbContext
    {
        public EveSde()
        {
        }

        public EveSde(DbContextOptions<EveSde> options)
            : base(options)
        {
        }

        public virtual DbSet<AgtAgent> AgtAgents { get; set; } = null!;
        public virtual DbSet<AgtAgentType> AgtAgentTypes { get; set; } = null!;
        public virtual DbSet<AgtAgentsInSpace> AgtAgentsInSpaces { get; set; } = null!;
        public virtual DbSet<AgtResearchAgent> AgtResearchAgents { get; set; } = null!;
        public virtual DbSet<CertCert> CertCerts { get; set; } = null!;
        public virtual DbSet<CertMastery> CertMasteries { get; set; } = null!;
        public virtual DbSet<CertSkill> CertSkills { get; set; } = null!;
        public virtual DbSet<ChrAncestry> ChrAncestries { get; set; } = null!;
        public virtual DbSet<ChrAttribute> ChrAttributes { get; set; } = null!;
        public virtual DbSet<ChrBloodline> ChrBloodlines { get; set; } = null!;
        public virtual DbSet<ChrFaction> ChrFactions { get; set; } = null!;
        public virtual DbSet<ChrRace> ChrRaces { get; set; } = null!;
        public virtual DbSet<CrpNpccorporation> CrpNpccorporations { get; set; } = null!;
        public virtual DbSet<CrpNpccorporationResearchField> CrpNpccorporationResearchFields { get; set; } = null!;
        public virtual DbSet<DgmAttributeCategory> DgmAttributeCategories { get; set; } = null!;
        public virtual DbSet<DgmAttributeType> DgmAttributeTypes { get; set; } = null!;
        public virtual DbSet<DgmEffect> DgmEffects { get; set; } = null!;
        public virtual DbSet<DgmTypeAttribute> DgmTypeAttributes { get; set; } = null!;
        public virtual DbSet<DgmTypeEffect> DgmTypeEffects { get; set; } = null!;
        public virtual DbSet<EveGraphic> EveGraphics { get; set; } = null!;
        public virtual DbSet<EveIcon> EveIcons { get; set; } = null!;
        public virtual DbSet<EveUnit> EveUnits { get; set; } = null!;
        public virtual DbSet<IndustryActivity> IndustryActivities { get; set; } = null!;
        public virtual DbSet<IndustryActivityMaterial> IndustryActivityMaterials { get; set; } = null!;
        public virtual DbSet<IndustryActivityProbability> IndustryActivityProbabilities { get; set; } = null!;
        public virtual DbSet<IndustryActivityProduct> IndustryActivityProducts { get; set; } = null!;
        public virtual DbSet<IndustryActivitySkill> IndustryActivitySkills { get; set; } = null!;
        public virtual DbSet<IndustryBlueprint> IndustryBlueprints { get; set; } = null!;
        public virtual DbSet<InvCategory> InvCategories { get; set; } = null!;
        public virtual DbSet<InvContrabandType> InvContrabandTypes { get; set; } = null!;
        public virtual DbSet<InvControlTowerResource> InvControlTowerResources { get; set; } = null!;
        public virtual DbSet<InvControlTowerResourcePurpose> InvControlTowerResourcePurposes { get; set; } = null!;
        public virtual DbSet<InvFlag> InvFlags { get; set; } = null!;
        public virtual DbSet<InvGroup> InvGroups { get; set; } = null!;
        public virtual DbSet<InvItem> InvItems { get; set; } = null!;
        public virtual DbSet<InvMarketGroup> InvMarketGroups { get; set; } = null!;
        public virtual DbSet<InvMetaGroup> InvMetaGroups { get; set; } = null!;
        public virtual DbSet<InvMetaType> InvMetaTypes { get; set; } = null!;
        public virtual DbSet<InvName> InvNames { get; set; } = null!;
        public virtual DbSet<InvPosition> InvPositions { get; set; } = null!;
        public virtual DbSet<InvTrait> InvTraits { get; set; } = null!;
        public virtual DbSet<InvType> InvTypes { get; set; } = null!;
        public virtual DbSet<InvTypeMaterial> InvTypeMaterials { get; set; } = null!;
        public virtual DbSet<InvUniqueName> InvUniqueNames { get; set; } = null!;
        public virtual DbSet<InvVolume> InvVolumes { get; set; } = null!;
        public virtual DbSet<MapCelestialGraphic> MapCelestialGraphics { get; set; } = null!;
        public virtual DbSet<MapCelestialStatistic> MapCelestialStatistics { get; set; } = null!;
        public virtual DbSet<MapConstellation> MapConstellations { get; set; } = null!;
        public virtual DbSet<MapConstellationJump> MapConstellationJumps { get; set; } = null!;
        public virtual DbSet<MapDenormalize> MapDenormalizes { get; set; } = null!;
        public virtual DbSet<MapJump> MapJumps { get; set; } = null!;
        public virtual DbSet<MapLandmark> MapLandmarks { get; set; } = null!;
        public virtual DbSet<MapLocationScene> MapLocationScenes { get; set; } = null!;
        public virtual DbSet<MapLocationWormholeClass> MapLocationWormholeClasses { get; set; } = null!;
        public virtual DbSet<MapRegion> MapRegions { get; set; } = null!;
        public virtual DbSet<MapRegionJump> MapRegionJumps { get; set; } = null!;
        public virtual DbSet<MapSolarSystem> MapSolarSystems { get; set; } = null!;
        public virtual DbSet<MapSolarSystemJump> MapSolarSystemJumps { get; set; } = null!;
        public virtual DbSet<MapUniverse> MapUniverses { get; set; } = null!;
        public virtual DbSet<PlanetSchematic> PlanetSchematics { get; set; } = null!;
        public virtual DbSet<PlanetSchematicsPinMap> PlanetSchematicsPinMaps { get; set; } = null!;
        public virtual DbSet<PlanetSchematicsTypeMap> PlanetSchematicsTypeMaps { get; set; } = null!;
        public virtual DbSet<RamActivity> RamActivities { get; set; } = null!;
        public virtual DbSet<RamAssemblyLineStation> RamAssemblyLineStations { get; set; } = null!;
        public virtual DbSet<RamAssemblyLineType> RamAssemblyLineTypes { get; set; } = null!;
        public virtual DbSet<RamAssemblyLineTypeDetailPerCategory> RamAssemblyLineTypeDetailPerCategories { get; set; } = null!;
        public virtual DbSet<RamAssemblyLineTypeDetailPerGroup> RamAssemblyLineTypeDetailPerGroups { get; set; } = null!;
        public virtual DbSet<RamInstallationTypeContent> RamInstallationTypeContents { get; set; } = null!;
        public virtual DbSet<Skin> Skins { get; set; } = null!;
        public virtual DbSet<SkinLicense> SkinLicenses { get; set; } = null!;
        public virtual DbSet<SkinMaterial> SkinMaterials { get; set; } = null!;
        public virtual DbSet<SkinShip> SkinShips { get; set; } = null!;
        public virtual DbSet<StaOperation> StaOperations { get; set; } = null!;
        public virtual DbSet<StaOperationService> StaOperationServices { get; set; } = null!;
        public virtual DbSet<StaService> StaServices { get; set; } = null!;
        public virtual DbSet<StaStation> StaStations { get; set; } = null!;
        public virtual DbSet<StaStationType> StaStationTypes { get; set; } = null!;
        public virtual DbSet<TranslationTable> TranslationTables { get; set; } = null!;
        public virtual DbSet<TrnTranslation> TrnTranslations { get; set; } = null!;
        public virtual DbSet<TrnTranslationColumn> TrnTranslationColumns { get; set; } = null!;
        public virtual DbSet<TrnTranslationLanguage> TrnTranslationLanguages { get; set; } = null!;
        public virtual DbSet<WarCombatZone> WarCombatZones { get; set; } = null!;
        public virtual DbSet<WarCombatZoneSystem> WarCombatZoneSystems { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=EveSde;User ID=sa;Password=tNCqxOh6IMKesh23LXvQ._;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgtAgent>(entity =>
            {
                entity.HasKey(e => e.AgentId)
                    .HasName("PK__agtAgent__350C70227B094FD7");

                entity.ToTable("agtAgents");

                entity.HasIndex(e => e.CorporationId, "ix_agtAgents_corporationID");

                entity.HasIndex(e => e.LocationId, "ix_agtAgents_locationID");

                entity.Property(e => e.AgentId)
                    .ValueGeneratedNever()
                    .HasColumnName("agentID");

                entity.Property(e => e.AgentTypeId).HasColumnName("agentTypeID");

                entity.Property(e => e.CorporationId).HasColumnName("corporationID");

                entity.Property(e => e.DivisionId).HasColumnName("divisionID");

                entity.Property(e => e.IsLocator).HasColumnName("isLocator");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.LocationId).HasColumnName("locationID");

                entity.Property(e => e.Quality).HasColumnName("quality");
            });

            modelBuilder.Entity<AgtAgentType>(entity =>
            {
                entity.HasKey(e => e.AgentTypeId)
                    .HasName("PK__agtAgent__97E63E0EB7C8F4B8");

                entity.ToTable("agtAgentTypes");

                entity.Property(e => e.AgentTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("agentTypeID");

                entity.Property(e => e.AgentType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("agentType");
            });

            modelBuilder.Entity<AgtAgentsInSpace>(entity =>
            {
                entity.HasKey(e => e.AgentId)
                    .HasName("PK__agtAgent__350C70225165DC1E");

                entity.ToTable("agtAgentsInSpace");

                entity.HasIndex(e => e.SolarSystemId, "ix_agtAgentsInSpace_solarSystemID");

                entity.Property(e => e.AgentId)
                    .ValueGeneratedNever()
                    .HasColumnName("agentID");

                entity.Property(e => e.DungeonId).HasColumnName("dungeonID");

                entity.Property(e => e.SolarSystemId).HasColumnName("solarSystemID");

                entity.Property(e => e.SpawnPointId).HasColumnName("spawnPointID");

                entity.Property(e => e.TypeId).HasColumnName("typeID");
            });

            modelBuilder.Entity<AgtResearchAgent>(entity =>
            {
                entity.HasKey(e => new { e.AgentId, e.TypeId })
                    .HasName("PK__agtResea__8A08AF33D6AE863D");

                entity.ToTable("agtResearchAgents");

                entity.HasIndex(e => e.TypeId, "ix_agtResearchAgents_typeID");

                entity.Property(e => e.AgentId).HasColumnName("agentID");

                entity.Property(e => e.TypeId).HasColumnName("typeID");
            });

            modelBuilder.Entity<CertCert>(entity =>
            {
                entity.HasKey(e => e.CertId)
                    .HasName("PK__certCert__D2C9361911472B4E");

                entity.ToTable("certCerts");

                entity.Property(e => e.CertId)
                    .ValueGeneratedNever()
                    .HasColumnName("certID");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.GroupId).HasColumnName("groupID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<CertMastery>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("certMasteries");

                entity.Property(e => e.CertId).HasColumnName("certID");

                entity.Property(e => e.MasteryLevel).HasColumnName("masteryLevel");

                entity.Property(e => e.TypeId).HasColumnName("typeID");
            });

            modelBuilder.Entity<CertSkill>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("certSkills");

                entity.HasIndex(e => e.SkillId, "ix_certSkills_skillID");

                entity.Property(e => e.CertId).HasColumnName("certID");

                entity.Property(e => e.CertLevelInt).HasColumnName("certLevelInt");

                entity.Property(e => e.CertLevelText)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("certLevelText");

                entity.Property(e => e.SkillId).HasColumnName("skillID");

                entity.Property(e => e.SkillLevel).HasColumnName("skillLevel");
            });

            modelBuilder.Entity<ChrAncestry>(entity =>
            {
                entity.HasKey(e => e.AncestryId)
                    .HasName("PK__chrAnces__B33BD93931522BBE");

                entity.ToTable("chrAncestries");

                entity.Property(e => e.AncestryId)
                    .ValueGeneratedNever()
                    .HasColumnName("ancestryID");

                entity.Property(e => e.AncestryName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ancestryName");

                entity.Property(e => e.BloodlineId).HasColumnName("bloodlineID");

                entity.Property(e => e.Charisma).HasColumnName("charisma");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IconId).HasColumnName("iconID");

                entity.Property(e => e.Intelligence).HasColumnName("intelligence");

                entity.Property(e => e.Memory).HasColumnName("memory");

                entity.Property(e => e.Perception).HasColumnName("perception");

                entity.Property(e => e.ShortDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("shortDescription");

                entity.Property(e => e.Willpower).HasColumnName("willpower");
            });

            modelBuilder.Entity<ChrAttribute>(entity =>
            {
                entity.HasKey(e => e.AttributeId)
                    .HasName("PK__chrAttri__03B803D0E3407959");

                entity.ToTable("chrAttributes");

                entity.Property(e => e.AttributeId)
                    .ValueGeneratedNever()
                    .HasColumnName("attributeID");

                entity.Property(e => e.AttributeName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("attributeName");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IconId).HasColumnName("iconID");

                entity.Property(e => e.Notes)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("notes");

                entity.Property(e => e.ShortDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("shortDescription");
            });

            modelBuilder.Entity<ChrBloodline>(entity =>
            {
                entity.HasKey(e => e.BloodlineId)
                    .HasName("PK__chrBlood__3D71B0E4BD7D2B52");

                entity.ToTable("chrBloodlines");

                entity.Property(e => e.BloodlineId)
                    .ValueGeneratedNever()
                    .HasColumnName("bloodlineID");

                entity.Property(e => e.BloodlineName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("bloodlineName");

                entity.Property(e => e.Charisma).HasColumnName("charisma");

                entity.Property(e => e.CorporationId).HasColumnName("corporationID");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.FemaleDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("femaleDescription");

                entity.Property(e => e.IconId).HasColumnName("iconID");

                entity.Property(e => e.Intelligence).HasColumnName("intelligence");

                entity.Property(e => e.MaleDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("maleDescription");

                entity.Property(e => e.Memory).HasColumnName("memory");

                entity.Property(e => e.Perception).HasColumnName("perception");

                entity.Property(e => e.RaceId).HasColumnName("raceID");

                entity.Property(e => e.ShipTypeId).HasColumnName("shipTypeID");

                entity.Property(e => e.ShortDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("shortDescription");

                entity.Property(e => e.ShortFemaleDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("shortFemaleDescription");

                entity.Property(e => e.ShortMaleDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("shortMaleDescription");

                entity.Property(e => e.Willpower).HasColumnName("willpower");
            });

            modelBuilder.Entity<ChrFaction>(entity =>
            {
                entity.HasKey(e => e.FactionId)
                    .HasName("PK__chrFacti__CA771A8E434F4ABD");

                entity.ToTable("chrFactions");

                entity.Property(e => e.FactionId)
                    .ValueGeneratedNever()
                    .HasColumnName("factionID");

                entity.Property(e => e.CorporationId).HasColumnName("corporationID");

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.FactionName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("factionName");

                entity.Property(e => e.IconId).HasColumnName("iconID");

                entity.Property(e => e.MilitiaCorporationId).HasColumnName("militiaCorporationID");

                entity.Property(e => e.RaceIds).HasColumnName("raceIDs");

                entity.Property(e => e.SizeFactor).HasColumnName("sizeFactor");

                entity.Property(e => e.SolarSystemId).HasColumnName("solarSystemID");

                entity.Property(e => e.StationCount).HasColumnName("stationCount");

                entity.Property(e => e.StationSystemCount).HasColumnName("stationSystemCount");
            });

            modelBuilder.Entity<CrpNpccorporation>(entity =>
            {
                entity.HasKey(e => e.CorporationId)
                    .HasName("PK__crpNPCCo__D36DBB173F1D57D1");

                entity.ToTable("crpNPCCorporations");

                entity.Property(e => e.CorporationId)
                    .ValueGeneratedNever()
                    .HasColumnName("corporationID");

                entity.Property(e => e.Border).HasColumnName("border");

                entity.Property(e => e.Corridor).HasColumnName("corridor");

                entity.Property(e => e.Description)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.EnemyId).HasColumnName("enemyID");

                entity.Property(e => e.Extent)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("extent")
                    .IsFixedLength();

                entity.Property(e => e.FactionId).HasColumnName("factionID");

                entity.Property(e => e.FriendId).HasColumnName("friendID");

                entity.Property(e => e.Fringe).HasColumnName("fringe");

                entity.Property(e => e.Hub).HasColumnName("hub");

                entity.Property(e => e.IconId).HasColumnName("iconID");

                entity.Property(e => e.InitialPrice).HasColumnName("initialPrice");

                entity.Property(e => e.InvestorId1).HasColumnName("investorID1");

                entity.Property(e => e.InvestorId2).HasColumnName("investorID2");

                entity.Property(e => e.InvestorId3).HasColumnName("investorID3");

                entity.Property(e => e.InvestorId4).HasColumnName("investorID4");

                entity.Property(e => e.InvestorShares1).HasColumnName("investorShares1");

                entity.Property(e => e.InvestorShares2).HasColumnName("investorShares2");

                entity.Property(e => e.InvestorShares3).HasColumnName("investorShares3");

                entity.Property(e => e.InvestorShares4).HasColumnName("investorShares4");

                entity.Property(e => e.MinSecurity).HasColumnName("minSecurity");

                entity.Property(e => e.PublicShares).HasColumnName("publicShares");

                entity.Property(e => e.Scattered).HasColumnName("scattered");

                entity.Property(e => e.Size)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("size")
                    .IsFixedLength();

                entity.Property(e => e.SizeFactor).HasColumnName("sizeFactor");

                entity.Property(e => e.SolarSystemId).HasColumnName("solarSystemID");

                entity.Property(e => e.StationCount).HasColumnName("stationCount");

                entity.Property(e => e.StationSystemCount).HasColumnName("stationSystemCount");
            });

            modelBuilder.Entity<CrpNpccorporationResearchField>(entity =>
            {
                entity.HasKey(e => new { e.SkillId, e.CorporationId })
                    .HasName("PK__crpNPCCo__C35CB06EA3FFE1C0");

                entity.ToTable("crpNPCCorporationResearchFields");

                entity.Property(e => e.SkillId).HasColumnName("skillID");

                entity.Property(e => e.CorporationId).HasColumnName("corporationID");
            });

            modelBuilder.Entity<DgmAttributeCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__dgmAttri__23CAF1F8B0648ABB");

                entity.ToTable("dgmAttributeCategories");

                entity.Property(e => e.CategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("categoryID");

                entity.Property(e => e.CategoryDescription)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("categoryDescription");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("categoryName");
            });

            modelBuilder.Entity<DgmAttributeType>(entity =>
            {
                entity.HasKey(e => e.AttributeId)
                    .HasName("PK__dgmAttri__03B803D0E10E26E0");

                entity.ToTable("dgmAttributeTypes");

                entity.Property(e => e.AttributeId)
                    .ValueGeneratedNever()
                    .HasColumnName("attributeID");

                entity.Property(e => e.AttributeName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("attributeName");

                entity.Property(e => e.CategoryId).HasColumnName("categoryID");

                entity.Property(e => e.DefaultValue).HasColumnName("defaultValue");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("displayName");

                entity.Property(e => e.HighIsGood).HasColumnName("highIsGood");

                entity.Property(e => e.IconId).HasColumnName("iconID");

                entity.Property(e => e.Published).HasColumnName("published");

                entity.Property(e => e.Stackable).HasColumnName("stackable");

                entity.Property(e => e.UnitId).HasColumnName("unitID");
            });

            modelBuilder.Entity<DgmEffect>(entity =>
            {
                entity.HasKey(e => e.EffectId)
                    .HasName("PK__dgmEffec__DD40F93301515925");

                entity.ToTable("dgmEffects");

                entity.Property(e => e.EffectId)
                    .ValueGeneratedNever()
                    .HasColumnName("effectID");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.DisallowAutoRepeat).HasColumnName("disallowAutoRepeat");

                entity.Property(e => e.DischargeAttributeId).HasColumnName("dischargeAttributeID");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("displayName");

                entity.Property(e => e.Distribution).HasColumnName("distribution");

                entity.Property(e => e.DurationAttributeId).HasColumnName("durationAttributeID");

                entity.Property(e => e.EffectCategory).HasColumnName("effectCategory");

                entity.Property(e => e.EffectName)
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("effectName");

                entity.Property(e => e.ElectronicChance).HasColumnName("electronicChance");

                entity.Property(e => e.FalloffAttributeId).HasColumnName("falloffAttributeID");

                entity.Property(e => e.FittingUsageChanceAttributeId).HasColumnName("fittingUsageChanceAttributeID");

                entity.Property(e => e.Guid)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("guid");

                entity.Property(e => e.IconId).HasColumnName("iconID");

                entity.Property(e => e.IsAssistance).HasColumnName("isAssistance");

                entity.Property(e => e.IsOffensive).HasColumnName("isOffensive");

                entity.Property(e => e.IsWarpSafe).HasColumnName("isWarpSafe");

                entity.Property(e => e.ModifierInfo).HasColumnName("modifierInfo");

                entity.Property(e => e.NpcActivationChanceAttributeId).HasColumnName("npcActivationChanceAttributeID");

                entity.Property(e => e.NpcUsageChanceAttributeId).HasColumnName("npcUsageChanceAttributeID");

                entity.Property(e => e.PostExpression).HasColumnName("postExpression");

                entity.Property(e => e.PreExpression).HasColumnName("preExpression");

                entity.Property(e => e.PropulsionChance).HasColumnName("propulsionChance");

                entity.Property(e => e.Published).HasColumnName("published");

                entity.Property(e => e.RangeAttributeId).HasColumnName("rangeAttributeID");

                entity.Property(e => e.RangeChance).HasColumnName("rangeChance");

                entity.Property(e => e.SfxName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("sfxName");

                entity.Property(e => e.TrackingSpeedAttributeId).HasColumnName("trackingSpeedAttributeID");
            });

            modelBuilder.Entity<DgmTypeAttribute>(entity =>
            {
                entity.HasKey(e => new { e.TypeId, e.AttributeId })
                    .HasName("PK__dgmTypeA__E0767127D62FA60C");

                entity.ToTable("dgmTypeAttributes");

                entity.HasIndex(e => e.AttributeId, "ix_dgmTypeAttributes_attributeID");

                entity.Property(e => e.TypeId).HasColumnName("typeID");

                entity.Property(e => e.AttributeId).HasColumnName("attributeID");

                entity.Property(e => e.ValueFloat).HasColumnName("valueFloat");

                entity.Property(e => e.ValueInt).HasColumnName("valueInt");
            });

            modelBuilder.Entity<DgmTypeEffect>(entity =>
            {
                entity.HasKey(e => new { e.TypeId, e.EffectId })
                    .HasName("PK__dgmTypeE__DD99FE89F7CFAC23");

                entity.ToTable("dgmTypeEffects");

                entity.Property(e => e.TypeId).HasColumnName("typeID");

                entity.Property(e => e.EffectId).HasColumnName("effectID");

                entity.Property(e => e.IsDefault).HasColumnName("isDefault");
            });        

            // yeah there is something inside
            modelBuilder.Entity<EveUnit>(entity =>
            {
                entity.HasKey(e => e.UnitId)
                    .HasName("PK__eveUnits__55D79215595D9F49");

                entity.ToTable("eveUnits");

                entity.Property(e => e.UnitId)
                    .ValueGeneratedNever()
                    .HasColumnName("unitID");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("displayName");

                entity.Property(e => e.UnitName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("unitName");
            });
           
            /*
            modelBuilder.Entity<IndustryActivityMaterial>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("industryActivityMaterials");

                entity.HasIndex(e => new { e.TypeId, e.ActivityId }, "industryActivityMaterials_idx1");

                entity.HasIndex(e => e.TypeId, "ix_industryActivityMaterials_typeID");

                entity.Property(e => e.ActivityId).HasColumnName("activityID");

                entity.Property(e => e.MaterialTypeId).HasColumnName("materialTypeID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.TypeId).HasColumnName("typeID");

                entity.HasOne(d => d.MaterialType)
                    .WithMany()
                    .HasForeignKey(d => d.MaterialTypeId)
                    .HasConstraintName("FK_INDUSTRYACTIVITYMATERIALS_MATERIALTYPEID");

                entity.HasOne(d => d.Type)
                    .WithMany()
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_INDUSTRYACTIVITYMATERIALS_TYPEID");

                entity.HasOne(d => d.IndustryActivity)
                    .WithMany()
                    .HasForeignKey(d => new { d.TypeId, d.ActivityId })
                    .HasConstraintName("FK_INDUSTRYACTIVITYMATERIALS_ACTIVITYID");
            });
            */

            modelBuilder.Entity<IndustryActivityProbability>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("industryActivityProbabilities");

                entity.HasIndex(e => e.ProductTypeId, "ix_industryActivityProbabilities_productTypeID");

                entity.HasIndex(e => e.TypeId, "ix_industryActivityProbabilities_typeID");

                entity.Property(e => e.ActivityId).HasColumnName("activityID");

                entity.Property(e => e.Probability)
                    .HasColumnType("decimal(3, 2)")
                    .HasColumnName("probability");

                entity.Property(e => e.ProductTypeId).HasColumnName("productTypeID");

                entity.Property(e => e.TypeId).HasColumnName("typeID");

                entity.HasOne(d => d.ProductType)
                    .WithMany()
                    .HasForeignKey(d => d.ProductTypeId)
                    .HasConstraintName("FK_INDUSTRYACTIVITYPROBABILITIES_PRODUCTID");

                entity.HasOne(d => d.IndustryActivity)
                    .WithMany()
                    .HasForeignKey(d => new { d.TypeId, d.ActivityId })
                    .HasConstraintName("FK_INDUSTRYACTIVITYPROBABILITIES_TYPEACTIVITYID");
            });

            modelBuilder.Entity<IndustryActivityProduct>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("industryActivityProducts");

                entity.HasIndex(e => e.ProductTypeId, "ix_industryActivityProducts_productTypeID");

                entity.HasIndex(e => e.TypeId, "ix_industryActivityProducts_typeID");

                entity.Property(e => e.ActivityId).HasColumnName("activityID");

                entity.Property(e => e.ProductTypeId).HasColumnName("productTypeID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.TypeId).HasColumnName("typeID");

                entity.HasOne(d => d.ProductType)
                    .WithMany()
                    .HasForeignKey(d => d.ProductTypeId)
                    .HasConstraintName("FK_INDUSTRYACTIVITYPRODUCTS_PRODUCTID");

                entity.HasOne(d => d.IndustryActivity)
                    .WithMany()
                    .HasForeignKey(d => new { d.TypeId, d.ActivityId })
                    .HasConstraintName("FK_INDUSTRYACTIVITYPRODUCTS_TYPEACTIVITYID");
            });

            modelBuilder.Entity<IndustryActivitySkill>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("industryActivitySkills");

                entity.HasIndex(e => new { e.TypeId, e.ActivityId }, "industryActivitySkills_idx1");

                entity.HasIndex(e => e.SkillId, "ix_industryActivitySkills_skillID");

                entity.HasIndex(e => e.TypeId, "ix_industryActivitySkills_typeID");

                entity.Property(e => e.ActivityId).HasColumnName("activityID");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.SkillId).HasColumnName("skillID");

                entity.Property(e => e.TypeId).HasColumnName("typeID");
            });

            modelBuilder.Entity<IndustryBlueprint>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK__industry__F04DF11AEEADE38B");

                entity.ToTable("industryBlueprints");

                entity.Property(e => e.TypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("typeID");

                entity.Property(e => e.MaxProductionLimit).HasColumnName("maxProductionLimit");

                entity.HasOne(d => d.Type)
                    .WithOne(p => p.IndustryBlueprint)
                    .HasForeignKey<IndustryBlueprint>(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_INDUSTRYBLUEPRINTS_TYPEID");
            });

            modelBuilder.Entity<InvCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__invCateg__23CAF1F83939A7DD");

                entity.ToTable("invCategories");

                entity.Property(e => e.CategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("categoryID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("categoryName");

                entity.Property(e => e.IconId).HasColumnName("iconID");

                entity.Property(e => e.Published).HasColumnName("published");

                entity.HasOne(d => d.Icon)
                    .WithMany(p => p.InvCategories)
                    .HasForeignKey(d => d.IconId)
                    .HasConstraintName("FK_CATEGORIES_ICONID");
            });

            modelBuilder.Entity<InvContrabandType>(entity =>
            {
                entity.HasKey(e => new { e.FactionId, e.TypeId })
                    .HasName("PK__invContr__7573C59F393CB71A");

                entity.ToTable("invContrabandTypes");

                entity.HasIndex(e => e.TypeId, "ix_invContrabandTypes_typeID");

                entity.Property(e => e.FactionId).HasColumnName("factionID");

                entity.Property(e => e.TypeId).HasColumnName("typeID");

                entity.Property(e => e.AttackMinSec).HasColumnName("attackMinSec");

                entity.Property(e => e.ConfiscateMinSec).HasColumnName("confiscateMinSec");

                entity.Property(e => e.FineByValue).HasColumnName("fineByValue");

                entity.Property(e => e.StandingLoss).HasColumnName("standingLoss");
            });

            modelBuilder.Entity<InvControlTowerResource>(entity =>
            {
                entity.HasKey(e => new { e.ControlTowerTypeId, e.ResourceTypeId })
                    .HasName("PK__invContr__16FC4C9FB98E4151");

                entity.ToTable("invControlTowerResources");

                entity.Property(e => e.ControlTowerTypeId).HasColumnName("controlTowerTypeID");

                entity.Property(e => e.ResourceTypeId).HasColumnName("resourceTypeID");

                entity.Property(e => e.FactionId).HasColumnName("factionID");

                entity.Property(e => e.MinSecurityLevel).HasColumnName("minSecurityLevel");

                entity.Property(e => e.Purpose).HasColumnName("purpose");

                entity.Property(e => e.Quantity).HasColumnName("quantity");
            });

            modelBuilder.Entity<InvControlTowerResourcePurpose>(entity =>
            {
                entity.HasKey(e => e.Purpose)
                    .HasName("PK__invContr__BBAA0C72AC161552");

                entity.ToTable("invControlTowerResourcePurposes");

                entity.Property(e => e.Purpose)
                    .ValueGeneratedNever()
                    .HasColumnName("purpose");

                entity.Property(e => e.PurposeText)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("purposeText");
            });

            modelBuilder.Entity<InvFlag>(entity =>
            {
                entity.HasKey(e => e.FlagId)
                    .HasName("PK__invFlags__12F396E047DA7F6D");

                entity.ToTable("invFlags");

                entity.Property(e => e.FlagId)
                    .ValueGeneratedNever()
                    .HasColumnName("flagID");

                entity.Property(e => e.FlagName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("flagName");

                entity.Property(e => e.FlagText)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("flagText");

                entity.Property(e => e.OrderId).HasColumnName("orderID");
            });

            modelBuilder.Entity<InvGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                    .HasName("PK__invGroup__88C102ADEDFDC538");

                entity.ToTable("invGroups");

                entity.HasIndex(e => e.CategoryId, "ix_invGroups_categoryID");

                entity.Property(e => e.GroupId)
                    .ValueGeneratedNever()
                    .HasColumnName("groupID");

                entity.Property(e => e.Anchorable).HasColumnName("anchorable");

                entity.Property(e => e.Anchored).HasColumnName("anchored");

                entity.Property(e => e.CategoryId).HasColumnName("categoryID");

                entity.Property(e => e.FittableNonSingleton).HasColumnName("fittableNonSingleton");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("groupName");

                entity.Property(e => e.IconId).HasColumnName("iconID");

                entity.Property(e => e.Published).HasColumnName("published");

                entity.Property(e => e.UseBasePrice).HasColumnName("useBasePrice");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.InvGroups)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_GROUPS_CATEGORYID");

                entity.HasOne(d => d.Icon)
                    .WithMany(p => p.InvGroups)
                    .HasForeignKey(d => d.IconId)
                    .HasConstraintName("FK_GROUPS_ICONID");
            });

            modelBuilder.Entity<InvItem>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PK__invItems__56A1284AAB1E8F52");

                entity.ToTable("invItems");

                entity.HasIndex(e => new { e.OwnerId, e.LocationId }, "items_IX_OwnerLocation");

                entity.HasIndex(e => e.LocationId, "ix_invItems_locationID");

                entity.Property(e => e.ItemId)
                    .ValueGeneratedNever()
                    .HasColumnName("itemID");

                entity.Property(e => e.FlagId).HasColumnName("flagID");

                entity.Property(e => e.LocationId).HasColumnName("locationID");

                entity.Property(e => e.OwnerId).HasColumnName("ownerID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.TypeId).HasColumnName("typeID");
            });

            modelBuilder.Entity<InvMarketGroup>(entity =>
            {
                entity.HasKey(e => e.MarketGroupId)
                    .HasName("PK__invMarke__1CC51B369ADA084E");

                entity.ToTable("invMarketGroups");

                entity.Property(e => e.MarketGroupId)
                    .ValueGeneratedNever()
                    .HasColumnName("marketGroupID");

                entity.Property(e => e.Description)
                    .HasMaxLength(3000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.HasTypes).HasColumnName("hasTypes");

                entity.Property(e => e.IconId).HasColumnName("iconID");

                entity.Property(e => e.MarketGroupName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("marketGroupName");

                entity.Property(e => e.ParentGroupId).HasColumnName("parentGroupID");

                entity.HasOne(d => d.Icon)
                    .WithMany(p => p.InvMarketGroups)
                    .HasForeignKey(d => d.IconId)
                    .HasConstraintName("FK_INVMARKETGROUPS_ICONID");

                entity.HasOne(d => d.ParentGroup)
                    .WithMany(p => p.InverseParentGroup)
                    .HasForeignKey(d => d.ParentGroupId)
                    .HasConstraintName("FK_INVMARKETGROUPS_PARENTGROUPID");
            });

            modelBuilder.Entity<InvMetaGroup>(entity =>
            {
                entity.HasKey(e => e.MetaGroupId)
                    .HasName("PK__invMetaG__C66506F4FC248E9C");

                entity.ToTable("invMetaGroups");

                entity.Property(e => e.MetaGroupId)
                    .ValueGeneratedNever()
                    .HasColumnName("metaGroupID");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IconId).HasColumnName("iconID");

                entity.Property(e => e.MetaGroupName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("metaGroupName");
            });

            modelBuilder.Entity<InvMetaType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK__invMetaT__F04DF11A39D6018E");

                entity.ToTable("invMetaTypes");

                entity.Property(e => e.TypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("typeID");

                entity.Property(e => e.MetaGroupId).HasColumnName("metaGroupID");

                entity.Property(e => e.ParentTypeId).HasColumnName("parentTypeID");
            });

            modelBuilder.Entity<InvName>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PK__invNames__56A1284AC8E1B74B");

                entity.ToTable("invNames");

                entity.Property(e => e.ItemId)
                    .ValueGeneratedNever()
                    .HasColumnName("itemID");

                entity.Property(e => e.ItemName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("itemName");
            });

            modelBuilder.Entity<InvPosition>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PK__invPosit__56A1284A48DFF34D");

                entity.ToTable("invPositions");

                entity.Property(e => e.ItemId)
                    .ValueGeneratedNever()
                    .HasColumnName("itemID");

                entity.Property(e => e.Pitch).HasColumnName("pitch");

                entity.Property(e => e.Roll).HasColumnName("roll");

                entity.Property(e => e.X).HasColumnName("x");

                entity.Property(e => e.Y).HasColumnName("y");

                entity.Property(e => e.Yaw).HasColumnName("yaw");

                entity.Property(e => e.Z).HasColumnName("z");
            });

            modelBuilder.Entity<InvTrait>(entity =>
            {
                entity.HasKey(e => e.TraitId)
                    .HasName("PK__invTrait__9F5AF018F4C0D563");

                entity.ToTable("invTraits");

                entity.Property(e => e.TraitId).HasColumnName("traitID");

                entity.Property(e => e.Bonus).HasColumnName("bonus");

                entity.Property(e => e.BonusText).HasColumnName("bonusText");

                entity.Property(e => e.SkillId).HasColumnName("skillID");

                entity.Property(e => e.TypeId).HasColumnName("typeID");

                entity.Property(e => e.UnitId).HasColumnName("unitID");
            });

            modelBuilder.Entity<InvType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK__invTypes__F04DF11A88ACDC4B");

                entity.ToTable("invTypes");

                entity.HasIndex(e => e.GroupId, "ix_invTypes_groupID");

                entity.Property(e => e.TypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("typeID");

                entity.Property(e => e.BasePrice)
                    .HasColumnType("decimal(19, 4)")
                    .HasColumnName("basePrice");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.GraphicId).HasColumnName("graphicID");

                entity.Property(e => e.GroupId).HasColumnName("groupID");

                entity.Property(e => e.IconId).HasColumnName("iconID");

                entity.Property(e => e.MarketGroupId).HasColumnName("marketGroupID");

                entity.Property(e => e.Mass).HasColumnName("mass");

                entity.Property(e => e.PortionSize).HasColumnName("portionSize");

                entity.Property(e => e.Published).HasColumnName("published");

                entity.Property(e => e.RaceId).HasColumnName("raceID");

                entity.Property(e => e.SoundId).HasColumnName("soundID");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("typeName");

                entity.Property(e => e.Volume).HasColumnName("volume");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.InvTypes)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_INVTYPES_GROUPID");

                entity.HasOne(d => d.Icon)
                    .WithMany(p => p.InvTypes)
                    .HasForeignKey(d => d.IconId)
                    .HasConstraintName("FK_INVTYPES_ICONID");

                entity.HasOne(d => d.MarketGroup)
                    .WithMany(p => p.InvTypes)
                    .HasForeignKey(d => d.MarketGroupId)
                    .HasConstraintName("FK_INVTYPES_MARKETGROUPID");

                entity.HasOne(d => d.Race)
                    .WithMany(p => p.InvTypes)
                    .HasForeignKey(d => d.RaceId)
                    .HasConstraintName("FK_INVTYPES_RACEID");
            });

            modelBuilder.Entity<InvTypeMaterial>(entity =>
            {
                entity.HasKey(e => new { e.TypeId, e.MaterialTypeId })
                    .HasName("PK__invTypeM__E739C02F3D016E9F");

                entity.ToTable("invTypeMaterials");

                entity.Property(e => e.TypeId).HasColumnName("typeID");

                entity.Property(e => e.MaterialTypeId).HasColumnName("materialTypeID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");
            });

            modelBuilder.Entity<InvUniqueName>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PK__invUniqu__56A1284A7C694471");

                entity.ToTable("invUniqueNames");

                entity.HasIndex(e => new { e.GroupId, e.ItemName }, "invUniqueNames_IX_GroupName");

                entity.HasIndex(e => e.ItemName, "ix_invUniqueNames_itemName")
                    .IsUnique();

                entity.Property(e => e.ItemId)
                    .ValueGeneratedNever()
                    .HasColumnName("itemID");

                entity.Property(e => e.GroupId).HasColumnName("groupID");

                entity.Property(e => e.ItemName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("itemName");
            });

            modelBuilder.Entity<InvVolume>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK__invVolum__F04DF11A3246A5DC");

                entity.ToTable("invVolumes");

                entity.Property(e => e.TypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("typeID");

                entity.Property(e => e.Volume).HasColumnName("volume");
            });

            modelBuilder.Entity<MapCelestialGraphic>(entity =>
            {
                entity.HasKey(e => e.CelestialId)
                    .HasName("PK__mapCeles__18B1D4CE99049247");

                entity.ToTable("mapCelestialGraphics");

                entity.Property(e => e.CelestialId)
                    .ValueGeneratedNever()
                    .HasColumnName("celestialID");

                entity.Property(e => e.HeightMap1).HasColumnName("heightMap1");

                entity.Property(e => e.HeightMap2).HasColumnName("heightMap2");

                entity.Property(e => e.Population).HasColumnName("population");

                entity.Property(e => e.ShaderPreset).HasColumnName("shaderPreset");
            });

            modelBuilder.Entity<MapCelestialStatistic>(entity =>
            {
                entity.HasKey(e => e.CelestialId)
                    .HasName("PK__mapCeles__18B1D4CECCDE5C77");

                entity.ToTable("mapCelestialStatistics");

                entity.Property(e => e.CelestialId)
                    .ValueGeneratedNever()
                    .HasColumnName("celestialID");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Density).HasColumnName("density");

                entity.Property(e => e.Eccentricity).HasColumnName("eccentricity");

                entity.Property(e => e.EscapeVelocity).HasColumnName("escapeVelocity");

                entity.Property(e => e.Fragmented).HasColumnName("fragmented");

                entity.Property(e => e.Life).HasColumnName("life");

                entity.Property(e => e.Locked).HasColumnName("locked");

                entity.Property(e => e.Luminosity).HasColumnName("luminosity");

                entity.Property(e => e.Mass).HasColumnName("mass");

                entity.Property(e => e.MassDust).HasColumnName("massDust");

                entity.Property(e => e.MassGas).HasColumnName("massGas");

                entity.Property(e => e.OrbitPeriod).HasColumnName("orbitPeriod");

                entity.Property(e => e.OrbitRadius).HasColumnName("orbitRadius");

                entity.Property(e => e.Pressure).HasColumnName("pressure");

                entity.Property(e => e.Radius).HasColumnName("radius");

                entity.Property(e => e.RotationRate).HasColumnName("rotationRate");

                entity.Property(e => e.SpectralClass)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("spectralClass");

                entity.Property(e => e.SurfaceGravity).HasColumnName("surfaceGravity");

                entity.Property(e => e.Temperature).HasColumnName("temperature");
            });

            modelBuilder.Entity<MapConstellation>(entity =>
            {
                entity.HasKey(e => e.ConstellationId)
                    .HasName("PK__mapConst__024F857909EF5040");

                entity.ToTable("mapConstellations");

                entity.Property(e => e.ConstellationId)
                    .ValueGeneratedNever()
                    .HasColumnName("constellationID");

                entity.Property(e => e.ConstellationName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("constellationName");

                entity.Property(e => e.FactionId).HasColumnName("factionID");

                entity.Property(e => e.Radius).HasColumnName("radius");

                entity.Property(e => e.RegionId).HasColumnName("regionID");

                entity.Property(e => e.X).HasColumnName("x");

                entity.Property(e => e.XMax).HasColumnName("xMax");

                entity.Property(e => e.XMin).HasColumnName("xMin");

                entity.Property(e => e.Y).HasColumnName("y");

                entity.Property(e => e.YMax).HasColumnName("yMax");

                entity.Property(e => e.YMin).HasColumnName("yMin");

                entity.Property(e => e.Z).HasColumnName("z");

                entity.Property(e => e.ZMax).HasColumnName("zMax");

                entity.Property(e => e.ZMin).HasColumnName("zMin");
            });

            modelBuilder.Entity<MapConstellationJump>(entity =>
            {
                entity.HasKey(e => new { e.FromConstellationId, e.ToConstellationId })
                    .HasName("PK__mapConst__1D13C556768D3780");

                entity.ToTable("mapConstellationJumps");

                entity.Property(e => e.FromConstellationId).HasColumnName("fromConstellationID");

                entity.Property(e => e.ToConstellationId).HasColumnName("toConstellationID");

                entity.Property(e => e.FromRegionId).HasColumnName("fromRegionID");

                entity.Property(e => e.ToRegionId).HasColumnName("toRegionID");
            });

            modelBuilder.Entity<MapDenormalize>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PK__mapDenor__56A1284AF39FD179");

                entity.ToTable("mapDenormalize");

                entity.HasIndex(e => e.ConstellationId, "ix_mapDenormalize_constellationID");

                entity.HasIndex(e => e.OrbitId, "ix_mapDenormalize_orbitID");

                entity.HasIndex(e => e.RegionId, "ix_mapDenormalize_regionID");

                entity.HasIndex(e => e.SolarSystemId, "ix_mapDenormalize_solarSystemID");

                entity.HasIndex(e => e.TypeId, "ix_mapDenormalize_typeID");

                entity.HasIndex(e => new { e.GroupId, e.ConstellationId }, "mapDenormalize_IX_groupConstellation");

                entity.HasIndex(e => new { e.GroupId, e.RegionId }, "mapDenormalize_IX_groupRegion");

                entity.HasIndex(e => new { e.GroupId, e.SolarSystemId }, "mapDenormalize_IX_groupSystem");

                entity.Property(e => e.ItemId)
                    .ValueGeneratedNever()
                    .HasColumnName("itemID");

                entity.Property(e => e.CelestialIndex).HasColumnName("celestialIndex");

                entity.Property(e => e.ConstellationId).HasColumnName("constellationID");

                entity.Property(e => e.GroupId).HasColumnName("groupID");

                entity.Property(e => e.ItemName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("itemName");

                entity.Property(e => e.OrbitId).HasColumnName("orbitID");

                entity.Property(e => e.OrbitIndex).HasColumnName("orbitIndex");

                entity.Property(e => e.Radius).HasColumnName("radius");

                entity.Property(e => e.RegionId).HasColumnName("regionID");

                entity.Property(e => e.Security).HasColumnName("security");

                entity.Property(e => e.SolarSystemId).HasColumnName("solarSystemID");

                entity.Property(e => e.TypeId).HasColumnName("typeID");

                entity.Property(e => e.X).HasColumnName("x");

                entity.Property(e => e.Y).HasColumnName("y");

                entity.Property(e => e.Z).HasColumnName("z");
            });

            modelBuilder.Entity<MapJump>(entity =>
            {
                entity.HasKey(e => e.StargateId)
                    .HasName("PK__mapJumps__F0F5445B99E39F41");

                entity.ToTable("mapJumps");

                entity.Property(e => e.StargateId)
                    .ValueGeneratedNever()
                    .HasColumnName("stargateID");

                entity.Property(e => e.DestinationId).HasColumnName("destinationID");
            });

            modelBuilder.Entity<MapLandmark>(entity =>
            {
                entity.HasKey(e => e.LandmarkId)
                    .HasName("PK__mapLandm__DC9AA17B940D1F2F");

                entity.ToTable("mapLandmarks");

                entity.Property(e => e.LandmarkId)
                    .ValueGeneratedNever()
                    .HasColumnName("landmarkID");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.IconId).HasColumnName("iconID");

                entity.Property(e => e.LandmarkName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("landmarkName");

                entity.Property(e => e.LocationId).HasColumnName("locationID");

                entity.Property(e => e.X).HasColumnName("x");

                entity.Property(e => e.Y).HasColumnName("y");

                entity.Property(e => e.Z).HasColumnName("z");
            });

            modelBuilder.Entity<MapLocationScene>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__mapLocat__30646B0EB2C58D06");

                entity.ToTable("mapLocationScenes");

                entity.Property(e => e.LocationId)
                    .ValueGeneratedNever()
                    .HasColumnName("locationID");

                entity.Property(e => e.GraphicId).HasColumnName("graphicID");
            });

            modelBuilder.Entity<MapLocationWormholeClass>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__mapLocat__30646B0ECF90A773");

                entity.ToTable("mapLocationWormholeClasses");

                entity.Property(e => e.LocationId)
                    .ValueGeneratedNever()
                    .HasColumnName("locationID");

                entity.Property(e => e.WormholeClassId).HasColumnName("wormholeClassID");
            });

            modelBuilder.Entity<MapRegion>(entity =>
            {
                entity.HasKey(e => e.RegionId)
                    .HasName("PK__mapRegio__15EA9088FF8F1411");

                entity.ToTable("mapRegions");

                entity.Property(e => e.RegionId)
                    .ValueGeneratedNever()
                    .HasColumnName("regionID");

                entity.Property(e => e.FactionId).HasColumnName("factionID");

                entity.Property(e => e.Nebula).HasColumnName("nebula");

                entity.Property(e => e.Radius).HasColumnName("radius");

                entity.Property(e => e.RegionName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("regionName");

                entity.Property(e => e.X).HasColumnName("x");

                entity.Property(e => e.XMax).HasColumnName("xMax");

                entity.Property(e => e.XMin).HasColumnName("xMin");

                entity.Property(e => e.Y).HasColumnName("y");

                entity.Property(e => e.YMax).HasColumnName("yMax");

                entity.Property(e => e.YMin).HasColumnName("yMin");

                entity.Property(e => e.Z).HasColumnName("z");

                entity.Property(e => e.ZMax).HasColumnName("zMax");

                entity.Property(e => e.ZMin).HasColumnName("zMin");
            });

            modelBuilder.Entity<MapRegionJump>(entity =>
            {
                entity.HasKey(e => new { e.FromRegionId, e.ToRegionId })
                    .HasName("PK__mapRegio__D11CB669F58C321C");

                entity.ToTable("mapRegionJumps");

                entity.Property(e => e.FromRegionId).HasColumnName("fromRegionID");

                entity.Property(e => e.ToRegionId).HasColumnName("toRegionID");
            });

            modelBuilder.Entity<MapSolarSystem>(entity =>
            {
                entity.HasKey(e => e.SolarSystemId)
                    .HasName("PK__mapSolar__8DD88C9349440B70");

                entity.ToTable("mapSolarSystems");

                entity.HasIndex(e => e.ConstellationId, "ix_mapSolarSystems_constellationID");

                entity.HasIndex(e => e.RegionId, "ix_mapSolarSystems_regionID");

                entity.HasIndex(e => e.Security, "ix_mapSolarSystems_security");

                entity.Property(e => e.SolarSystemId)
                    .ValueGeneratedNever()
                    .HasColumnName("solarSystemID");

                entity.Property(e => e.Border).HasColumnName("border");

                entity.Property(e => e.Constellation).HasColumnName("constellation");

                entity.Property(e => e.ConstellationId).HasColumnName("constellationID");

                entity.Property(e => e.Corridor).HasColumnName("corridor");

                entity.Property(e => e.FactionId).HasColumnName("factionID");

                entity.Property(e => e.Fringe).HasColumnName("fringe");

                entity.Property(e => e.Hub).HasColumnName("hub");

                entity.Property(e => e.International).HasColumnName("international");

                entity.Property(e => e.Luminosity).HasColumnName("luminosity");

                entity.Property(e => e.Radius).HasColumnName("radius");

                entity.Property(e => e.RegionId).HasColumnName("regionID");

                entity.Property(e => e.Regional).HasColumnName("regional");

                entity.Property(e => e.Security).HasColumnName("security");

                entity.Property(e => e.SecurityClass)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("securityClass");

                entity.Property(e => e.SolarSystemName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("solarSystemName");

                entity.Property(e => e.SunTypeId).HasColumnName("sunTypeID");

                entity.Property(e => e.X).HasColumnName("x");

                entity.Property(e => e.XMax).HasColumnName("xMax");

                entity.Property(e => e.XMin).HasColumnName("xMin");

                entity.Property(e => e.Y).HasColumnName("y");

                entity.Property(e => e.YMax).HasColumnName("yMax");

                entity.Property(e => e.YMin).HasColumnName("yMin");

                entity.Property(e => e.Z).HasColumnName("z");

                entity.Property(e => e.ZMax).HasColumnName("zMax");

                entity.Property(e => e.ZMin).HasColumnName("zMin");
            });

            modelBuilder.Entity<MapSolarSystemJump>(entity =>
            {
                entity.HasKey(e => new { e.FromSolarSystemId, e.ToSolarSystemId })
                    .HasName("PK__mapSolar__A576C1004D6F24AF");

                entity.ToTable("mapSolarSystemJumps");

                entity.Property(e => e.FromSolarSystemId).HasColumnName("fromSolarSystemID");

                entity.Property(e => e.ToSolarSystemId).HasColumnName("toSolarSystemID");

                entity.Property(e => e.FromConstellationId).HasColumnName("fromConstellationID");

                entity.Property(e => e.FromRegionId).HasColumnName("fromRegionID");

                entity.Property(e => e.ToConstellationId).HasColumnName("toConstellationID");

                entity.Property(e => e.ToRegionId).HasColumnName("toRegionID");
            });

            modelBuilder.Entity<MapUniverse>(entity =>
            {
                entity.HasKey(e => e.UniverseId)
                    .HasName("PK__mapUnive__4569484B93865ADB");

                entity.ToTable("mapUniverse");

                entity.Property(e => e.UniverseId)
                    .ValueGeneratedNever()
                    .HasColumnName("universeID");

                entity.Property(e => e.Radius).HasColumnName("radius");

                entity.Property(e => e.UniverseName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("universeName");

                entity.Property(e => e.X).HasColumnName("x");

                entity.Property(e => e.XMax).HasColumnName("xMax");

                entity.Property(e => e.XMin).HasColumnName("xMin");

                entity.Property(e => e.Y).HasColumnName("y");

                entity.Property(e => e.YMax).HasColumnName("yMax");

                entity.Property(e => e.YMin).HasColumnName("yMin");

                entity.Property(e => e.Z).HasColumnName("z");

                entity.Property(e => e.ZMax).HasColumnName("zMax");

                entity.Property(e => e.ZMin).HasColumnName("zMin");
            });

            modelBuilder.Entity<PlanetSchematic>(entity =>
            {
                entity.HasKey(e => e.SchematicId)
                    .HasName("PK__planetSc__A84EA046CF59E76A");

                entity.ToTable("planetSchematics");

                entity.Property(e => e.SchematicId)
                    .ValueGeneratedNever()
                    .HasColumnName("schematicID");

                entity.Property(e => e.CycleTime).HasColumnName("cycleTime");

                entity.Property(e => e.SchematicName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("schematicName");
            });

            modelBuilder.Entity<PlanetSchematicsPinMap>(entity =>
            {
                entity.HasKey(e => new { e.SchematicId, e.PinTypeId })
                    .HasName("PK__planetSc__2F2D0BD8A6927182");

                entity.ToTable("planetSchematicsPinMap");

                entity.Property(e => e.SchematicId).HasColumnName("schematicID");

                entity.Property(e => e.PinTypeId).HasColumnName("pinTypeID");
            });

            modelBuilder.Entity<PlanetSchematicsTypeMap>(entity =>
            {
                entity.HasKey(e => new { e.SchematicId, e.TypeId })
                    .HasName("PK__planetSc__174A7F57C80995E6");

                entity.ToTable("planetSchematicsTypeMap");

                entity.Property(e => e.SchematicId).HasColumnName("schematicID");

                entity.Property(e => e.TypeId).HasColumnName("typeID");

                entity.Property(e => e.IsInput).HasColumnName("isInput");

                entity.Property(e => e.Quantity).HasColumnName("quantity");
            });

            modelBuilder.Entity<RamActivity>(entity =>
            {
                entity.HasKey(e => e.ActivityId)
                    .HasName("PK__ramActiv__0FC9CBCCCDAD7C75");

                entity.ToTable("ramActivities");

                entity.Property(e => e.ActivityId)
                    .ValueGeneratedNever()
                    .HasColumnName("activityID");

                entity.Property(e => e.ActivityName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("activityName");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IconNo)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("iconNo");

                entity.Property(e => e.Published).HasColumnName("published");
            });

            modelBuilder.Entity<RamAssemblyLineStation>(entity =>
            {
                entity.HasKey(e => new { e.StationId, e.AssemblyLineTypeId })
                    .HasName("PK__ramAssem__7FEE94267E53539D");

                entity.ToTable("ramAssemblyLineStations");

                entity.HasIndex(e => e.OwnerId, "ix_ramAssemblyLineStations_ownerID");

                entity.HasIndex(e => e.RegionId, "ix_ramAssemblyLineStations_regionID");

                entity.HasIndex(e => e.SolarSystemId, "ix_ramAssemblyLineStations_solarSystemID");

                entity.Property(e => e.StationId).HasColumnName("stationID");

                entity.Property(e => e.AssemblyLineTypeId).HasColumnName("assemblyLineTypeID");

                entity.Property(e => e.OwnerId).HasColumnName("ownerID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.RegionId).HasColumnName("regionID");

                entity.Property(e => e.SolarSystemId).HasColumnName("solarSystemID");

                entity.Property(e => e.StationTypeId).HasColumnName("stationTypeID");
            });

            modelBuilder.Entity<RamAssemblyLineType>(entity =>
            {
                entity.HasKey(e => e.AssemblyLineTypeId)
                    .HasName("PK__ramAssem__F4967C69040AB493");

                entity.ToTable("ramAssemblyLineTypes");

                entity.Property(e => e.AssemblyLineTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("assemblyLineTypeID");

                entity.Property(e => e.ActivityId).HasColumnName("activityID");

                entity.Property(e => e.AssemblyLineTypeName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("assemblyLineTypeName");

                entity.Property(e => e.BaseCostMultiplier).HasColumnName("baseCostMultiplier");

                entity.Property(e => e.BaseMaterialMultiplier).HasColumnName("baseMaterialMultiplier");

                entity.Property(e => e.BaseTimeMultiplier).HasColumnName("baseTimeMultiplier");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.MinCostPerHour).HasColumnName("minCostPerHour");

                entity.Property(e => e.Volume).HasColumnName("volume");
            });

            modelBuilder.Entity<RamAssemblyLineTypeDetailPerCategory>(entity =>
            {
                entity.HasKey(e => new { e.AssemblyLineTypeId, e.CategoryId })
                    .HasName("PK__ramAssem__66AAD376A412194F");

                entity.ToTable("ramAssemblyLineTypeDetailPerCategory");

                entity.Property(e => e.AssemblyLineTypeId).HasColumnName("assemblyLineTypeID");

                entity.Property(e => e.CategoryId).HasColumnName("categoryID");

                entity.Property(e => e.CostMultiplier).HasColumnName("costMultiplier");

                entity.Property(e => e.MaterialMultiplier).HasColumnName("materialMultiplier");

                entity.Property(e => e.TimeMultiplier).HasColumnName("timeMultiplier");
            });

            modelBuilder.Entity<RamAssemblyLineTypeDetailPerGroup>(entity =>
            {
                entity.HasKey(e => new { e.AssemblyLineTypeId, e.GroupId })
                    .HasName("PK__ramAssem__3C1A6C4381443129");

                entity.ToTable("ramAssemblyLineTypeDetailPerGroup");

                entity.Property(e => e.AssemblyLineTypeId).HasColumnName("assemblyLineTypeID");

                entity.Property(e => e.GroupId).HasColumnName("groupID");

                entity.Property(e => e.CostMultiplier).HasColumnName("costMultiplier");

                entity.Property(e => e.MaterialMultiplier).HasColumnName("materialMultiplier");

                entity.Property(e => e.TimeMultiplier).HasColumnName("timeMultiplier");
            });

            modelBuilder.Entity<RamInstallationTypeContent>(entity =>
            {
                entity.HasKey(e => new { e.InstallationTypeId, e.AssemblyLineTypeId })
                    .HasName("PK__ramInsta__EA61FA1C7588F686");

                entity.ToTable("ramInstallationTypeContents");

                entity.Property(e => e.InstallationTypeId).HasColumnName("installationTypeID");

                entity.Property(e => e.AssemblyLineTypeId).HasColumnName("assemblyLineTypeID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");
            });

            modelBuilder.Entity<Skin>(entity =>
            {
                entity.ToTable("skins");

                entity.Property(e => e.SkinId)
                    .ValueGeneratedNever()
                    .HasColumnName("skinID");

                entity.Property(e => e.InternalName)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("internalName");

                entity.Property(e => e.SkinMaterialId).HasColumnName("skinMaterialID");
            });

            modelBuilder.Entity<SkinLicense>(entity =>
            {
                entity.HasKey(e => e.LicenseTypeId)
                    .HasName("PK__skinLice__98D8B2A8D877AFF3");

                entity.ToTable("skinLicense");

                entity.Property(e => e.LicenseTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("licenseTypeID");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.SkinId).HasColumnName("skinID");
            });

            modelBuilder.Entity<SkinMaterial>(entity =>
            {
                entity.ToTable("skinMaterials");

                entity.Property(e => e.SkinMaterialId)
                    .ValueGeneratedNever()
                    .HasColumnName("skinMaterialID");

                entity.Property(e => e.DisplayNameId).HasColumnName("displayNameID");

                entity.Property(e => e.MaterialSetId).HasColumnName("materialSetID");
            });

            modelBuilder.Entity<SkinShip>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("skinShip");

                entity.HasIndex(e => e.SkinId, "ix_skinShip_skinID");

                entity.HasIndex(e => e.TypeId, "ix_skinShip_typeID");

                entity.Property(e => e.SkinId).HasColumnName("skinID");

                entity.Property(e => e.TypeId).HasColumnName("typeID");
            });

            modelBuilder.Entity<StaOperation>(entity =>
            {
                entity.HasKey(e => e.OperationId)
                    .HasName("PK__staOpera__34C2D1B9CEFB74DD");

                entity.ToTable("staOperations");

                entity.Property(e => e.OperationId)
                    .ValueGeneratedNever()
                    .HasColumnName("operationID");

                entity.Property(e => e.ActivityId).HasColumnName("activityID");

                entity.Property(e => e.AmarrStationTypeId).HasColumnName("amarrStationTypeID");

                entity.Property(e => e.Border).HasColumnName("border");

                entity.Property(e => e.CaldariStationTypeId).HasColumnName("caldariStationTypeID");

                entity.Property(e => e.Corridor).HasColumnName("corridor");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Fringe).HasColumnName("fringe");

                entity.Property(e => e.GallenteStationTypeId).HasColumnName("gallenteStationTypeID");

                entity.Property(e => e.Hub).HasColumnName("hub");

                entity.Property(e => e.JoveStationTypeId).HasColumnName("joveStationTypeID");

                entity.Property(e => e.MinmatarStationTypeId).HasColumnName("minmatarStationTypeID");

                entity.Property(e => e.OperationName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("operationName");

                entity.Property(e => e.Ratio).HasColumnName("ratio");
            });

            modelBuilder.Entity<StaOperationService>(entity =>
            {
                entity.HasKey(e => new { e.OperationId, e.ServiceId })
                    .HasName("PK__staOpera__D097D68A589794E2");

                entity.ToTable("staOperationServices");

                entity.Property(e => e.OperationId).HasColumnName("operationID");

                entity.Property(e => e.ServiceId).HasColumnName("serviceID");
            });

            modelBuilder.Entity<StaService>(entity =>
            {
                entity.HasKey(e => e.ServiceId)
                    .HasName("PK__staServi__4550733F0BD23598");

                entity.ToTable("staServices");

                entity.Property(e => e.ServiceId)
                    .ValueGeneratedNever()
                    .HasColumnName("serviceID");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.ServiceName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("serviceName");
            });

            modelBuilder.Entity<StaStation>(entity =>
            {
                entity.HasKey(e => e.StationId)
                    .HasName("PK__staStati__F0A7F3E08F65AE05");

                entity.ToTable("staStations");

                entity.HasIndex(e => e.ConstellationId, "ix_staStations_constellationID");

                entity.HasIndex(e => e.CorporationId, "ix_staStations_corporationID");

                entity.HasIndex(e => e.OperationId, "ix_staStations_operationID");

                entity.HasIndex(e => e.RegionId, "ix_staStations_regionID");

                entity.HasIndex(e => e.SolarSystemId, "ix_staStations_solarSystemID");

                entity.HasIndex(e => e.StationTypeId, "ix_staStations_stationTypeID");

                entity.Property(e => e.StationId)
                    .ValueGeneratedNever()
                    .HasColumnName("stationID");

                entity.Property(e => e.ConstellationId).HasColumnName("constellationID");

                entity.Property(e => e.CorporationId).HasColumnName("corporationID");

                entity.Property(e => e.DockingCostPerVolume).HasColumnName("dockingCostPerVolume");

                entity.Property(e => e.MaxShipVolumeDockable).HasColumnName("maxShipVolumeDockable");

                entity.Property(e => e.OfficeRentalCost).HasColumnName("officeRentalCost");

                entity.Property(e => e.OperationId).HasColumnName("operationID");

                entity.Property(e => e.RegionId).HasColumnName("regionID");

                entity.Property(e => e.ReprocessingEfficiency).HasColumnName("reprocessingEfficiency");

                entity.Property(e => e.ReprocessingHangarFlag).HasColumnName("reprocessingHangarFlag");

                entity.Property(e => e.ReprocessingStationsTake).HasColumnName("reprocessingStationsTake");

                entity.Property(e => e.Security).HasColumnName("security");

                entity.Property(e => e.SolarSystemId).HasColumnName("solarSystemID");

                entity.Property(e => e.StationName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("stationName");

                entity.Property(e => e.StationTypeId).HasColumnName("stationTypeID");

                entity.Property(e => e.X).HasColumnName("x");

                entity.Property(e => e.Y).HasColumnName("y");

                entity.Property(e => e.Z).HasColumnName("z");
            });

            modelBuilder.Entity<StaStationType>(entity =>
            {
                entity.HasKey(e => e.StationTypeId)
                    .HasName("PK__staStati__0C27685CC42172D2");

                entity.ToTable("staStationTypes");

                entity.Property(e => e.StationTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("stationTypeID");

                entity.Property(e => e.Conquerable).HasColumnName("conquerable");

                entity.Property(e => e.DockEntryX).HasColumnName("dockEntryX");

                entity.Property(e => e.DockEntryY).HasColumnName("dockEntryY");

                entity.Property(e => e.DockEntryZ).HasColumnName("dockEntryZ");

                entity.Property(e => e.DockOrientationX).HasColumnName("dockOrientationX");

                entity.Property(e => e.DockOrientationY).HasColumnName("dockOrientationY");

                entity.Property(e => e.DockOrientationZ).HasColumnName("dockOrientationZ");

                entity.Property(e => e.OfficeSlots).HasColumnName("officeSlots");

                entity.Property(e => e.OperationId).HasColumnName("operationID");

                entity.Property(e => e.ReprocessingEfficiency).HasColumnName("reprocessingEfficiency");
            });

            modelBuilder.Entity<TranslationTable>(entity =>
            {
                entity.HasKey(e => new { e.SourceTable, e.TranslatedKey })
                    .HasName("PK__translat__F46A12AB4CB465B6");

                entity.ToTable("translationTables");

                entity.Property(e => e.SourceTable)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("sourceTable");

                entity.Property(e => e.TranslatedKey)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("translatedKey");

                entity.Property(e => e.DestinationTable)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("destinationTable");

                entity.Property(e => e.TcGroupId).HasColumnName("tcGroupID");

                entity.Property(e => e.TcId).HasColumnName("tcID");
            });

            modelBuilder.Entity<TrnTranslation>(entity =>
            {
                entity.HasKey(e => new { e.TcId, e.KeyId, e.LanguageId })
                    .HasName("PK__trnTrans__35675F1555A91158");

                entity.ToTable("trnTranslations");

                entity.Property(e => e.TcId).HasColumnName("tcID");

                entity.Property(e => e.KeyId).HasColumnName("keyID");

                entity.Property(e => e.LanguageId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("languageID");

                entity.Property(e => e.Text).HasColumnName("text");
            });

            modelBuilder.Entity<TrnTranslationColumn>(entity =>
            {
                entity.HasKey(e => e.TcId)
                    .HasName("PK__trnTrans__E072AA626E04F410");

                entity.ToTable("trnTranslationColumns");

                entity.Property(e => e.TcId)
                    .ValueGeneratedNever()
                    .HasColumnName("tcID");

                entity.Property(e => e.ColumnName)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("columnName");

                entity.Property(e => e.MasterId)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("masterID");

                entity.Property(e => e.TableName)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("tableName");

                entity.Property(e => e.TcGroupId).HasColumnName("tcGroupID");
            });

            modelBuilder.Entity<TrnTranslationLanguage>(entity =>
            {
                entity.HasKey(e => e.NumericLanguageId)
                    .HasName("PK__trnTrans__38AFAE0533E984B2");

                entity.ToTable("trnTranslationLanguages");

                entity.Property(e => e.NumericLanguageId)
                    .ValueGeneratedNever()
                    .HasColumnName("numericLanguageID");

                entity.Property(e => e.LanguageId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("languageID");

                entity.Property(e => e.LanguageName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("languageName");
            });

            modelBuilder.Entity<WarCombatZone>(entity =>
            {
                entity.HasKey(e => e.CombatZoneId)
                    .HasName("PK__warComba__F2212FCE25AA4DC0");

                entity.ToTable("warCombatZones");

                entity.Property(e => e.CombatZoneId)
                    .ValueGeneratedNever()
                    .HasColumnName("combatZoneID");

                entity.Property(e => e.CenterSystemId).HasColumnName("centerSystemID");

                entity.Property(e => e.CombatZoneName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("combatZoneName");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.FactionId).HasColumnName("factionID");
            });

            modelBuilder.Entity<WarCombatZoneSystem>(entity =>
            {
                entity.HasKey(e => e.SolarSystemId)
                    .HasName("PK__warComba__8DD88C9348D9F643");

                entity.ToTable("warCombatZoneSystems");

                entity.Property(e => e.SolarSystemId)
                    .ValueGeneratedNever()
                    .HasColumnName("solarSystemID");

                entity.Property(e => e.CombatZoneId).HasColumnName("combatZoneID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
