namespace Blog.Web.Models.View
{
    public class UserViewModel
    {
        public List<User> Users { get; set; }
        /* 297. Add props to receive input */
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool AdminRoleCheckbox { get; set; }
    }
}
