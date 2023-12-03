/* 212 Create new domain model for like */
namespace Blog.Web.Models.Domain
{
    public class BlogPostLike
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
    }
}
