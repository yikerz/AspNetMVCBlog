/* 5. Create DbContext with inheritance */
using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Data
{
    public class BlogDbContext : DbContext
    {
        /* 6. Create constructor */
        public BlogDbContext(DbContextOptions options) : base(options)
        {
        }
        /* 7. Create DbSet props */
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
