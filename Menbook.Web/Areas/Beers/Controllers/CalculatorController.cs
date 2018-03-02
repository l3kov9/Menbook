namespace Menbook.Web.Areas.Beers.Controllers
{
    using Menbook.Data.Models;
    using Menbook.Web.Areas.Beers.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CalculatorController : BaseBeerController
    {
        private readonly IBeerService beers;
        private readonly IUserService users;
        private readonly UserManager<User> userManager;

        public CalculatorController(IBeerService beers, IUserService users, UserManager<User> userManager)
        {
            this.beers = beers;
            this.users = users;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var form = new CalculateFormViewModel
            {
                BeerNames = this.GetSupplierListItems(),
                Age = await this.users.CurrentUserAgeByIdAsync(this.userManager.GetUserId(User))
            };

            return View(form);
        }

        [Authorize]
        public async Task<IActionResult> Result(CalculateFormViewModel model)
        {
            var age = model.Age;
            var weight = model.Weight;
            var drinkingHours = model.HoursDrinking;
            var totalDrinks = model.TotalDrinks;
            var beerAbv = await this.beers.GetAbvBIdAsync(model.BeerId);

            var bacResult = BAC.Calculate(age, weight, drinkingHours, totalDrinks, beerAbv);

            var bacConclusion = BAC.Conclusion(bacResult/10.0);
            bacConclusion.Bac = bacResult;
            bacConclusion.ImageUrl = "https://fthmb.tqn.com/mm0XORjQpYtckiap7AQsuklUkJM=/768x0/filters:no_upscale()/homer_2008_v2F_hires2-56a00fd43df78cafda9fde98.jpg";

            return View(nameof(Result), bacConclusion);
        }

        private IEnumerable<SelectListItem> GetSupplierListItems()
            => this.beers
                    .GetAllNamesAsync()
                    .Select(s => new SelectListItem
                    {
                        Text = s.Name,
                        Value = s.Id.ToString()
                    })
                    .ToList();
    }
}
