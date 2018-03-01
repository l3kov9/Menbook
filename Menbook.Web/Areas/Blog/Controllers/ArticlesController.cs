namespace Menbook.Web.Areas.Blog.Controllers
{
    using Data.Models;
    using Helpers.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using System;
    using System.Threading.Tasks;

    public class ArticlesController : BlogBaseController
    {
        private const int PageSize = 3;

        private readonly UserManager<User> userManager;
        private readonly IUserService users;
        private readonly IBlogArticleService articles;
        private readonly IHtmlService html;

        public ArticlesController(UserManager<User> userManager, IUserService users, IBlogArticleService articles, IHtmlService html)
        {
            this.userManager = userManager;
            this.users = users;
            this.articles = articles;
            this.html = html;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
            =>View(new ArticleListingViewModel
            {
                Articles = await this.articles.AllAsync(page, PageSize),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)await this.articles.TotalAsync() / PageSize),
                CurrentUserName = await this.users.CurrentUserNameByIdAsync(this.userManager.GetUserId(User))
            });
        
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var article = await this.articles.ByIdAsync(id);

            return View(article);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PublishArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Content = this.html.Sanitize(model.Content);

            var userId = this.userManager.GetUserId(User);

            await this.articles.CreateAsync(model.Title, model.Content, userId);

            TempData.AddSuccessMessage($"Article {model.Title} was created by {this.userManager.GetUserName(User)}");

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteArticle(int ArticleId, int page)
        {
            bool success = await this.articles.DeleteByIdAsync(ArticleId);

            if (!success)
            {
                return BadRequest();
            }
            
            return RedirectToAction(nameof(Index), new { page });
        }
    }
}
