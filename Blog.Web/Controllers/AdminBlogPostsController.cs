using Blog.Web.Models.Domain;
using Blog.Web.Models.View;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        /* 76. Add blogPostRepo param into constructor */
        private readonly ITagRepository tagRepo;
        private readonly IBlogPostRepository blogPostRepo;
        public AdminBlogPostsController(ITagRepository tagRepo, IBlogPostRepository blogPostRepo)
        {
            this.tagRepo = tagRepo;
            this.blogPostRepo = blogPostRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tagRepo.GetAllAsync();
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text=x.Name, Value=x.Id.ToString() })
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            /* 77. Map view model to domain model */
            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,
            };
            var selectedTags = new List<Tag>();
            foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                var existingTag = await tagRepo.GetAsync(Guid.Parse(selectedTagId));
                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }
            blogPost.Tags = selectedTags;

            /* 78. Add to database and save */
            await blogPostRepo.AddAsync(blogPost);
            return RedirectToAction("Add");
        }

    }
}
