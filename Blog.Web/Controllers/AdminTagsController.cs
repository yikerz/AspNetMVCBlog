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
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var tag = blogDbContext.Tags.FirstOrDefault(t => t.Id == id);
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
        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var tag = blogDbContext.Tags.Find(editTagRequest.Id);

            if (tag != null)
            {
                tag.Name = editTagRequest.Name;
                tag.DisplayName = editTagRequest.DisplayName;

                blogDbContext.SaveChanges();
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
        /* 45. Create Delete action method (POST) */
        [HttpPost]
        public IActionResult Delete(EditTagRequest editTagRequest) 
        {
            /* 46. Get tag by id */
            var tag = blogDbContext.Tags.Find(editTagRequest.Id);
            /* 47. Remove tag from database */
            if (tag != null)
            {
                blogDbContext.Tags.Remove(tag);
                blogDbContext.SaveChanges();
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

    }
}
