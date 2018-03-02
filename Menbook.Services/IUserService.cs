namespace Menbook.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<IEnumerable<int>> FavouriteCarIdsByUserIdAsync(string id);

        Task<IDictionary<int, int>> AllRatesIdsAsync(string id);

        Task<IDictionary<int, int>> AllRatedBeerIdsAsync(string id);

        Task<string> CurrentUserNameByIdAsync(string id);

        Task<int> CurrentUserAgeByIdAsync(string id);
    }
}
