using Jabil.Pico.Common;
using Jabil.Pico.Common.ViewModels;
using Jabil.Pico.Web.DAL.Models;
using Jabil.Pico.Web.Models;
using Jabil.Pico.Web.Models.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Jabil.Pico.Web.BLL.Services
{
    public class DeviceService : IDeviceService
    {
        readonly ApplicationContext _ctx;

        public DeviceService(ApplicationContext ctx)
        {
            this._ctx = ctx;
        }

        public async Task<DeviceVM[]> GetAllAsync()
        {
            var result = await _ctx.Database.SqlQuery<DeviceVM>("EXEC pi_GetDevices").ToArrayAsync();

            foreach (var device in result)
            {
                var sqlParams = new SqlParameter[]
                    {
                        new SqlParameter("@MachineGuid", SqlDbType.UniqueIdentifier)
                            {
                                Value = DBNull.Value
                            },
                        new SqlParameter("@Guid", SqlDbType.UniqueIdentifier)
                            {
                                Value = device.Guid
                            }
                    };
                var ticket = await _ctx.Database.SqlQuery<TicketVM>("EXEC pi_GetDeviceOpenTicket @MachineGuid, @Guid", sqlParams).FirstOrDefaultAsync();
                if (ticket != null)
                    device.TicketDateStop = ticket.DateStop;
                else
                    device.TicketDateStop = null;
            }
            return result;
        }

        public async Task<DeviceVM> GetByIdAsync(int id)
        {
            var result = await _ctx.Database.SqlQuery<DeviceVM>("EXEC pi_GetDeviceDetail @DeviceId", new SqlParameter("DeviceId", id)).FirstOrDefaultAsync();
            if (result != null)
            {
                var sqlParams = new SqlParameter[]
                  {
                        new SqlParameter("@MachineGuid", SqlDbType.UniqueIdentifier)
                            {
                                Value = DBNull.Value
                            },
                        new SqlParameter("@Guid", SqlDbType.UniqueIdentifier)
                            {
                                Value = result.Guid
                            }
                  };
                var ticket = await _ctx.Database.SqlQuery<TicketVM>("EXEC pi_GetDeviceOpenTicket @MachineGuid, @Guid", sqlParams).FirstOrDefaultAsync();
                if (ticket != null)
                    result.TicketDateStop = ticket.DateStop;
                else
                    result.TicketDateStop = null;
            }

            return result;
        }

        public async Task<DeviceVM> GetByGuidAsync(string guid)
        {
            var result = await _ctx.Database.SqlQuery<DeviceVM>("EXEC pi_GetDeviceByGuid @Guid", new SqlParameter("Guid", new Guid(guid))).FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> AddAsync(Device device)
        {
            var remarkHistory = "Add new a device.";
            SqlParameter outputParam = new SqlParameter("@InsertedId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            var sqlParams = new SqlParameter[]
            {
                 new SqlParameter("@MachineId", SqlDbType.Int)
                 {
                     Value = device.MachineId,
                     Direction = ParameterDirection.Input
                 },
                 new SqlParameter("@Remark", SqlDbType.NVarChar)
                 {
                     Value = device.Remark,
                     Direction = ParameterDirection.Input
                 },
                 new SqlParameter("@CreatedBy", SqlDbType.NVarChar)
                 {
                     Value = device.CreatedBy,
                     Direction = ParameterDirection.Input
                 },
                 new SqlParameter("@Created", SqlDbType.DateTime)
                 {
                     Value = device.Created,
                     Direction = ParameterDirection.Input
                 },
                 new SqlParameter("@Available", SqlDbType.Bit)
                 {
                     Value = device.Available,
                     Direction = ParameterDirection.Input
                 },
                 new SqlParameter("@Guid", SqlDbType.UniqueIdentifier)
                 {
                     Value = device.Guid,
                     Direction = ParameterDirection.Input
                 },
                new SqlParameter("@Status", SqlDbType.NVarChar)
                {
                    Value = device.Status,
                    Direction = ParameterDirection.Input
                },
                new SqlParameter("@RemarkHistory", SqlDbType.NVarChar)
                {
                    Value = remarkHistory,
                    Direction = ParameterDirection.Input
                },
             outputParam
            };
            await _ctx.Database.ExecuteSqlCommandAsync("EXEC pi_AddDevice @MachineId, @Remark, @CreatedBy, @Created, @Available, @Guid, @Status, @RemarkHistory, @InsertedId OUTPUT", sqlParams);
            var deviceId = (int)outputParam.Value;
            return deviceId;
        }

        public async Task<bool> UpdateStatusSingleAsync(int id, string status, string user, int machineId)
        {
            var remark = "Edit in detail page.";
            var sqlParams = new SqlParameter[]
            {
               new SqlParameter("@Id", SqlDbType.Int)
                    {
                        Value = id
                    },
               new SqlParameter("@Status", SqlDbType.NVarChar)
                    {
                        Value = status
                    },
               new SqlParameter("@UpdatedBy", SqlDbType.NVarChar)
                    {
                        Value = user
                    },
               new SqlParameter("@Updated", SqlDbType.DateTime)
                    {
                        Value = DateTime.Now
                    },
                new SqlParameter("@Remark", SqlDbType.NVarChar)
                    {
                        Value = remark
                    },
                new SqlParameter("@MachineId", SqlDbType.Int)
                    {
                        Value = machineId
                    },
            };
            var result = await _ctx.Database.ExecuteSqlCommandAsync("EXEC pi_UpdateDevicesStatusSingle @Id, @Status, @UpdatedBy, @Updated, @Remark, @MachineId", sqlParams);
            if(result!=2)
                return false;
            return true;
        }

        public async Task<int> UpdateStatusMultiAsync(int[] ids, string status, string user)
        {
            if (!Utils.IsValidDeviceStatus(status)) return 0;
            if (ids == null) return 0;
            var xml = Utils.XmlFromIds(ids);
            var sqlParams = new SqlParameter[]
            {
               new SqlParameter("@IdsXml", SqlDbType.Xml)
                    {
                        Value = xml.ToString()
                    },
               new SqlParameter("@Status", SqlDbType.NVarChar)
                    {
                        Value = status
                    },
               new SqlParameter("@UpdatedBy", SqlDbType.NVarChar)
                    {
                        Value = user
                    },
               new SqlParameter("@Updated", SqlDbType.DateTime)
               {
                   Value = DateTime.Now
               }
            };

            var result = await _ctx.Database.ExecuteSqlCommandAsync("EXEC pi_UpdateDevicesStatus @IdsXml, @Status, @UpdatedBy, @Updated", sqlParams);
            if (result > 0)
            {
                var remark = "Edit in list device page.";
                foreach (var id in ids)
                {
                    var machine = await this.GetMachineByDeviceIdAsync(id);
                    var checkAddDeviceHistory = await this.AddDeviceHistoryAsync(id, status, user, remark, machine.Id);
                    if (!checkAddDeviceHistory)
                        return 0;
                }
            }
            return result;
        }

        public async Task<int> DeleteAsync(int id, string user)
        {
            var status = "delete";
            var remark = "delete success";
            var sqlParams = new SqlParameter[]
                {
                     new SqlParameter("@DeviceId", SqlDbType.Int)
                         {
                             Value = id
                         },
                     new SqlParameter("@Status", SqlDbType.NVarChar)
                         {
                             Value = status
                         },
                     new SqlParameter("@CreatedBy", SqlDbType.NVarChar)
                         {
                             Value = user
                         },
                     new SqlParameter("@Created", SqlDbType.DateTime)
                         {
                             Value = DateTime.Now
                         },
                     new SqlParameter("@Remark", SqlDbType.NVarChar)
                         {
                             Value = remark
                         },
                };
            var result = await _ctx.Database.ExecuteSqlCommandAsync("EXEC pi_DeleteDevice @DeviceId, @Status, @CreatedBy, @Created, @Remark", sqlParams);
            return result;
        }

        public async Task<int> UpdateAsync(int id, string remark, string user)
        {
            var sqlParams1 = new SqlParameter[]
                {
                     new SqlParameter("@Id", SqlDbType.Int)
                         {
                             Value = id
                         },
                     new SqlParameter("@Remark", SqlDbType.NVarChar)
                         {
                             Value = remark == null ? DBNull.Value : (object) remark
                         },
                     new SqlParameter("@UpdatedBy", SqlDbType.NVarChar)
                         {
                             Value = user
                         },
                     new SqlParameter("@Updated", SqlDbType.DateTime)
                         {
                             Value = DateTime.Now
                         },
                };
            var result = await _ctx.Database.ExecuteSqlCommandAsync("EXEC pi_UpdateDevice @Id, @Remark, @UpdatedBy, @Updated", sqlParams1);
            if (result == 0)
                return 0;

            return id;
        }

        public async Task<MachineVM[]> GetUnassignedMachinesAsync()
        {
            var result = await _ctx.Database.SqlQuery<MachineVM>("EXEC pi_GetUnassignedMachines").ToArrayAsync();
            return result;
        }

        public async Task<DeviceHistoryVM[]> GetListDeviceHistoriesByDeviceID(int deviceId)
        {
            var result = await _ctx.Database.SqlQuery<DeviceHistoryVM>("EXEC pi_GetDeviceHistories @DeviceId", new SqlParameter("DeviceId", deviceId)).ToArrayAsync();
            return result;
        }

        private async Task<bool> AddDeviceHistoryAsync(int id, string status, string user, string remark, int machineId)
        {
            var sqlParams1 = new SqlParameter[]
                {
                     new SqlParameter("@Id", SqlDbType.Int)
                         {
                             Value = id
                         },
                     new SqlParameter("@Status", SqlDbType.NVarChar)
                         {
                             Value = status
                         },
                     new SqlParameter("@CreatedBy", SqlDbType.NVarChar)
                         {
                             Value = user
                         },
                     new SqlParameter("@Created", SqlDbType.DateTime)
                         {
                             Value = DateTime.Now
                         },
                     new SqlParameter("@Remark", SqlDbType.NVarChar)
                         {
                             Value = remark
                         },
                     new SqlParameter("@MachineId", SqlDbType.Int)
                         {
                             Value = machineId
                         }
                };
            var result = await _ctx.Database.ExecuteSqlCommandAsync("EXEC pi_AddDevicesStatusHistories @Id, @Status, @CreatedBy, @Created, @Remark, @MachineId", sqlParams1);
            if (result == 0)
                return false;

            return true;
        }
        private async Task<MachineVM> GetMachineByDeviceIdAsync(int deviceId)
        {
            var result = await _ctx.Database.SqlQuery<MachineVM>("EXEC pi_GetMachineByDeviceId @DeviceId", new SqlParameter("DeviceId", deviceId)).FirstOrDefaultAsync();
            return result;
        }
    }
}
