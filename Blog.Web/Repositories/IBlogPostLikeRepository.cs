using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikesAsync(Guid blogPostId);
        /* 235. Abstract method to add like to post */
        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);
    }
}
