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
        public DbSet<UserChat> UsersChats { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .HasDefaultValueSql("uuid_generate_v4()");

            modelBuilder.Entity<Post>()
                .Property(p => p.Id)
                .HasDefaultValueSql("uuid_generate_v4()");

            modelBuilder.Entity<Comment>()
                .Property(c => c.Id)
                .HasDefaultValueSql("uuid_generate_v4()");

            modelBuilder.Entity<Chat>()
                .Property(c => c.ChatId)
                .HasDefaultValueSql("uuid_generate_v4()");

            modelBuilder.Entity<UserChat>()
                .Property(uc => uc.Id)
                .HasDefaultValueSql("uuid_generate_v4()");
            
            modelBuilder.Entity<Message>()
                .Property(m => m.Id)
                .HasDefaultValueSql("uuid_generate_v4()");

            modelBuilder.Entity<Friendship>()
                .Property(f => f.FriendshipId)
                .HasDefaultValueSql("uuid_generate_v4()");

            modelBuilder.Entity<Chat>()
               .HasOne(chat => chat.LatestMessage)
               .WithOne()
               .HasForeignKey<Chat>(chat => chat.LatestMessageId);

            modelBuilder.Entity<Chat>()
                .HasMany(c => c.Users)
                .WithMany()
                .UsingEntity<UserChat>();

            modelBuilder.Entity<Message>()
                .HasOne(c => c.Chat)
                .WithMany();

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Creator)
                .WithMany();

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Creator)
                .WithMany();

            //Friendship Table

           modelBuilder.Entity<Friendship>()
                .HasKey(f => f.FriendshipId);

            modelBuilder.Entity<Friendship>()
                .Property(f => f.UserId);

            modelBuilder.Entity<Friendship>()
                .Property(f => f.FriendId);

            //Friends requests table

            modelBuilder.Entity<FriendRequest>()
                .HasKey(f => f.FriendRequestId);
            
            modelBuilder.Entity<FriendRequest>()
                .Property(f => f.SenderId);

            modelBuilder.Entity<FriendRequest>()
                .Property(f => f.ReceiverId);

        }
    }
}
