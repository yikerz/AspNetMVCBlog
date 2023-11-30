using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.View;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly BlogDbContext blogDbContext;
        public AdminTagsController(BlogDbContext blogDbContext)
        {
            this.blogDbContext = blogDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddTagRequest addTagRequest) 
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };
            blogDbContext.Tags.Add(tag);
            blogDbContext.SaveChanges();


            return View("Add"); 
        }

        /* 26. Create List action method (GET) */
        [HttpGet]
        public IActionResult List()
        { 
            /* 28. Get all tags and pass to view */
            var tags = blogDbContext.Tags.ToList();

            return View(tags);
        }
    }
}
