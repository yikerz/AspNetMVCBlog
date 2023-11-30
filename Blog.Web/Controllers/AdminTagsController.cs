using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.View;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        /* 24. Create constructor taking DbContext */
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
            /* 23. Map view model instance to domain model instance */
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };
            /* 25. Add tag into database and save changes */
            blogDbContext.Tags.Add(tag);
            blogDbContext.SaveChanges();


            return View("Add"); 
        }
    }
}
