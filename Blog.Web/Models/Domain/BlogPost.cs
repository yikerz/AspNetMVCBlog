/* 1. Create domain model */
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Blog.Web.Models.Domain
{
    public class BlogPost
    {
        /* 2. Create props */
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }
        /* 3. Many-to-Many relation */
        public ICollection<Tag> Tags { get; set; }
    }
}
