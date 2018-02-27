namespace Menbook.Web.Models.ManageViewModels
{
    using System;
    using System.Collections.Generic;

    public class ProfileViewModel
    {
        public string Username { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public DateTime Birthdate { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public List<CarModelListingFavouriteViewModel> Cars { get; set; } = new List<CarModelListingFavouriteViewModel>();
    }
}
