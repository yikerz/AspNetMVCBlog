/* 231. Create controller for post like */
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
        /* 237. Create constructor taking IBlogPostLikeRepo */
        private readonly IBlogPostLikeRepository blogPostLikeRepo;
        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepo)
        {
            this.blogPostLikeRepo = blogPostLikeRepo;
        }

        /* 232. Create AddLike action method (POST) */
        /* 234. Use AddLikeRequest as param */
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
        {
            /* 238. Mapping from view model to domain model */
            var blogPostLike = new BlogPostLike
            {
                PostId = addLikeRequest.BlogPostId,
                UserId = addLikeRequest.UserId,
            };
            /* 239. Add Like For Blog to database */
            await blogPostLikeRepo.AddLikeForBlog(blogPostLike);
            /* 240. Return Ok */
            return Ok();
        }
    }
}
