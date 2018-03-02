namespace Menbook.Web.Areas.Beers.Models
{
    using Data.Models.Beers;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstrants;

    public class EditBeerFormViewModel
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

        [Range(0, 100)]
        [Display(Name = "Alcohol By Volume (ABV)")]
        public double AlcoholByVolume { get; set; }

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        public int CurrentPage { get; set; }
    }
}
