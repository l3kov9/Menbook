namespace Menbook.Web.Models.AccountViewModels
{
    using Menbook.Data;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Required]
        [MinLength(6)]
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
        
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
