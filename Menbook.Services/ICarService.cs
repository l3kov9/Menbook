namespace Menbook.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICarService
    {
        Task<IEnumerable<CarMakeListingServiceModel>> AllAsync(int page = 1, int pageSize = 10);

        Task<int> Total();

        Task<IEnumerable<CarModelListingServiceModel>> ModelsByMake(int id);

        Task<string> NameById(int id);

        Task ChangeModelImgAsync(string make, string model, string imageUrl);

        Task DeleteImageAsync(string make, string model);

        Task<int> GetMakeIdByNameAsync(string make);
    }
}
