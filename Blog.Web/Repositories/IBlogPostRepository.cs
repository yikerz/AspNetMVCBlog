/* 70. Create interface */
using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories
{
    public interface IBlogPostRepository
    {
        /* 71. Create abtract CRUD */
        Task<BlogPost> AddAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetAsync(Guid id);
        Task<BlogPost?> UpdateAsync(BlogPost blogPost);
        Task<BlogPost?> DeleteAsync(Guid id);

    }
}
