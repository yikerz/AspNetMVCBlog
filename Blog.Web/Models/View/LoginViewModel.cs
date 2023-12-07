using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models.View
{
    public class LoginViewModel
    {
        /* 312. Add annotation to input */
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password has to be at least 8 characters")]
        public string Password { get; set; }
        [Required]
        public string ReturnUrl { get; set; }
    }
}
