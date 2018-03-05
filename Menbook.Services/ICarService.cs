namespace Menbook.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICarService
    {
        Task<IEnumerable<CarMakeListingServiceModel>> AllBySearchAsync(int page = 1, int pageSize = 10, string search = "");

        Task<int> Total();

        Task<int> TotalBySearchAsync(string search);

        Task<IEnumerable<CarModelListingServiceModel>> ModelsByMake(int id);

        Task<string> NameById(int id);

        Task ChangeModelImgAsync(string make, string model, string imageUrl);

        Task<IEnumerable<TopCarsDetailsListingServiceModel>> GetTopFiveCarsByRatingAsync();

        Task DeleteImageAsync(string make, string model);

        Task<int> GetMakeIdByNameAsync(string make);

        Task<bool> AddUserCarAsync(string userId, int modelId);

        Task<bool> RemoveFromMyCarsAsync(string userId, int modelId);

        Task<bool> RateCarAsync(string userId, int modelId, int rating);

        Task<IEnumerable<CarModelFavouritesListingServiceModel>> GetCarInfoByIds(IEnumerable<int> carIds);

        Task<IDictionary<int, double>> AverageRateListingByIdAsync(int id);

        Task<CarDetailsServiceModel> DetailsByIdAsync(int id);

        Task<bool> PostReviewAsync(string userId, int modelId, string review);

        Task<IEnumerable<string>> UserReviewsIdsAsync(int id);

        Task<bool> AddModelToMakeAsync(int makeId, string name, string imageUrl);
    }
}
