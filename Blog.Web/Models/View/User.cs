/* 287.1 Create view model for individual user */
namespace Blog.Web.Models.View
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
    }
}
