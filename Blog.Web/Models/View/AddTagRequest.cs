using Blog.Web.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models.View
{
    public class AddTagRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string DisplayName { get; set; }
    }
}
