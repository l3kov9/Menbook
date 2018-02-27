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
            var cars = await this.cars.AllAsync(page, PageSize);
            var totalPages = (int)Math.Ceiling((double)await this.cars.Total() / PageSize);

            return View(new CarMakeListingViewModel
            {
                Cars = cars,
                CurrentPage = page,
                TotalPages = totalPages
            });
        }
    }
}
