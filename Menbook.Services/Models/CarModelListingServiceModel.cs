namespace Menbook.Services.Models
{
    using Common.Mapping;
    using Data.Models.Cars;

    public class CarModelListingServiceModel : IMapFrom<Model>
    {
        public string Name { get; set; }
        
        public string ImageUrl { get; set; }
    }
}
