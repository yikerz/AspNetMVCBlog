/* 141. Create controller */
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class BlogsController : Controller
    {
        /* 146. Create constructor taking blogPostRepo */
        private readonly IBlogPostRepository blogPostRepo;
        public BlogsController(IBlogPostRepository blogPostRepo)
        {
            this.blogPostRepo = blogPostRepo;
        }
        /* 142. Index takes urlHandle param */
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {

            /* 147. Get post by urlHandle */
            var blogPost = await blogPostRepo.GetByUrlHandleAsync(urlHandle);

            return View(blogPost);
        }
    }
}
