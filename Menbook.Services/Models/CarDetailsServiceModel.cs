namespace Menbook.Services.Models
{
    using System.Collections.Generic;

    public class CarDetailsServiceModel
    {
        public int ModelId { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<CarReviewListingServiceModel> Reviews { get; set; }
    }
}
