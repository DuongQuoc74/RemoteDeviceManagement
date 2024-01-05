using Jabil.Pico.Common.ViewModels;
using Jabil.Pico.Web.DAL.Migrations;
using Jabil.Pico.Web.Models;
using Jabil.Pico.Web.Models.Context;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Jabil.Pico.Web.BLL.Services
{
    public class ApiLogService : IApiLogService
    {
        private readonly ApplicationContext _ctx;

        public ApiLogService(ApplicationContext ctx)
        {
            this._ctx = ctx;
        }

        public async Task<ApiLogsVM[]> GetAllApiLogsAsync(DateTime? startDate, DateTime? endDate, string guidDevice)
        {
            string pattern = @"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$";
            RegexOptions options = RegexOptions.None;

            if (guidDevice != null && !Regex.IsMatch(guidDevice, pattern, options, TimeSpan.FromSeconds(3)))
            {
                guidDevice = "00000000-0000-0000-0000-000000000000";
            }

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
                         new SqlParameter("@GuidDevice", SqlDbType.UniqueIdentifier)
                            {
                                Value = guidDevice == null ? DBNull.Value :(object) Guid.Parse(guidDevice)
                            }
                    };

            var result = await _ctx.Database.SqlQuery<ApiLogsVM>("EXEC pi_GetApiLogs @StartDate, @EndDate, @GuidDevice", sqlParams).ToArrayAsync();
            return result;
        }

        public async Task<bool> AddAsync(ApiLog apiLog)
        {
            var sqlParams1 = new SqlParameter[]
                {
                     new SqlParameter("@Api", SqlDbType.NVarChar)
                         {
                             Value = apiLog.Api 
                         },
                     new SqlParameter("@Request", SqlDbType.NVarChar)
                         {
                             Value = apiLog.Request 
                         },
                     new SqlParameter("@Response", SqlDbType.NVarChar)
                         {
                             Value = apiLog.Response
                         },
                     new SqlParameter("@MachineId", SqlDbType.Int)
                         {
                             Value = apiLog.MachineId
                         },
                     new SqlParameter("@Created", SqlDbType.DateTime)
                         {
                             Value = apiLog.Created
                         },
                     new SqlParameter("@CreatedBy", SqlDbType.NVarChar)
                         {
                             Value = apiLog.CreatedBy
                         },
                     new SqlParameter("@DeviceGuid", SqlDbType.UniqueIdentifier)
                         {
                            Value = apiLog.DeviceGuid
                         }
                };
            var result = await _ctx.Database.ExecuteSqlCommandAsync("EXEC pi_AddApiLogs @Api, @Request, @Response, @MachineId, @Created, @CreatedBy, @DeviceGuid", sqlParams1);
            if (result != 1)
                return false;

            return true;
        }
    }
}
