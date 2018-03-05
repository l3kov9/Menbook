namespace Menbook.Web.Areas.Cars.Models
{
    using Data;
    using System.ComponentModel.DataAnnotations;

    public class CreateModelFormViewModel
    {
        public int MakeId { get; set; }

        [Required]
        [MinLength(DataConstrants.CarMakeNameMinLength)]
        [MaxLength(DataConstrants.CarMakeNameMaxLength)]
        public string Name { get; set; }

        [MinLength(DataConstrants.UrlMinLength)]
        [MaxLength(DataConstrants.UrlMaxLength)]
        public string ImageUrl { get; set; }
    }
}
