/* 217. Create Like repo interface */
namespace Blog.Web.Repositories
{
    public interface IBlogPostLikeRepository
    {
        /* 218. Create abstract method returning total likes */
        Task<int> GetTotalLikesAsync(Guid blogPostId);
    }
}
