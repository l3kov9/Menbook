namespace Menbook.Web.Models.ManageViewModels
{
    using Data;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class IndexViewModel
    {
        public string Username { get; set; }

        [MaxLength(DataConstrants.UserNameMaxLength)]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [MinLength(DataConstrants.UrlMinLength)]
        [MaxLength(DataConstrants.UrlMaxLength)]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
