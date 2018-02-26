namespace Menbook.Data.Models
{
    using Cars;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        [Required]
        [MaxLength(DataConstrants.UserNameMaxLength)]
        public string Name { get; set; }

        [MinLength(DataConstrants.UrlMinLength)]
        [MaxLength(DataConstrants.UrlMaxLength)]
        public string ImageUrl { get; set; }

        public DateTime Birthdate { get; set; }

        public List<UserCar> Cars { get; set; } = new List<UserCar>();

        public List<Rating> CarRatings { get; set; } = new List<Rating>();

        public List<Review> CarReviews { get; set; } = new List<Review>();
    }
}
