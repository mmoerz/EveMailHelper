// <auto-generated />
using System;
using EveMailHelper.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    [DbContext(typeof(EveMailHelperContext))]
    [Migration("20221127001258_MigrationAlliance2")]
    partial class MigrationAlliance2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AccountRole", b =>
                {
                    b.Property<Guid>("AccountsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RolesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AccountsId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("AccountRole", "Security");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Alliance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatorCorporationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateFounded")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EveDeleteInGame")
                        .HasColumnType("bit");

                    b.Property<int>("EveId")
                        .HasColumnType("int");

                    b.Property<Guid?>("ExecutorCorporationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Ticker")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorCorporationId")
                        .IsUnique();

                    b.HasIndex("CreatorId")
                        .IsUnique();

                    b.HasIndex("ExecutorCorporationId")
                        .IsUnique()
                        .HasFilter("[ExecutorCorporationId] IS NOT NULL");

                    b.ToTable("Alliances");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("BloodlineId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CorporationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(8000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EveAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("EveDeletedInGame")
                        .HasColumnType("bit");

                    b.Property<int>("EveId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EveLastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("FactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsExcluded")
                        .HasColumnType("bit");

                    b.Property<bool>("IsInRecruitment")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("RaceId")
                        .HasColumnType("int");

                    b.Property<int?>("ReallifeAge")
                        .HasColumnType("int");

                    b.Property<string>("RecruitmentNote")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<float?>("SecurityStatus")
                        .HasColumnType("real");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("None");

                    b.Property<string>("Title")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CorporationId");

                    b.HasIndex("EveAccountId");

                    b.ToTable("Character", (string)null);
                });

            modelBuilder.Entity("EveMailHelper.DataModels.CharacterAuthInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("random number for added security");

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)")
                        .HasComment("oauth accesstoken");

                    b.Property<Guid?>("CharId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpiresUTC")
                        .HasColumnType("datetime2");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasMaxLength(1536)
                        .HasColumnType("nvarchar(1536)")
                        .HasComment("oauth refreshtoken");

                    b.Property<string>("Scopes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TokenType")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasComment("??");

                    b.HasKey("Id");

                    b.HasIndex("CharId");

                    b.ToTable("CharacterAuthInfos");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Chat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AttachedToId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ChannelName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<Guid>("ChatFileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ListenerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SessionStarted")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AttachedToId");

                    b.HasIndex("ChatFileId")
                        .IsUnique();

                    b.HasIndex("ListenerId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.ChatFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Content")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("ChatFiles");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.ChatMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(8000)
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ChatId");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Corporation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AllianceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CeoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateFounded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<bool>("EveDeletedInGame")
                        .HasColumnType("bit");

                    b.Property<int>("EveId")
                        .HasColumnType("int");

                    b.Property<Guid?>("FactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("HomeStationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MemberCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<long?>("Shares")
                        .HasColumnType("bigint");

                    b.Property<float>("TaxRate")
                        .HasColumnType("real");

                    b.Property<string>("Ticker")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Url")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("WarEligible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AllianceId");

                    b.HasIndex("CeoId")
                        .IsUnique()
                        .HasFilter("[CeoId] IS NOT NULL");

                    b.HasIndex("CreatorId")
                        .IsUnique()
                        .HasFilter("[CreatorId] IS NOT NULL");

                    b.ToTable("Corporations");

                    b.HasData(
                        new
                        {
                            Id = new Guid("11110000-0000-0000-0000-000011110000"),
                            DateFounded = new DateTime(2022, 11, 27, 1, 12, 58, 108, DateTimeKind.Local).AddTicks(4937),
                            Description = "Noname Default",
                            EveDeletedInGame = false,
                            EveId = 0,
                            MemberCount = 0,
                            Name = "Noname Default",
                            TaxRate = 0f,
                            Ticker = "Noname Default",
                            WarEligible = false
                        });
                });

            modelBuilder.Entity("EveMailHelper.DataModels.EveMailRecipient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MailId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MailId");

                    b.ToTable("EveMailRecipient");

                    b.HasDiscriminator<string>("Discriminator").HasValue("EveMailRecipient");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.EveMailSentTo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EveMailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SentDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("EveMailId");

                    b.ToTable("EveMailSentTo", (string)null);
                });

            modelBuilder.Entity("EveMailHelper.DataModels.EveMailTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(8000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("EveMailTemplate", (string)null);
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Mail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(16000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("EveId")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("EveMailTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FromId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("EveMailTemplateId");

                    b.HasIndex("FromId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Mail", "Eve");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.MailLabel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long?>("EveLabelId")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("MailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int?>("UnreadCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("MailId");

                    b.ToTable("EveMailLabels");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.MailList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("EveId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MailLists");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Note", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AttachedToId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("AttachedToId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Security.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("LastName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Account", "Security");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Security.EveAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("EveAccount", "Security");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Security.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Permission", "Security");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Security.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Role", "Security");
                });

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.Property<Guid>("PermissionsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RolesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PermissionsId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("PermissionRole", "Security");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.EveMailRecipientCharacter", b =>
                {
                    b.HasBaseType("EveMailHelper.DataModels.EveMailRecipient");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("CharacterId");

                    b.HasDiscriminator().HasValue("EveMailRecipientCharacter");
                });

            modelBuilder.Entity("AccountRole", b =>
                {
                    b.HasOne("EveMailHelper.DataModels.Security.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EveMailHelper.DataModels.Security.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Alliance", b =>
                {
                    b.HasOne("EveMailHelper.DataModels.Corporation", "CreatorCorporation")
                        .WithOne()
                        .HasForeignKey("EveMailHelper.DataModels.Alliance", "CreatorCorporationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EveMailHelper.DataModels.Character", "Creator")
                        .WithOne()
                        .HasForeignKey("EveMailHelper.DataModels.Alliance", "CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EveMailHelper.DataModels.Corporation", "ExecutorCorporation")
                        .WithOne()
                        .HasForeignKey("EveMailHelper.DataModels.Alliance", "ExecutorCorporationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Creator");

                    b.Navigation("CreatorCorporation");

                    b.Navigation("ExecutorCorporation");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Character", b =>
                {
                    b.HasOne("EveMailHelper.DataModels.Security.Account", "Account")
                        .WithMany("Characters")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EveMailHelper.DataModels.Corporation", "Corporation")
                        .WithMany("Members")
                        .HasForeignKey("CorporationId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("EveMailHelper.DataModels.Security.EveAccount", "EveAccount")
                        .WithMany("Characters")
                        .HasForeignKey("EveAccountId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Corporation");

                    b.Navigation("EveAccount");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.CharacterAuthInfo", b =>
                {
                    b.HasOne("EveMailHelper.DataModels.Character", "Char")
                        .WithMany()
                        .HasForeignKey("CharId");

                    b.Navigation("Char");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Chat", b =>
                {
                    b.HasOne("EveMailHelper.DataModels.Character", "AttachedTo")
                        .WithMany("Chats")
                        .HasForeignKey("AttachedToId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("EveMailHelper.DataModels.ChatFile", "ChatFile")
                        .WithOne("Chat")
                        .HasForeignKey("EveMailHelper.DataModels.Chat", "ChatFileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EveMailHelper.DataModels.Character", "Listener")
                        .WithMany()
                        .HasForeignKey("ListenerId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("AttachedTo");

                    b.Navigation("ChatFile");

                    b.Navigation("Listener");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.ChatMessage", b =>
                {
                    b.HasOne("EveMailHelper.DataModels.Character", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("EveMailHelper.DataModels.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Chat");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Corporation", b =>
                {
                    b.HasOne("EveMailHelper.DataModels.Alliance", "Alliance")
                        .WithMany("Corporations")
                        .HasForeignKey("AllianceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EveMailHelper.DataModels.Character", "Ceo")
                        .WithOne()
                        .HasForeignKey("EveMailHelper.DataModels.Corporation", "CeoId");

                    b.HasOne("EveMailHelper.DataModels.Character", "Createor")
                        .WithOne()
                        .HasForeignKey("EveMailHelper.DataModels.Corporation", "CreatorId");

                    b.Navigation("Alliance");

                    b.Navigation("Ceo");

                    b.Navigation("Createor");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.EveMailRecipient", b =>
                {
                    b.HasOne("EveMailHelper.DataModels.Mail", null)
                        .WithMany("Recipients")
                        .HasForeignKey("MailId");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.EveMailSentTo", b =>
                {
                    b.HasOne("EveMailHelper.DataModels.Character", "Character")
                        .WithMany("EveMailReceived")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EveMailHelper.DataModels.Mail", "EveMail")
                        .WithMany("SentTo")
                        .HasForeignKey("EveMailId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("EveMail");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Mail", b =>
                {
                    b.HasOne("EveMailHelper.DataModels.EveMailTemplate", "EveMailTemplate")
                        .WithMany("EveMailsGenerated")
                        .HasForeignKey("EveMailTemplateId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("EveMailHelper.DataModels.Character", "From")
                        .WithMany()
                        .HasForeignKey("FromId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("EveMailHelper.DataModels.Character", "Owner")
                        .WithMany("Mails")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("EveMailTemplate");

                    b.Navigation("From");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.MailLabel", b =>
                {
                    b.HasOne("EveMailHelper.DataModels.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("EveMailHelper.DataModels.Mail", null)
                        .WithMany("Labels")
                        .HasForeignKey("MailId");

                    b.Navigation("Character");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Note", b =>
                {
                    b.HasOne("EveMailHelper.DataModels.Character", "AttachedTo")
                        .WithMany("Notes")
                        .HasForeignKey("AttachedToId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("AttachedTo");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Security.EveAccount", b =>
                {
                    b.HasOne("EveMailHelper.DataModels.Security.Account", "Account")
                        .WithMany("EveAccounts")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.HasOne("EveMailHelper.DataModels.Security.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EveMailHelper.DataModels.Security.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EveMailHelper.DataModels.EveMailRecipientCharacter", b =>
                {
                    b.HasOne("EveMailHelper.DataModels.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Alliance", b =>
                {
                    b.Navigation("Corporations");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Character", b =>
                {
                    b.Navigation("Chats");

                    b.Navigation("EveMailReceived");

                    b.Navigation("Mails");

                    b.Navigation("Notes");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Chat", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.ChatFile", b =>
                {
                    b.Navigation("Chat")
                        .IsRequired();
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Corporation", b =>
                {
                    b.Navigation("Members");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.EveMailTemplate", b =>
                {
                    b.Navigation("EveMailsGenerated");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Mail", b =>
                {
                    b.Navigation("Labels");

                    b.Navigation("Recipients");

                    b.Navigation("SentTo");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Security.Account", b =>
                {
                    b.Navigation("Characters");

                    b.Navigation("EveAccounts");
                });

            modelBuilder.Entity("EveMailHelper.DataModels.Security.EveAccount", b =>
                {
                    b.Navigation("Characters");
                });
#pragma warning restore 612, 618
        }
    }
}
