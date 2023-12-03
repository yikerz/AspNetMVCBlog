/* 219. Create Like Repo */
using Blog.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        /* 220. Create constructor taking blogDbContext */
        private readonly BlogDbContext blogDbContext;
        public BlogPostLikeRepository(BlogDbContext blogDbContext)
        {
            this.blogDbContext = blogDbContext;
        }
        public async Task<int> GetTotalLikesAsync(Guid blogPostId)
        {
            /* 221. Count likes by post Id */
            return await blogDbContext.BlogPostLikes.CountAsync(x => x.PostId == blogPostId); 
        }
    }
}
