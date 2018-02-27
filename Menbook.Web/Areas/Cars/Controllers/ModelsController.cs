namespace Menbook.Web.Areas.Cars.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using System.Threading.Tasks;

    public class ModelsController : CarsBaseController
    {
        private readonly ICarService cars;

        public ModelsController(ICarService cars)
        {
            this.cars = cars;
        }

        [Route("/cars/models/{id}")]
        public async Task<IActionResult> Index(int id)
            => View(new CarModelListingViewModel
            {
                Models = await this.cars.ModelsByMake(id),
                MakeName = await this.cars.NameById(id)
            });

        public async Task<IActionResult> Reviews(int id)
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [Route("/cars/models/addimg")]
        public async Task<IActionResult> AddImg(AddImgToModelForm carModel)
        {
            await this.cars.ChangeModelImgAsync(carModel.Make, carModel.Model, carModel.ImageUrl);

            var id = await this.cars.GetMakeIdByNameAsync(carModel.Make);

            return RedirectToAction(nameof(Index), new { id });
        }

        [Authorize]
        [HttpPost]
        [Route("/cars/models/deleteimg")]
        public async Task<IActionResult> DeleteImg(AddImgToModelForm carModel)
        {
            await this.cars.DeleteImageAsync(carModel.Make, carModel.Model);

            var id = await this.cars.GetMakeIdByNameAsync(carModel.Make);

            return RedirectToAction(nameof(Index), new { id });
        }
    }
}
