namespace Menbook.Services.Models.Beers
{
    using Data.Models.Beers;

    public class BeerListingServiceModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Country { get; set; }

        public BeerType Type { get; set; }

        public double AlcoholByVolume { get; set; }

        public string ImageUrl { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public double AverageRate { get; set; }
    }
}
