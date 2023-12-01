﻿using Blog.Web.Models.Domain;
using Blog.Web.Models.View;
using Blog.Web.Views.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        /* 65. Create constructor */
        private readonly ITagRepository tagRepo;
        public AdminBlogPostsController(ITagRepository tagRepo)
        {
            this.tagRepo = tagRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            /* 66. Pass all tags into view */
            var tags = await tagRepo.GetAllAsync();
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text=x.Name, Value=x.Id.ToString() })
            };
            return View(model);
        }
        /* 68. Create Add action method (POST) */
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        { 
            return RedirectToAction("Add");
        }

    }
}
