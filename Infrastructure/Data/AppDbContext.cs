using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // One-to-One: Writer → UserInfo
            builder.Entity<Writer>()
                .HasOne(w => w.UserInfo)
                .WithOne()
                .HasForeignKey<Writer>(w => w.UserInfoId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many: Blog → Comment
            builder.Entity<Comment>()
                .HasOne(c => c.Blog)
                .WithMany(b => b.Comments)
                .HasForeignKey(c => c.BlogId)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ One-to-Many: Writer → Blog
            builder.Entity<Blog>()
                .HasOne(b => b.Writer)
                .WithMany(w => w.Blogs)
                .HasForeignKey(b => b.WriterId)
                .OnDelete(DeleteBehavior.Restrict); // ✔ izbegava ciklične cascade putanje

            // Many-to-Many: Writer ↔ Blog (via Favorite)
            builder.Entity<Favorite>()
                .HasKey(f => new { f.WriterId, f.BlogId });

            builder.Entity<Favorite>()
                .HasOne(f => f.Writer)
                .WithMany(w => w.Favorites)
                .HasForeignKey(f => f.WriterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Favorite>()
                .HasOne(f => f.Blog)
                .WithMany(b => b.Favorites)
                .HasForeignKey(f => f.BlogId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
