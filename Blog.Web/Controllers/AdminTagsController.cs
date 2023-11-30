/* 14. Create new controller */
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        /* 15. Create Add action method (GET) */
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
