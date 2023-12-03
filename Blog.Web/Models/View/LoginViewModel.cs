namespace Blog.Web.Models.View
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        /* 208. Create ReturnUrl prop */
        public string ReturnUrl { get; set; }
    }
}
