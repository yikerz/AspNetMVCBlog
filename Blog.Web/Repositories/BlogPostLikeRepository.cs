using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BlogDbContext blogDbContext;
        public BlogPostLikeRepository(BlogDbContext blogDbContext)
        {
            this.blogDbContext = blogDbContext;
        }
        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await blogDbContext.BlogPostLikes.AddAsync(blogPostLike);
            await blogDbContext.SaveChangesAsync();
            return blogPostLike;
        }
        /* 251. Implements GetLikesForBlog */
        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
            return await blogDbContext.BlogPostLikes.Where(x => x.PostId == blogPostId).ToListAsync();
        }

        public async Task<int> GetTotalLikesAsync(Guid blogPostId)
        {
            return await blogDbContext.BlogPostLikes.CountAsync(x => x.PostId == blogPostId); 
        }
    }
}
