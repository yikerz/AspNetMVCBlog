using Blog.Web.Models.View;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        /* 20. Create Add action method (POST) */
        [HttpPost]
        public IActionResult Add(AddTagRequest addTagRequest) 
        { 
            return View("Add"); 
        }
    }
}
