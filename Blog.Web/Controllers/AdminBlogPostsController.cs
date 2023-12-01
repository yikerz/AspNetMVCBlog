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
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {  
            var blogPost = await blogPostRepo.GetAsync(id);

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
        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            var blogPost = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                ShortDescription = editBlogPostRequest.ShortDescription,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                UrlHandle = editBlogPostRequest.UrlHandle,
                PublishedDate = editBlogPostRequest.PublishedDate,
                Author = editBlogPostRequest.Author,
                Visible = editBlogPostRequest.Visible,
            };
            var selectedTags = new List<Tag>();
            foreach (var selectedTag in editBlogPostRequest.SelectedTags) 
            {
                if (Guid.TryParse(selectedTag, out var tagId)) 
                {
                    var foundTag = await tagRepo.GetAsync(tagId);
                    if (foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }
            blogPost.Tags = selectedTags;

            var updatedBlogPost = await blogPostRepo.UpdateAsync(blogPost);

            if (updatedBlogPost != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = editBlogPostRequest.Id });
        }
        /* 102. Create Delete action method (POST) */
        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest) 
        {
            /* 104. Complete the action method */
            var deletedPost = await blogPostRepo.DeleteAsync(editBlogPostRequest.Id);
            if (deletedPost != null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = editBlogPostRequest.Id });
        }
    }
}
