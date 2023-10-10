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

            modelBuilder.Entity<Friendship>()
                .HasKey(f => f.FriendshipId);

            modelBuilder.Entity<Friendship>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friendship>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(f => f.FriendId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
