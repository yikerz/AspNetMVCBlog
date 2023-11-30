using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.View;
using Blog.Web.Views.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Blog.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        /* 55. Modify constructor taking TagRepo */
        private readonly ITagRepository tagRepo;
        public AdminTagsController(ITagRepository tagRepo)
        {
            this.tagRepo = tagRepo;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest) 
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };
            /* 56. Switch all `BlogDbContext` methods to `TagRepo` methods */
            await tagRepo.AddAsync(tag);


            return RedirectToAction("List"); 
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            /* 56. Switch all `BlogDbContext` methods to `TagRepo` methods */
            var tags = await tagRepo.GetAllAsync();

            return View(tags);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            /* 56. Switch all `BlogDbContext` methods to `TagRepo` methods */
            var tag = await tagRepo.GetAsync(id);
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
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };
            /* 56. Switch all `BlogDbContext` methods to `TagRepo` methods */
            var updatedTag = await tagRepo.UpdateAsync(tag);

            if (updatedTag != null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest) 
        {
            /* 56. Switch all `BlogDbContext` methods to `TagRepo` methods */
            var deletedTag = await tagRepo.DeleteAsync(editTagRequest.Id);
            if (deletedTag != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

    }
}
