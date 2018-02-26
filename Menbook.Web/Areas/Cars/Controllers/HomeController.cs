namespace Menbook.Web.Areas.Cars.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : CarsBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
