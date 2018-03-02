namespace Menbook.Web.Areas.Cars.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using System;
    using System.Threading.Tasks;

    public class MakesController : CarsBaseController
    {
        private const int PageSize = 6;

        private readonly ICarService cars;

        public MakesController(ICarService cars)
        {
            this.cars = cars;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            string search = string.Empty;
            var cars = await this.cars.AllBySearchAsync(page, PageSize, search);
            var totalPages = (int)Math.Ceiling((double)await this.cars.Total() / PageSize);

            return View(new CarMakeListingViewModel
            {
                Cars = cars,
                CurrentPage = page,
                TotalPages = totalPages
            });
        }

        public async Task<IActionResult> Search(string Search, int page = 1)
        {
            if(Search == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var cars = await this.cars.AllBySearchAsync(page, PageSize, Search);
            var totalPages = (int)Math.Ceiling((double) await this.cars.TotalBySearchAsync(Search) / PageSize);

            return View(new SearchCarMakeListingViewModel
            {
                Cars = cars,
                CurrentPage = page,
                TotalPages = totalPages,
                Search = Search,
                TotalFound = await this.cars.TotalBySearchAsync(Search)
            });
        }
    }
}
