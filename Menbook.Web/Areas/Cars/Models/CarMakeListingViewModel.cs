namespace Menbook.Web.Areas.Cars.Models
{
    using System.Collections.Generic;
    using Services.Models;

    public class CarMakeListingViewModel
    {
        public IEnumerable<CarMakeListingServiceModel> Cars { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? this.CurrentPage : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage >= this.TotalPages ? this.CurrentPage : this.CurrentPage + 1;
    }
}
