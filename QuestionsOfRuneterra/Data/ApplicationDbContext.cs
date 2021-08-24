using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuestionsOfRuneterra.Data.Models;

namespace QuestionsOfRuneterra.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<FriendRequest> FriendRequests { get; set; }

        public DbSet<Friendship> Friendships { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<QuizGame> QuizGames { get; set; }

        public DbSet<QuizGameSession> QuizGameSessions { get; set; }

        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasMany(au => au.OwnedRooms)
                .WithOne(r => r.Owner)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(au => au.JoinedRooms)
                .WithMany(r => r.Members);

            builder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<QuizGameSession>()
                .HasOne(qgs => qgs.Question)
                .WithMany(q => q.Sessions)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Friendship>()
                .HasKey(fs => new { fs.FirstFriendId, fs.SecondFriendId });

            builder.Entity<FriendRequest>()
                .HasKey(fr => new { fr.SenderId, fr.ReceiverId });

            base.OnModelCreating(builder);
        }
    }
}
