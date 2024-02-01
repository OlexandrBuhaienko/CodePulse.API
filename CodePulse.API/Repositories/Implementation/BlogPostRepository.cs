using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;

namespace CodePulse.API.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            this._dbcontext = dbContext;
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await _dbcontext.BlogPosts.AddAsync(blogPost);
            await _dbcontext.SaveChangesAsync();
            return blogPost;
        }
    }
}
