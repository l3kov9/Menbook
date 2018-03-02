namespace Menbook.Services.Models.Beers
{
    using Common.Mapping;
    using Data.Models.Beers;

    public class BeerDetailsServiceModel : IMapFrom<Beer>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Country { get; set; }

        public BeerType Type { get; set; }
        
        public double AlcoholByVolume { get; set; }

        public string ImageUrl { get; set; }
    }
}
