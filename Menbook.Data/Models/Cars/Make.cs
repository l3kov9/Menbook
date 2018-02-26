namespace Menbook.Data.Models.Cars
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class Make
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstrants.CarMakeNameMinLength)]
        [MaxLength(DataConstrants.CarMakeNameMaxLength)]
        public string Name { get; set; }

        [MinLength(DataConstrants.UrlMinLength)]
        [MaxLength(DataConstrants.UrlMaxLength)]
        public string ImageUrl { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        public double? Rating { get; set; }

        public List<Model> Models { get; set; } = new List<Model>();
    }
}
