namespace Menbook.Web.Areas.Cars.Controllers
{
    using Menbook.Data.Models;
    using Menbook.Web.Helpers.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using System.Threading.Tasks;

    public class ModelsController : CarsBaseController
    {
        private readonly ICarService cars;
        private readonly IUserService users;
        private readonly UserManager<User> userManager;

        public ModelsController(ICarService cars, IUserService users, UserManager<User> userManager)
        {
            this.cars = cars;
            this.users = users;
            this.userManager = userManager;
        }
        
        [Route("/cars/models/{id}")]
        public async Task<IActionResult> Index(int id)
            => View(new CarModelListingViewModel
            {
                Models = await this.cars.ModelsByMake(id),
                MakeName = await this.cars.NameById(id),
                UserCarFavouriteIds = await this.users.FavouriteCarIdsByUserIdAsync(this.userManager.GetUserId(User)),
                UserRateIds = await this.users.AllRatesIdsAsync(this.userManager.GetUserId(User)),
                AverageRating = await this.cars.AverageRateListingByIdAsync(id)
            });

        [Route("/cars/models/details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var car = await this.cars.DetailsByIdAsync(id);
            var reviewUserIds = await this.cars.UserReviewsIdsAsync(id);

            if(car == null)
            {
                return NotFound();
            }

            return View(new CarDetailsViewModel
            {
                Car = car,
                UserReviewIds = reviewUserIds,
                CurrentUserId = this.userManager.GetUserId(User)
            });
        }

        [Authorize]
        [HttpPost]
        [Route("/cars/models/addimg")]
        public async Task<IActionResult> AddImg(AddImgToModelForm carModel)
        {
            await this.cars.ChangeModelImgAsync(carModel.Make, carModel.Model, carModel.ImageUrl);

            var id = await this.cars.GetMakeIdByNameAsync(carModel.Make);

            TempData.AddSuccessMessage($"You successfully added image to {carModel.Make} {carModel.Model}!");

            return RedirectToAction(nameof(Index), new { id });
        }

        [Authorize]
        [HttpPost]
        [Route("/cars/models/deleteimg")]
        public async Task<IActionResult> DeleteImg(AddImgToModelForm carModel)
        {
            await this.cars.DeleteImageAsync(carModel.Make, carModel.Model);

            var id = await this.cars.GetMakeIdByNameAsync(carModel.Make);

            TempData.AddSuccessMessage($"You successfully deleted image from {carModel.Make} {carModel.Model}!");

            return RedirectToAction(nameof(Index), new { id });
        }

        [Authorize]
        [HttpPost]
        [Route("/cars/models/AddToMyCar")]
        public async Task<IActionResult> AddToMyCar(int Id, string Make)
        {
            var userId = this.userManager.GetUserId(User);

            var success = await this.cars.AddUserCarAsync(userId, Id);

            if (!success)
            {
                return BadRequest();
            }

            var id = await this.cars.GetMakeIdByNameAsync(Make);

            TempData.AddSuccessMessage($"You successfully added {Make} to your cars!");

            return RedirectToAction(nameof(Index),new { id });
        }

        [Authorize]
        [HttpPost]
        [Route("/cars/models/removemycar")]
        public async Task<IActionResult> RemoveMyCar(int ModelId)
        {
            var userId = this.userManager.GetUserId(User);

            var success = await this.cars.RemoveFromMyCarsAsync(userId, ModelId);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage($"You successfully removed this car!");

            return RedirectToAction("Profile", "Manage", new { area = "" });
        }

        [Authorize]
        [HttpPost]
        [Route("/cars/models/ratecar")]
        public async Task<IActionResult> RateCar(int ModelId, int Rating, string Make)
        {
            var userId = this.userManager.GetUserId(User);
            
            var success =await this.cars.RateCarAsync(userId, ModelId, Rating);

            if (!success)
            {
                return BadRequest();
            }

            var id = await this.cars.GetMakeIdByNameAsync(Make);

            TempData.AddSuccessMessage($"You successfully voted with {Rating} stars!");

            return RedirectToAction(nameof(Index), new { id });
        }

        [Authorize]
        [HttpPost]
        [Route("/cars/models/postreview")]
        public async Task<IActionResult> PostReview(string Text, int ModelId)
        {
            var review = Text;
            var id = ModelId;
            var userId = this.userManager.GetUserId(User);

            bool success = await this.cars.PostReviewAsync(userId, id, review);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage($"You successfully posted a review to this car!");

            return RedirectToAction(nameof(Details),new { id });
        }
    }
}
