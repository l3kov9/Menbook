namespace Menbook.Data.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Model
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstrants.CarMakeNameMinLength)]
        [MaxLength(DataConstrants.CarMakeNameMaxLength)]
        public string Name { get; set; }

        [MinLength(DataConstrants.UrlMinLength)]
        [MaxLength(DataConstrants.UrlMaxLength)]
        public string ImageUrl { get; set; }

        public int MakeId { get; set; }

        public Make Make { get; set; }

        public List<UserCar> Users { get; set; } = new List<UserCar>();

        public List<Rating> UserRatings { get; set; } = new List<Rating>();

        public List<Review> UserReviews { get; set; } = new List<Review>();
    }
}
