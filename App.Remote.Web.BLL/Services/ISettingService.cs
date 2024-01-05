using Jabil.Pico.Common.ViewModels;
using Jabil.Pico.Web.Models;
using System.Threading.Tasks;

namespace Jabil.Pico.Web.BLL.Services
{
    public interface ISettingService
    {
        Task<SettingVM[]> GetAllAsync();
        Task<SettingVM[]> GetAllNowAsync();

        Task<SettingVM> GetByNameAsync(string name);
        Task<bool> UpdateAsync(SettingVM request);
    }
}
