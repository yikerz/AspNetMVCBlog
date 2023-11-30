/* 36. Create view model */
namespace Blog.Web.Models.View
{
    public class EditTagRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
