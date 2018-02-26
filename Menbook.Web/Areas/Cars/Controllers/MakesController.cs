namespace Menbook.Web.Areas.Cars.Controllers
{
    using Microsoft.AspNetCore.Authorization;
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

        public async Task<IActionResult> Models(int id)
            => View(new CarModelListingViewModel
            {
                Models = await this.cars.ModelsByMake(id),
                MakeName = await this.cars.NameById(id)
            });

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddImg(AddImgToModelForm carModel)
        {
            await this.cars.ChangeModelImgAsync(carModel.Make, carModel.Model, carModel.ImageUrl);

            var id = await this.cars.GetMakeIdByNameAsync(carModel.Make);

            return RedirectToAction(nameof(Models),new { id });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteImg(AddImgToModelForm carModel)
        {
            await this.cars.DeleteImageAsync(carModel.Make, carModel.Model);

            var id = await this.cars.GetMakeIdByNameAsync(carModel.Make);

            return RedirectToAction(nameof(Models), new { id });
        }
    }
}
