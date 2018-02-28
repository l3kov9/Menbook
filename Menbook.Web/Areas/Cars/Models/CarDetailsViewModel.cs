namespace Menbook.Web.Areas.Cars.Models
{
    using Services.Models;
    using System.Collections.Generic;

    public class CarDetailsViewModel
    {
        public CarDetailsServiceModel Car { get; set; }

        public IEnumerable<string> UserReviewIds { get; set; }

        public string CurrentUserId { get; set; }
    }
}
