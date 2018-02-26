namespace Menbook.Services.Models
{
    using Common.Mapping;
    using Data.Models.Cars;

    public class CarMakeListingServiceModel : IMapFrom<Make>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public double? Rating { get; set; }
    }
}
