using Microsoft.EntityFrameworkCore;
using WebSocialNet.Domain.Entities;

namespace WebSocialNet.Dal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Chat> Chats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .HasDefaultValueSql("uuid_generate_v4()");

            modelBuilder.Entity<Post>()
            .Property(u => u.Id)
            .HasDefaultValueSql("uuid_generate_v4()");

            modelBuilder.Entity<Comment>()
            .Property(u => u.Id)
            .HasDefaultValueSql("uuid_generate_v4()");

            modelBuilder.Entity<Chat>()
                .Property(u => u.ChatId)
                .HasDefaultValueSql("uuid_generate_v4()");

            modelBuilder.Entity<Message>()
                .Property(m => m.Id)
                .HasDefaultValueSql("uuid_generate_v4()"); // Add a unique identifier property for Message

            modelBuilder.Entity<Chat>()
               .HasOne(chat => chat.LatestMessage)
               .WithOne()
               .HasForeignKey<Chat>(chat => chat.LatestMessageId);

            modelBuilder.Entity<Comment>()
                .Property(f => f.UserId);

            modelBuilder.Entity<Friendship>()
                .HasKey(f => f.FriendshipId);

            // Configure the foreign keys
            modelBuilder.Entity<Friendship>()
                .Property(f => f.UserId);

            modelBuilder.Entity<Friendship>()
                .Property(f => f.FriendId);

        }
    }
}
