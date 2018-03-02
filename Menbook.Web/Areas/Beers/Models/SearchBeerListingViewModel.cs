namespace Menbook.Web.Areas.Beers.Models
{
    public class SearchBeerListingViewModel : BeerListingViewModel
    {
        public string Search { get; set; }

        public int TotalFound { get; set; }
    }
}
