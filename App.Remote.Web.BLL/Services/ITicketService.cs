using Jabil.Pico.Common.ViewModels;
using Jabil.Pico.Web.Models;
using System;
using System.Threading.Tasks;

namespace Jabil.Pico.Web.BLL.Services
{
    public interface ITicketService
    {
        Task<TicketVM[]> GetAllAsync(DateTime? startDate, DateTime? endDate, string machineName);
        Task<TicketVM> FindOpenTicketAsync(string guid);
        Task<TicketVM[]> FindTicketsByMachineAsync(int machineId);
    }
}
