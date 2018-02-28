namespace Menbook.Services.Models
{
    public class TopCarsDetailsListingServiceModel
    {
        public int ModelId { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string ImageUrl { get; set; }

        public double AverageRating { get; set; }

        public int TotalVotes { get; set; }
    }
}
