namespace Menbook.Web.Areas.Cars.Models
{
    public class SearchCarMakeListingViewModel : CarMakeListingViewModel
    {
        public string Search { get; set; }

        public int TotalFound { get; set; }
    }
}
