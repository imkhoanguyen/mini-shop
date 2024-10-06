using API.Interfaces;
using API.Data;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Build.ObjectModelRemoting;
namespace API.Repositories
{ 

    
    public class BlogsRepository : IBlogRepository
    {
        private readonly StoreContext _context;


        public BlogsRepository(StoreContext context)
        {
            _context = context;
        }

        public void AddBlog(Blog blog)
        {
            _context.Blogs.Add(blog);   
        }
        public void DeleteBlog(Blog blog)
        {
            var BlogDb = _context.Blogs.FirstOrDefault(c => c.Id == blog.Id);

            _context.Blogs.Remove(blog);

        }

        //void UpdateBlog(Blog blog);
        //Task<Blog?> GetBlogsById(int id);
        //Task<IEnumerable<Blog>> GetBlogsByName(string name);
        //Task<string?> GetBlogsNameById(int id);
        //Task<IEnumerable<Blog>> GetAllBlogAsync();
        //Task<bool> BlogExistsAsync(string name);


        public void UpdateBlog(Blog blog)
        {
            var blogDb = _context.Blogs.FirstOrDefault(b => b.Id == blog.Id);
            if (blogDb != null)
            {
                blogDb.Title = blog.Title;
                blogDb.Content = blog.Content;
                blogDb.Category = blog.Category;
                blogDb.Update = blog.Update;
                blogDb.Create = blog.Create;
                blogDb.AppUser = blog.AppUser;
                blog.UserId = blogDb.UserId;
            }
        }

        public async Task<Blog?> GetBlogsById(int id)
        {
            return await _context.Blogs.FindAsync(id);
        }

        public async Task<IEnumerable<Blog>> GetAllBLogsAsync()
        {
            return await _context.Blogs.ToListAsync();
        }
        public async Task<bool> BlogExistsAsync(string name )
        {
            return await _context.Blogs.AnyAsync(c=>c.Title == name);
        }

        public async Task<PageList<Blog>> GetAllBLogsAsync(BlogsParam blogParams)
        {
            var query = _context.Blogs.OrderBy(c => c.Id).AsQueryable();
            if (!string.IsNullOrEmpty(blogParams.SearchString))
            {
                query = query.Where(c => c.Title.ToLower().Contains(blogParams.SearchString.ToLower())
                    || c.Id.ToString() == blogParams.SearchString);
            }

            var count = await query.CountAsync();

            var items = await query.Skip((blogParams.PageNumber - 1) * blogParams.PageSize)
                                   .Take(blogParams.PageSize)
                                   .ToListAsync();
            return new PageList<Blog>(items, count, blogParams.PageNumber, blogParams.PageSize);
        }

        public async Task<string?> GetBlogsNameById(int id)
        {
            var blogs = await _context.Blogs
                                .Where(c => c.Id == id)
                                .Select(c => c.Title)
                                .FirstOrDefaultAsync();
            return blogs;
        }

    }


    }
