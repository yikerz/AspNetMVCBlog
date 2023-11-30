/* 51. Create repository implementing interface */
using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.View;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Views.Repositories
{
    public class TagRepository : ITagRepository
    {
        /* 52. Create constructor taking DbContext */
        private readonly BlogDbContext blogDbContext;
        public TagRepository(BlogDbContext blogDbContext)
        {
            this.blogDbContext = blogDbContext;
        }
        /* 53. Migrate code from controller */
        public async Task<Tag> AddAsync(Tag tag)
        {
            await blogDbContext.Tags.AddAsync(tag);
            await blogDbContext.SaveChangesAsync();
            return tag;
        }
        /* 53. Migrate code from controller */
        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var tag = await blogDbContext.Tags.FindAsync(id);
            if (tag != null) 
            {
                blogDbContext.Tags.Remove(tag);
                await blogDbContext.SaveChangesAsync();
                return tag;
            }
            return null;
        }
        /* 53. Migrate code from controller */
        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var tags = await blogDbContext.Tags.ToListAsync();
            return tags;
        }
        /* 53. Migrate code from controller */
        public async Task<Tag?> GetAsync(Guid id)
        {
            var tag = await blogDbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
            if (tag != null) 
            { 
                return tag;
            }
            return null;
        }
        /* 53. Migrate code from controller */
        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await blogDbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null) 
            { 
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await blogDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
    }
}
