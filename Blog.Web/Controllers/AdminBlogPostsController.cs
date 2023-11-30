/* 57. Create new controller */
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        /* 58. Create Add action method (GET) */
        [HttpGet]
        public async Task<IActionResult> Add()
        { 
            return View();
        }

    }
}
