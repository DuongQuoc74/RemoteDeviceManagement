using Jabil.Pico.Common.ViewModels;
using Jabil.Pico.Web.Models;
using System;
using System.Threading.Tasks;

namespace Jabil.Pico.Web.BLL.Services
{
    public interface IApiLogService
    {
        Task<ApiLogsVM[]> GetAllApiLogsAsync(DateTime? startDate, DateTime? endDate, string guidDevice);

        Task<bool> AddAsync(ApiLog apiLog);
    }
}
