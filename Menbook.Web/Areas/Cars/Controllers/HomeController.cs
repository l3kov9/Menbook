namespace Menbook.Web.Areas.Cars.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System.Threading.Tasks;

    public class HomeController : CarsBaseController
    {
        private readonly ICarService cars;

        public HomeController(ICarService cars)
        {
            this.cars = cars;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await this.cars.GetTopFiveCarsByRatingAsync();

            return View(cars);
        }
    }
}
