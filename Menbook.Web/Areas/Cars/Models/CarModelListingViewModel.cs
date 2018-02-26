namespace Menbook.Web.Areas.Cars.Models
{
    using Services.Models;
    using System.Collections.Generic;

    public class CarModelListingViewModel
    {
        public IEnumerable<CarModelListingServiceModel> Models { get; set; }

        public string MakeName { get; set; }
    }
}
