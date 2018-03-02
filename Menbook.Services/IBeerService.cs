namespace Menbook.Services
{
    using Menbook.Data.Models.Beers;
    using Models.Beers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBeerService
    {
        Task<IEnumerable<BeerListingServiceModel>> AllAsync(int page = 1, int pageSize = 9);

        IEnumerable<BeerNameListingServiceModel> GetAllNamesAsync();

        Task AddBeerAsync(string name, string country, BeerType type, double alcoholByVolume, string imageUrl, string userId);

        Task<int> TotalAsync();

        Task<bool> DeleteByIdAsync(int id);

        Task<BeerDetailsServiceModel> GetBeerDetailsByIdAsync(int id);

        Task<bool> EditBeerAsync(int id, string name, string country, BeerType type, double alcoholByVolume, string imageUrl);

        Task<bool> RateBeerAsync(string userId, int beerId, int rating);

        Task<IEnumerable<BeerListingServiceModel>> AllBySearchAsync(int page = 1, int pageSize = 9, string search = "");

        Task<int> TotalBySearchAsync(string search);

        Task<double> GetAbvBIdAsync(int beerId);
    }
}
