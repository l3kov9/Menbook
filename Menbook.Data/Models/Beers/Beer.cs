namespace Menbook.Data.Models.Beers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstrants;

    public class Beer
    {
        public int Id { get; set; }

        [Required]
        [MinLength(BeerNameMinLength)]
        [MaxLength(BeerNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(BeerCountryMinLength)]
        [MaxLength(BeerCountryMaxLength)]
        public string Country { get; set; }

        public BeerType Type { get; set; }

        [Range(0,100)]
        public double AlcoholByVolume { get; set; }

        public string ImageUrl { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }

        public IEnumerable<BeerRating> Ratings { get; set; } = new List<BeerRating>();
    }
}
