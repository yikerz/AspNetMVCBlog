/* 110. Create interface */
namespace Blog.Web.Repositories
{
    public interface IImageRepository
    {
        /* 111. Create UploadAsync abstract method */
        Task<string> UploadAsync(IFormFile file);
    }
}
