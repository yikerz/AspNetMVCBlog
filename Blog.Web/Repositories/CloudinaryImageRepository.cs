/* 112. Create repo class implemented from repo interface */
namespace Blog.Web.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {
        public Task<string> UploadAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
