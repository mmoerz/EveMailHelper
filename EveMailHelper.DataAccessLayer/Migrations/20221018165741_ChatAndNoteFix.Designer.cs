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
    [Migration("20221018165741_ChatAndNoteFix")]
    partial class ChatAndNoteFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EveMailHelper.DataAccessLayer.Models.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<bool>("IsExcluded")
                        .HasColumnType("bit");

                    b.Property<bool>("IsInRecruitment")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int?>("ReallifeAge")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Character", (string)null);
                });

            modelBuilder.Entity("EveMailHelper.DataAccessLayer.Models.Chat", b =>
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

                    b.Property<Guid>("ListenerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SessionStarted")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AttachedToId");

                    b.HasIndex("ListenerId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("EveMailHelper.DataAccessLayer.Models.ChatMessage", b =>
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

            modelBuilder.Entity("EveMailHelper.DataAccessLayer.Models.EveMail", b =>
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

                    b.Property<Guid?>("EveMailTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("EveMailTemplateId");

                    b.ToTable("EveMails");
                });

            modelBuilder.Entity("EveMailHelper.DataAccessLayer.Models.EveMailSentTo", b =>
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

            modelBuilder.Entity("EveMailHelper.DataAccessLayer.Models.EveMailTemplate", b =>
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

            modelBuilder.Entity("EveMailHelper.DataAccessLayer.Models.Note", b =>
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

            modelBuilder.Entity("EveMailHelper.DataAccessLayer.Models.Chat", b =>
                {
                    b.HasOne("EveMailHelper.DataAccessLayer.Models.Character", "AttachedTo")
                        .WithMany("Chats")
                        .HasForeignKey("AttachedToId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("EveMailHelper.DataAccessLayer.Models.Character", "Listener")
                        .WithMany()
                        .HasForeignKey("ListenerId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("AttachedTo");

                    b.Navigation("Listener");
                });

            modelBuilder.Entity("EveMailHelper.DataAccessLayer.Models.ChatMessage", b =>
                {
                    b.HasOne("EveMailHelper.DataAccessLayer.Models.Character", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("EveMailHelper.DataAccessLayer.Models.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Chat");
                });

            modelBuilder.Entity("EveMailHelper.DataAccessLayer.Models.EveMail", b =>
                {
                    b.HasOne("EveMailHelper.DataAccessLayer.Models.EveMailTemplate", "EveMailTemplate")
                        .WithMany("EveMailsGenerated")
                        .HasForeignKey("EveMailTemplateId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("EveMailTemplate");
                });

            modelBuilder.Entity("EveMailHelper.DataAccessLayer.Models.EveMailSentTo", b =>
                {
                    b.HasOne("EveMailHelper.DataAccessLayer.Models.Character", "Character")
                        .WithMany("EveMailReceived")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EveMailHelper.DataAccessLayer.Models.EveMail", "EveMail")
                        .WithMany("SentTo")
                        .HasForeignKey("EveMailId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("EveMail");
                });

            modelBuilder.Entity("EveMailHelper.DataAccessLayer.Models.Note", b =>
                {
                    b.HasOne("EveMailHelper.DataAccessLayer.Models.Character", "AttachedTo")
                        .WithMany("Notes")
                        .HasForeignKey("AttachedToId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("AttachedTo");
                });

            modelBuilder.Entity("EveMailHelper.DataAccessLayer.Models.Character", b =>
                {
                    b.Navigation("Chats");

                    b.Navigation("EveMailReceived");

                    b.Navigation("Notes");
                });

            modelBuilder.Entity("EveMailHelper.DataAccessLayer.Models.Chat", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("EveMailHelper.DataAccessLayer.Models.EveMail", b =>
                {
                    b.Navigation("SentTo");
                });

            modelBuilder.Entity("EveMailHelper.DataAccessLayer.Models.EveMailTemplate", b =>
                {
                    b.Navigation("EveMailsGenerated");
                });
#pragma warning restore 612, 618
        }
    }
}
