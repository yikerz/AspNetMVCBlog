using Blog.Web.Models.Domain;
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
        private readonly IBlogPostCommentRepository blogPostCommentRepo;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public BlogsController(IBlogPostRepository blogPostRepo, 
                               IBlogPostLikeRepository blogPostLikeRepo,
                               IBlogPostCommentRepository blogPostCommentRepo,
                               SignInManager<IdentityUser> signInManager,
                               UserManager<IdentityUser> userManager)
        {
            this.blogPostRepo = blogPostRepo;
            this.blogPostLikeRepo = blogPostLikeRepo;
            this.blogPostCommentRepo = blogPostCommentRepo;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {

            var blogPost = await blogPostRepo.GetByUrlHandleAsync(urlHandle);
            var blogDetailsViewModel = new BlogDetailsViewModel();
            var liked = false;

            if (blogPost != null)
            {
                var likes = await blogPostLikeRepo.GetTotalLikesAsync(blogPost.Id);

                if (signInManager.IsSignedIn(User))
                {
                    var likesForBlog = await blogPostLikeRepo.GetLikesForBlog(blogPost.Id);
                    var userId = userManager.GetUserId(User);
                    if (userId != null)
                    {
                        var likeFromUser = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        liked = likeFromUser != null;
                    }
                }
                /* 275. Get all comments by post */
                var blogComments = await blogPostCommentRepo.GetCommentByBlogIdAsync(blogPost.Id);
                /* 277. Add comment to a list one-by-one */
                var blogCommentsForView = new List<BlogComment>();
                foreach (var blogComment in blogComments)
                {
                    blogCommentsForView.Add(new BlogComment
                    {
                        Description = blogComment.Description,
                        DateAdded = blogComment.DateAdded,
                        Username = (await userManager.FindByIdAsync(blogComment.UserId.ToString())).UserName
                    });
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
                    Liked = liked,
                    /* 278. Add comment list to view model */
                    Comments = blogCommentsForView,
                };
                blogDetailsViewModel.TotalLikes = likes;
            }

            return View(blogDetailsViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Index(BlogDetailsViewModel blogDetailsViewModel)
        {
            if (signInManager.IsSignedIn(User))
            {
                var blogPostComment = new BlogPostComment
                {
                    BlogPostId = blogDetailsViewModel.Id,
                    Description = blogDetailsViewModel.CommentDescription,
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    DateAdded = DateTime.Now,
                };
                await blogPostCommentRepo.AddAsync(blogPostComment);
                return RedirectToAction("Index", "Blogs", 
                    new { urlHandle = blogDetailsViewModel.UrlHandle });
            }
            return View();
        }
    }
}
