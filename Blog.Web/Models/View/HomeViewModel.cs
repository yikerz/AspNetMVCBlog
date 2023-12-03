/* 152. Create View model to combine two models */
using Blog.Web.Models.Domain;

namespace Blog.Web.Models.View
{
    public class HomeViewModel
    {
        /* 153. Create IEnumerable props */
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
