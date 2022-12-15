using Microsoft.EntityFrameworkCore;
using Project.Models;
using Slugify;

namespace Project.Services
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions options) : base(options) { }
        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>()
                  .HasKey(m => new { m.Text, m.PostId });

            modelBuilder.Entity<Post>()
           .HasMany(s => s.Comments)
           .WithOne(c => c.Post)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
           .HasMany(s => s.Tags)
           .WithOne(c => c.Post)
           .OnDelete(DeleteBehavior.Cascade);

            SlugHelper helper = new SlugHelper();

            var post = new Post
            {
                Id=1,
                slug = "augmented-reality-ios-application",
                Title = "Augmented Reality iOS Application",
                Body = "The app is simple to use, and will help you decide on your best furniture fit.",
                Description = "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app.",
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now,
            };

            var post2 = new Post
            {
                Id = 2,
                slug = "augmented-reality-ios-application-2",
                Title = "Augmented Reality iOS Application",
                Body = "The app is simple to use, and will help you decide on your best furniture fit.",
                Description = "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app.",
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now,
            };

            modelBuilder.Entity<Post>().HasData(
              post,
              post2
            );

            modelBuilder.Entity<Comment>().HasData(
                new
                {
                    Id = 1,
                    Body = "Great blog.",
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now,
                    PostId = 1
                }
            );

            modelBuilder.Entity<Tag>().HasData(
               new Tag
               {
                   PostId=1,
                   Text = "iOS",
               },
                new Tag
                {
                    PostId = 1,
                    Text = "AR",
                },
                new Tag
                {
                    PostId = 2,
                    Text = "iOS",
                },
                new Tag
                {
                    PostId = 2,
                    Text = "AR",
                },
                new Tag
                {
                    PostId = 2,
                    Text = "Gazzda",
                }
           );

        }

    }
}