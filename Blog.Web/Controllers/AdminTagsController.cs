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


            return RedirectToAction("List"); 
        }

        [HttpGet]
        public IActionResult List()
        { 
            var tags = blogDbContext.Tags.ToList();

            return View(tags);
        }
        /* 33. Create Edit action method (GET) */
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            /* 35. Get tag by id */
            var tag = blogDbContext.Tags.FirstOrDefault(t => t.Id == id);
            /* 37. Map domain model to view model, then pass it to view */
            if (tag != null)
            {
                var model = new EditTagRequest
                {
                    Id = id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };
                return View(model);
            }

            return View(null);
        }
        /* 40. Create Edit action method (POST) */
        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            /* 41. Get domain model by id */
            var tag = blogDbContext.Tags.Find(editTagRequest.Id);

            /* 42. Update domain model and save change */
            if (tag != null)
            {
                tag.Name = editTagRequest.Name;
                tag.DisplayName = editTagRequest.DisplayName;

                blogDbContext.SaveChanges();
                /* 43. Redirect to List action method */
                return RedirectToAction("List");
            }
            /* 43. Redirect to Edit action method */
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

    }
}
