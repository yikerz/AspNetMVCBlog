using Blog.Web.Models.Domain;
using Blog.Web.Models.View;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository blogPostLikeRepo;
        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepo)
        {
            this.blogPostLikeRepo = blogPostLikeRepo;
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
        {
            var blogPostLike = new BlogPostLike
            {
                PostId = addLikeRequest.BlogPostId,
                UserId = addLikeRequest.UserId,
            };
            await blogPostLikeRepo.AddLikeForBlog(blogPostLike);

            return Ok();
        }
        /* 245. Create GetTotalLikesForBlog action method (GET) */
        [HttpGet]
        [Route("{blogPostId:Guid}/total_likes")]
        public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogPostId)
        {
            var totalLikes = await blogPostLikeRepo.GetTotalLikesAsync(blogPostId);
            return Ok(totalLikes);
        }
    }
}
