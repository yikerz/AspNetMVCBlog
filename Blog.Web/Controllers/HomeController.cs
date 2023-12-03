using Blog.Web.Models;
using Blog.Web.Models.View;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository blogPostRepo;
        private readonly ITagRepository tagRepo;

        public HomeController(ILogger<HomeController> logger, IBlogPostRepository blogPostRepo, ITagRepository tagRepo)
        {
            _logger = logger;
            this.blogPostRepo = blogPostRepo;
            this.tagRepo = tagRepo;
        }

        public async Task<IActionResult> Index()
        {
            var blogPosts = await blogPostRepo.GetAllAsync();
            var tags = await tagRepo.GetAllAsync();
            var homeViewModel = new HomeViewModel
            {
                BlogPosts = blogPosts,
                Tags = tags
            };
            return View(homeViewModel);
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
