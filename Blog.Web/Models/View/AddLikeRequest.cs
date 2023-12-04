/* 233. Add view model for post like request */
namespace Blog.Web.Models.View
{
    public class AddLikeRequest
    {
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
    }
}
