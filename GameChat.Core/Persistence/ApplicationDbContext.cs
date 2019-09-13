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
            modelBuilder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.PasswordSalt).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Username).IsRequired();
            #endregion

            #region Conversation configuration
            modelBuilder.Entity<Conversation>().
                HasOne(c => c.Participant1).
                WithMany(u => u.Conversations).
                HasForeignKey(c => c.Participant1Id).
                OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Conversation>().
                HasOne(c => c.Participant2).
                WithMany().
                HasForeignKey(c => c.Participant2Id).
                OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Message configuration
            modelBuilder.Entity<Message>().Property(m => m.Contents).IsRequired();
            modelBuilder.Entity<Message>().HasOne(m => m.Conversation).WithMany(c => c.Messages);
            #endregion
        }
    }
}
