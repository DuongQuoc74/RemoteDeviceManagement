using Jabil.Pico.Common.ViewModels;
using Jabil.Pico.Web.DAL.Models;
using Jabil.Pico.Web.Models;
using Jabil.Pico.Web.Models.Context;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages;

namespace Jabil.Pico.Web.BLL.Services
{
    public class TicketService : ITicketService
    {
        readonly ApplicationContext _ctx;

        public TicketService(ApplicationContext ctx)
        {
            this._ctx = ctx;
        }
        public async Task<TicketVM[]> GetAllAsync(DateTime? startDate, DateTime? endDate, string machineName)
        {
            var sqlParams = new SqlParameter[]
               {
                    new SqlParameter("@StartDate", SqlDbType.DateTime)
                    {
                        Value = startDate == null ? DBNull.Value : (object)startDate
                    },
                    new SqlParameter("@EndDate", SqlDbType.DateTime)
                    {
                        Value = endDate == null ? DBNull.Value : (object)endDate
                    },
                    new SqlParameter("@MachineName", SqlDbType.NVarChar)
                    {
                        Value = !string.IsNullOrEmpty(machineName) ? (object)machineName : DBNull.Value
                    }
                };

            var result = await _ctx.Database.SqlQuery<TicketVM>("EXEC pi_GetTickets @StartDate, @EndDate, @MachineName", sqlParams).ToArrayAsync();
            return result;
        }

        public async Task<TicketVM> FindOpenTicketAsync(string guid)
        {
            var sqlParams = new SqlParameter[]
            {
                new SqlParameter("@MachineGuid", SqlDbType.UniqueIdentifier)
                {
                    Value = new Guid(guid)
                },
                new SqlParameter("@Guid", SqlDbType.UniqueIdentifier)
                {
                    Value =  DBNull.Value
                }
            };
            var ticket = await _ctx.Database.SqlQuery<TicketVM>("EXEC pi_GetDeviceOpenTicket @MachineGuid, @Guid", sqlParams).FirstOrDefaultAsync();
            return ticket;
        }

        public async Task<TicketVM[]> FindTicketsByMachineAsync(int machineId)
        {
            var tickets = await _ctx.Database.SqlQuery<TicketVM>("EXEC pi_GetTicketByMachine @MachineId", new SqlParameter("MachineId", machineId)).ToArrayAsync();
            return tickets;
        }
    }
}
