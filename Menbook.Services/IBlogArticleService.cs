namespace Menbook.Services
{
    using Models.Blog;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBlogArticleService
    {
        Task<IEnumerable<ArticleListingServiceModel>> AllAsync(int page = 1, int pageSize = 10);

        Task<int> TotalAsync();

        Task<ArticleDetailsServiceModel> ByIdAsync(int id);

        Task CreateAsync(string title, string content, string userId);

        Task<bool> DeleteByIdAsync(int articleId);
    }
}
