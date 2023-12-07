using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models.View
{
    public class RegisterViewModel
    {
        /* 305. Input validation */
        [Required]
        public string Username { get; set; }
        [Required]
        /* 308. Validate email format */
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        /* 309. Validate password length and customize error message */
        [MinLength(8, ErrorMessage = "Password has to be at least 8 characters")]
        public string Password { get; set; }
    }
}
