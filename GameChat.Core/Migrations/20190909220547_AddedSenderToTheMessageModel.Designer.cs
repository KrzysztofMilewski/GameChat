﻿// <auto-generated />
using System;
using GameChat.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GameChat.Core.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190909220547_AddedSenderToTheMessageModel")]
    partial class AddedSenderToTheMessageModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GameChat.Core.Models.Conversation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Participant1Id");

                    b.Property<int>("Participant2Id");

                    b.HasKey("Id");

                    b.HasIndex("Participant1Id");

                    b.HasIndex("Participant2Id");

                    b.ToTable("Conversation");
                });

            modelBuilder.Entity("GameChat.Core.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contents")
                        .IsRequired();

                    b.Property<int>("ConversationId");

                    b.Property<DateTime>("DateSent");

                    b.Property<int>("SenderId");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.HasIndex("SenderId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("GameChat.Core.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired();

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired();

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("GameChat.Core.Models.Conversation", b =>
                {
                    b.HasOne("GameChat.Core.Models.User", "Participant1")
                        .WithMany("Conversations")
                        .HasForeignKey("Participant1Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GameChat.Core.Models.User", "Participant2")
                        .WithMany()
                        .HasForeignKey("Participant2Id")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("GameChat.Core.Models.Message", b =>
                {
                    b.HasOne("GameChat.Core.Models.Conversation", "Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameChat.Core.Models.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
