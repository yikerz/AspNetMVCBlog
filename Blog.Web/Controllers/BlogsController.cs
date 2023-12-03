using Blog.Web.Models.View;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class BlogsController : Controller
    {
        /* 223. Add blogPostLikeRepo to constructor */
        private readonly IBlogPostRepository blogPostRepo;
        private readonly IBlogPostLikeRepository blogPostLikeRepo;

        public BlogsController(IBlogPostRepository blogPostRepo, IBlogPostLikeRepository blogPostLikeRepo)
        {
            this.blogPostRepo = blogPostRepo;
            this.blogPostLikeRepo = blogPostLikeRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {

            var blogPost = await blogPostRepo.GetByUrlHandleAsync(urlHandle);
            var blogDetailsViewModel = new BlogDetailsViewModel();
            /* 224. Get total Likes */
            if (blogPost != null)
            {
                var likes = await blogPostLikeRepo.GetTotalLikesAsync(blogPost.Id);

                /* 226. Map from domain model to view model */
                blogDetailsViewModel = new BlogDetailsViewModel
                {
                    Id = blogPost.Id,
                    Content = blogPost.Content,
                    PageTitle = blogPost.PageTitle,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    Heading = blogPost.Heading,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    UrlHandle = blogPost.UrlHandle,
                    Visible = blogPost.Visible,
                    Tags = blogPost.Tags,
                };
                blogDetailsViewModel.TotalLikes = likes;
            }

            return View(blogDetailsViewModel);
        }
    }
}
