using Blog.Web.Models.View;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepo;
        private readonly IBlogPostLikeRepository blogPostLikeRepo;
        /* 249. Add SignInManager and UserManager in constructor */
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public BlogsController(IBlogPostRepository blogPostRepo, 
                               IBlogPostLikeRepository blogPostLikeRepo,
                               SignInManager<IdentityUser> signInManager,
                               UserManager<IdentityUser> userManager)
        {
            this.blogPostRepo = blogPostRepo;
            this.blogPostLikeRepo = blogPostLikeRepo;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {

            var blogPost = await blogPostRepo.GetByUrlHandleAsync(urlHandle);
            var blogDetailsViewModel = new BlogDetailsViewModel();
            /* 252. Initialize liked state by current user */
            var liked = false;

            if (blogPost != null)
            {
                var likes = await blogPostLikeRepo.GetTotalLikesAsync(blogPost.Id);

                if (signInManager.IsSignedIn(User))
                {
                    /* 253. Get all Likes if User is logged in */
                    var likesForBlog = await blogPostLikeRepo.GetLikesForBlog(blogPost.Id);
                    /* 254. Get userId using userManager */
                    var userId = userManager.GetUserId(User);
                    /* 255. Set liked state to true */
                    if (userId != null)
                    {
                        var likeFromUser = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        liked = likeFromUser != null;
                    }
                }

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
                    /* 256. Add Liked into view model */
                    Liked = liked,
                };
                blogDetailsViewModel.TotalLikes = likes;
            }

            return View(blogDetailsViewModel);
        }
    }
}
