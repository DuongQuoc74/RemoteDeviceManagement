using AutoMapper;
using Jabil.Pico.Web.BLL.Services;
using Jabil.Pico.Common.ViewModels;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Jabil.Pico.Web.Models;
using Jabil.Pico.Web.DAL.Models;
using Jabil.Pico.Common;
using Antlr.Runtime.Misc;
using System.Web.Services.Description;
using Jabil.Pico.Common.Common;
using System.Linq;

namespace Jabil.Pico.Web.Controllers
{
    [Authorize]
    public class DeviceController : Controller
    {
        private readonly IDeviceService _deviceService;
        private readonly IApiLogService _apiLogService;
        private readonly ITicketService _ticketService;
        private readonly ISettingService _settingService;
        

        public DeviceController(IDeviceService deviceService, ITicketService ticketService, IApiLogService apiLogService, ISettingService settingService)
        {
            _deviceService = deviceService;
            _ticketService = ticketService;
            _apiLogService = apiLogService;
            _settingService = settingService;
        }

        // GET: Devices
        public async Task<ActionResult> Index()
        {
            var deviceList = await this.SettingsChangeMultipleDeviceValuesAsync(); 
            return View(deviceList);
        }

        public async Task<ActionResult> Partial()
        {
            var deviceList = await this.SettingsChangeMultipleDeviceValuesAsync();
            return PartialView("DevicesTable", deviceList);
        }

        public async Task<ActionResult> UpdateStatusSingle(int id, string status, int machineId)
        {
            var updateResult = await _deviceService.UpdateStatusSingleAsync(id, status, User.Identity.Name, machineId);
            if (!updateResult)
            {
                return Redirect("Error");
            }
            return RedirectToAction("Detail", new { id });
        }

        [HttpPost]
        public async Task<JsonResult> UpdateStatusList(int[] ids, bool active)
        {
            try
            {
                await _deviceService.UpdateStatusMultiAsync(ids, active ? DeviceStatusValues.Active : DeviceStatusValues.Deactivated, User.Identity.Name);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("device/{id}")]
        public async Task<ActionResult> Detail(int id)
        {
            var deviceList = new DeviceDetailVM();
            var device = await _deviceService.GetByIdAsync(id);
            if (device == null)
            {
                return Redirect("Error");
            }
            else
            {
                var tickets = await _ticketService.FindTicketsByMachineAsync(device.MachineId);
                device.TicketListVM = new TicketListVM
                {
                    Tickets = tickets
                };
                var settings = await _settingService.GetAllAsync();
                var deviceSetting = new DeviceSettingVM()
                {
                    OnlineTimeOutMinutes = settings.First(s => s.Name == SystemConstant.Settings.OnlineTimeOutMinutes).Value,
                    TicketsApiCheckTimeOutMinutes = settings.First(s => s.Name == SystemConstant.Settings.TicketsApiCheckTimeOutMinutes).Value,
                    TicketsValidDays = settings.First(s => s.Name == SystemConstant.Settings.TicketsValidDays).Value,
                    CloseTicketURL = settings.First(s => s.Name == SystemConstant.Settings.CloseTicketURL).Value,
                    CloseTicketParameterProcess = settings.First(s => s.Name == SystemConstant.Settings.CloseTicketParameterProcess).Value,
                    CloseTicketParameterProcesId = settings.First(s => s.Name == SystemConstant.Settings.CloseTicketParameterProcesId).Value,
                };
                var deviceHistories = await _deviceService.GetListDeviceHistoriesByDeviceID(device.Id);
                device.ListDeviceHistoryVM = deviceHistories;
                deviceList.Device= device;
                deviceList.DeviceSetting = deviceSetting;
                return View(deviceList);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Detail(int id, string remark)
        {
            var result = await _deviceService.UpdateAsync(id, remark, User.Identity.Name);
            if (result == 0)
            {
                return Redirect("Error");
            }
            return RedirectToAction("Detail", new { id = result});
        }

        [HttpGet]
        public async Task<ActionResult> Add(int? machineId, string remark)
        {
            var machines = await _deviceService.GetUnassignedMachinesAsync();

            return View( new DeviceAddVM
            {
                Machines = machines,
                MachineId = machineId ?? -1,
                Remark = remark
            });
        }

        [HttpPost]
        public async Task<ActionResult> Add(int MachineId, string Remark)
        {
            var device = new Device
            {
                MachineId = MachineId,
                Remark = Remark,
                CreatedBy = User.Identity.Name,
                Created = DateTime.Now,
                Available = true,
                Guid = Guid.NewGuid(),
                Status = DeviceStatusValues.Deactivated
            };

            var deviceId = await _deviceService.AddAsync(device);
            if (deviceId > 0)
            {
                return RedirectToAction("Detail", new { id = deviceId });
            }

            return RedirectToAction("Add", new { machineId = device.Id, remark = device.Remark });
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _deviceService.DeleteAsync(id, User.Identity.Name);

            if (deleted==2)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Detail", new { id });
        }

        [HttpGet]
        public async Task<ActionResult> ApiLogs()     
        {
            var apiLogs = await _apiLogService.GetAllApiLogsAsync(null, null, null);
            return View(new ApiLogsListVM { ApiLogs = apiLogs });
        }

        [HttpPost]
        [Route("Device/FindApiLogs")]
        public async Task<ActionResult> FindApiLogs(ApiLogsListVM request)
        {
            var apiLogs = await _apiLogService.GetAllApiLogsAsync(request.FindFromDate, request.FindToDate != null? request.FindToDate.Value.AddDays(1) : request.FindToDate, request.GuidDevice );
            request.ApiLogs = apiLogs;
            return View("ApiLogs", request );
        }

        private async Task<DeviceListVM> SettingsChangeMultipleDeviceValuesAsync()
        {
            var deviceList = new DeviceListVM();
            var devices = await _deviceService.GetAllAsync();
            var settings = await _settingService.GetAllAsync();
            var deviceSetting = new DeviceSettingVM()
            {
                OnlineTimeOutMinutes = settings.First(s => s.Name == SystemConstant.Settings.OnlineTimeOutMinutes).Value,
                TicketsApiCheckTimeOutMinutes = settings.First(s => s.Name == SystemConstant.Settings.TicketsApiCheckTimeOutMinutes).Value,
                TicketsValidDays = settings.First(s => s.Name == SystemConstant.Settings.TicketsValidDays).Value,
                CloseTicketURL = settings.First(s => s.Name == SystemConstant.Settings.CloseTicketURL).Value,
                CloseTicketParameterProcess = settings.First(s => s.Name == SystemConstant.Settings.CloseTicketParameterProcess).Value,
                CloseTicketParameterProcesId = settings.First(s => s.Name == SystemConstant.Settings.CloseTicketParameterProcesId).Value,
            };
            deviceList.Devices = devices;
            deviceList.DeviceSetting = deviceSetting;
            return deviceList;
        }
    }
}