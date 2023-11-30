using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        /* 48. Implement asynchronous */
        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest) 
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };
            await blogDbContext.Tags.AddAsync(tag);
            await blogDbContext.SaveChangesAsync();


            return RedirectToAction("List"); 
        }

        /* 48. Implement asynchronous */
        [HttpGet]
        public async Task<IActionResult> List()
        { 
            var tags = await blogDbContext.Tags.ToListAsync();

            return View(tags);
        }
        /* 48. Implement asynchronous */
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await blogDbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
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
        /* 48. Implement asynchronous */
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = await blogDbContext.Tags.FindAsync(editTagRequest.Id);

            if (tag != null)
            {
                tag.Name = editTagRequest.Name;
                tag.DisplayName = editTagRequest.DisplayName;

                await blogDbContext.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
        /* 48. Implement asynchronous */
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest) 
        {
            var tag = await blogDbContext.Tags.FindAsync(editTagRequest.Id);
            if (tag != null)
            {
                blogDbContext.Tags.Remove(tag);
                await blogDbContext.SaveChangesAsync();
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

    }
}
