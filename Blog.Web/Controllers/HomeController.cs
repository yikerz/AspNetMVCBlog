using Blog.Web.Models;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        /* 137. Add blogPostRepo to constructor  */
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository blogPostRepo;

        public HomeController(ILogger<HomeController> logger, IBlogPostRepository blogPostRepo)
        {
            _logger = logger;
            this.blogPostRepo = blogPostRepo;
        }

        public async Task<IActionResult> Index()
        {
            /* 138. Get all posts and pass to view */
            var blogPosts = await blogPostRepo.GetAllAsync();
            return View(blogPosts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
