using Jabil.Pico.Common.ViewModels;
using Jabil.Pico.Web.Models;
using System.Threading.Tasks;

namespace Jabil.Pico.Web.BLL.Services
{
    public interface IUserRoleService
    {
        Task<UserRoleVM[]> GetAllAsync();

        Task<UserRoleVM> GetByIdAsync(int userId);

        Task<UserRoleVM> GetByNameAsync(string userName);
        UserRoleVM GetByName(string userName);

        Task<bool> AddAsync(UserRoleVM user);

        Task UpdateAsync(UserRoleVM user);

        Task ActivateAsync(int userId);

        Task DeactivateAsync(int userId);

        Task PromoteAsync(int userId);

        Task DemoteAsync(int userId);

        Task DeleteAsync (int userId);
    }
}
