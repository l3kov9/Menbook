namespace Menbook.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models.Blog;
    using Microsoft.EntityFrameworkCore;
    using Models.Blog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BlogArticleService : IBlogArticleService
    {
        private readonly MenbookDbContext db;

        public BlogArticleService(MenbookDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<ArticleListingServiceModel>> AllAsync(int page = 1, int pageSize = 10)
            => await this.db
                .Articles
                .OrderByDescending(a => a.Id)
                .Skip((page-1) * pageSize)
                .Take(pageSize)
                .Select(a=> new ArticleListingServiceModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Content = a.Content,
                    PublishDate = a.PublishDate,
                    Author = a.Author.UserName,
                    AuthorImage = a.Author.ImageUrl
                })
                .ToListAsync();

        public async Task<ArticleDetailsServiceModel> ByIdAsync(int id)
            => await this.db
                .Articles
                .Where(a => a.Id == id)
                .Select(a=>new ArticleDetailsServiceModel
                {
                    Title = a.Title,
                    Content = a.Content,
                    Author = a.Author.UserName,
                    AuthorImage = a.Author.ImageUrl,
                    PublishDate = a.PublishDate
                })
                .FirstOrDefaultAsync();

        public async Task CreateAsync(string title, string content, string userId)
        {
            var article = new Article
            {
                AuthorId = userId,
                Title = title,
                Content = content,
                PublishDate = DateTime.UtcNow
            };

            await this.db.AddAsync(article);

            await this.db.SaveChangesAsync();
        }

        public async Task<bool> DeleteByIdAsync(int articleId)
        {
            var article = this.db
                    .Articles
                    .Find(articleId);

            if(article == null)
            {
                return false;
            }

            this.db.Articles.Remove(article);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<int> TotalAsync()
            => await this.db
                .Articles
                .CountAsync();
    }
}
