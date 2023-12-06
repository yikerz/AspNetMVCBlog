using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);
        /* 274. Method to get all comments by blog Id */
        Task<IEnumerable<BlogPostComment>> GetCommentByBlogIdAsync(Guid blogPostId);
    }
}
