using Blog.Web.Models.Domain;
using Blog.Web.Models.View;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
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

            await blogPostRepo.AddAsync(blogPost);
            /* 86. Redirect to List */
            return RedirectToAction("List");
        }
        /* 79. Create List action method (GET) */
        [HttpGet]
        public async Task<IActionResult> List() 
        {
            /* 82. Get all blog posts and pass to view */
            var blogPosts = await blogPostRepo.GetAllAsync();
            return View(blogPosts);
        }
    }
}
