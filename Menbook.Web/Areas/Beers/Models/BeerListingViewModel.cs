namespace Menbook.Web.Areas.Beers.Models
{
    using Services.Models.Beers;
    using System.Collections.Generic;

    public class BeerListingViewModel
    {
        public IEnumerable<BeerListingServiceModel> Beers { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage >= this.TotalPages ? this.TotalPages : this.CurrentPage + 1;

        public string CurrentUserId { get; set; }

        public IDictionary<int, int> CurrentUserRatedBeerIds { get; set; }
    }
}
