/* 1. Create domain model */
namespace Blog.Web.Models.Domain
{
    public class Tag
    {
        /* 2. Create props */
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        /* 3. Many-to-Many relation */
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
