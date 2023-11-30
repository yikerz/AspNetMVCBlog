/* 49. Create repository interface */
using Blog.Web.Models.Domain;

namespace Blog.Web.Views.Repositories
{
    public interface ITagRepository
    {
        /* 50. Create abstract CRUD */
        Task<Tag> AddAsync(Tag tag);
        Task<Tag?> GetAsync(Guid id);
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag?> DeleteAsync(Guid id);
    }
}
