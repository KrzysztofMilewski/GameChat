using GameChat.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GameChat.Core.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User configuration

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.PasswordSalt).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Username).IsRequired();

            #endregion

            #region Conversation configuration

            modelBuilder.Entity<Conversation>().ToTable("Conversations");
            modelBuilder.Entity<Conversation>().HasMany(c => c.Participants).WithOne(p => p.Conversation);

            modelBuilder.Entity<ConversationParticipant>().ToTable("ConversationParticipants");
            modelBuilder.Entity<ConversationParticipant>().HasOne(cp => cp.Participant).WithMany(p => p.Conversations);
            modelBuilder.Entity<ConversationParticipant>().HasKey(cp => new { cp.ConversationId, cp.ParticipantId });

            #endregion

            #region Message configuration

            modelBuilder.Entity<Message>().ToTable("Messages");
            modelBuilder.Entity<Message>().Property(m => m.Contents).IsRequired();
            modelBuilder.Entity<Message>().HasOne(m => m.Conversation).WithMany(c => c.Messages);

            #endregion

            #region Unread messages configuration

            modelBuilder.Entity<UnreadMessage>().ToTable("UnreadMessages");
            modelBuilder.Entity<UnreadMessage>().HasKey(um => new { um.MessageId, um.ParticipantId });
            modelBuilder.Entity<UnreadMessage>().HasOne(um => um.Message).WithMany(m => m.UsersThatHaventReadMessage);
            modelBuilder.Entity<UnreadMessage>().HasOne(um => um.Participant).WithMany(p => p.UnreadMessages).OnDelete(DeleteBehavior.Restrict);

            #endregion
        }
    }
}
