using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories
{
    public interface ITagRepository
    {
        Task<Tag> AddAsync(Tag tag);
        Task<Tag?> GetAsync(Guid id);
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag?> DeleteAsync(Guid id);
    }
}
