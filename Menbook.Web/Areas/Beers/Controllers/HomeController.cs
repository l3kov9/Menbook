namespace Menbook.Web.Areas.Beers.Controllers
{
    using Data.Models;
    using Menbook.Web.Helpers.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using System;
    using System.Threading.Tasks;

    public class HomeController : BaseBeerController
    {
        private const int PageSize = 9;

        private readonly IBeerService beers;
        private readonly IUserService users;
        private readonly UserManager<User> userManager;

        public HomeController(IBeerService beers, IUserService users, UserManager<User> userManager)
        {
            this.beers = beers;
            this.users = users;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> All(int page = 1)
            => View(new BeerListingViewModel
            {
                Beers = await this.beers.AllAsync(page, PageSize),
                CurrentPage = page,
                CurrentUserId = this.userManager.GetUserId(User),
                CurrentUserRatedBeerIds = await this.users.AllRatedBeerIdsAsync(this.userManager.GetUserId(User)),
                TotalPages = (int)Math.Ceiling((double)await this.beers.TotalAsync()/PageSize),
            });

        public async Task<IActionResult> Search(string search, int page = 1)
        {
            if(search == null)
            {
                return RedirectToAction(nameof(All));
            }
            var totalBeersFound = await this.beers.TotalBySearchAsync(search);
            var beers = await this.beers.AllBySearchAsync(page, PageSize, search);
            var totalPages = (int)Math.Ceiling((double)totalBeersFound / PageSize);
            var userId = this.userManager.GetUserId(User);

            return View(new SearchBeerListingViewModel
            {
                Beers = beers,
                CurrentPage = page,
                TotalFound = totalBeersFound,
                Search = search,
                TotalPages = totalPages,
                CurrentUserId = userId,
                CurrentUserRatedBeerIds = await this.users.AllRatedBeerIdsAsync(userId)
            });
        }

        [Authorize]
        public IActionResult Create()
            => View();

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(AddBeerFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = this.userManager.GetUserId(User);

            await this.beers.AddBeerAsync(model.Name, model.Country, model.Type, model.AlcoholByVolume, model.ImageUrl, userId);

            TempData.AddSuccessMessage($"You successfully added {model.Name} to our fest!");

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id, int page)
        {
            var beer = await this.beers.GetBeerDetailsByIdAsync(id);

            if(beer == null)
            {
                return NotFound();
            }

            return View(new EditBeerFormViewModel
            {
                Id = beer.Id,
                Name = beer.Name,
                Country = beer.Country,
                Type = beer.Type,
                AlcoholByVolume = beer.AlcoholByVolume,
                ImageUrl = beer.ImageUrl,
                CurrentPage = page
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditBeerFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var success = await this.beers.EditBeerAsync(model.Id, model.Name, model.Country, model.Type, model.AlcoholByVolume, model.ImageUrl);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage($"You successfully edited {model.Name}");

            return RedirectToAction(nameof(All), new { model.CurrentPage });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteBeer(int id, int currentPage)
        {
            var success = await this.beers.DeleteByIdAsync(id);

            if (!success)
            {
                return BadRequest();
            }

            var totalPages = (int)Math.Ceiling((double)await this.beers.TotalAsync() / PageSize);
            currentPage = currentPage > totalPages ? totalPages : currentPage;

            TempData.AddSuccessMessage("You successfully deleted this beer!");

            return RedirectToAction(nameof(All), new { currentPage });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RateBeer(int id, int rating, int currentPage)
        {
            var userId = this.userManager.GetUserId(User);

            var success = await this.beers.RateBeerAsync(userId, id, rating);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage($"You have successfully rated with {rating} stars.");

            return RedirectToAction(nameof(All), new { currentPage });
        }
    }
}
