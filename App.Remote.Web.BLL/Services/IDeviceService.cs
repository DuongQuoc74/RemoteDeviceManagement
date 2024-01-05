using Jabil.Pico.Web.DAL.Models;
using Jabil.Pico.Common.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Jabil.Pico.Web.BLL.Services
{
    public interface IDeviceService
    {
        Task<DeviceVM[]> GetAllAsync();

        Task<DeviceVM> GetByIdAsync(int id);

        Task<DeviceVM> GetByGuidAsync(string guid);

        Task<int> AddAsync(Device device);
        Task<int> UpdateAsync(int id, string remark, string user);
        Task<int> DeleteAsync(int id, string user);

        Task<bool> UpdateStatusSingleAsync(int id, string status, string user, int machineId);
        Task<int> UpdateStatusMultiAsync(int[] ids, string status, string user);
        Task<MachineVM[]> GetUnassignedMachinesAsync();

        //Thêm mới
        Task<DeviceHistoryVM[]> GetListDeviceHistoriesByDeviceID(int deviceId);
       
       
    }
}
