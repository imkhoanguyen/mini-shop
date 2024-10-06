using API.Entities;
using API.Helpers;



namespace API.Interfaces
{
    public interface IBlogRepository 
    {
        void AddBlog(Blog blog);
        void DeleteBlog(Blog blog);
        void UpdateBlog(Blog blog);
        Task<Blog?> GetBlogsById(int id);
        Task<string?> GetBlogsNameById(int id);
        Task<IEnumerable<Blog>> GetAllBLogsAsync();
        Task<bool> BlogExistsAsync(string name);
        Task<PageList<Blog>> GetAllBLogsAsync(BlogsParam blogParams);
    }
}
