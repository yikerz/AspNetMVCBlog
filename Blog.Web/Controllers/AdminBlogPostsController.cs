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
            return RedirectToAction("List");
        }
        [HttpGet]
        public async Task<IActionResult> List() 
        {
            var blogPosts = await blogPostRepo.GetAllAsync();
            return View(blogPosts);
        }
        /* 88. Create Edit action method (GET) */
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {  
            /* 91. Get blog post by Id */
            var blogPost = await blogPostRepo.GetAsync(id);

            /* 93. Map domain model to view model */
            if (blogPost != null)
            {
                var model = new EditBlogPostRequest
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    ShortDescription = blogPost.ShortDescription,
                    PublishedDate = blogPost.PublishedDate,
                    Visible = blogPost.Visible,
                };
                var tags = await tagRepo.GetAllAsync();
                model.Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
                model.SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray();

                return View(model);
            }
            return View(null);
        }

    }
}
