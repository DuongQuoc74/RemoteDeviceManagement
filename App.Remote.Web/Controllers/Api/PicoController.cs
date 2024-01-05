using Jabil.Pico.Common;
using Jabil.Pico.Web.BLL.Services;
using Jabil.Pico.Web.Common;
using Jabil.Pico.Web.DAL.Models;
using Jabil.Pico.Web.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Management;

namespace Jabil.Pico.Web.Controllers.Api
{
    [ApiAuthorizeAttribute]
    public class PicoController : ApiController
    {
        private readonly ITicketService _ticketService;
        private readonly IApiLogService _apiLogService;
        private readonly ISettingService _settingService;

        public PicoController(ITicketService ticketService, IApiLogService apiLogService, ISettingService settingService)
        {
            _ticketService = ticketService;
            _apiLogService = apiLogService;
            _settingService = settingService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetOpenTicket(string machineGuid)
        {
            var apiKey = $"api-key:{Request.Headers.GetValues("api-key").FirstOrDefault()}";
            try
            {
                var ticket = await _ticketService.FindOpenTicketAsync(machineGuid);

                if (ticket == null)
                {
                    // Log
                    await _apiLogService.AddAsync(new Models.ApiLog
                    {
                        Api = "api/pico/getOpenTicket",
                        Request = $"api/pico/getOpenTicket?{nameof(machineGuid)}={machineGuid}",
                        Response = "404 Not Found",
                        MachineId = -1,
                        Created = DateTime.Now,
                        CreatedBy = apiKey,
                        DeviceGuid = new Guid(machineGuid)
                    });

                    return NotFound();
                }

                // Log
                await _apiLogService.AddAsync(new Models.ApiLog
                {
                    Api = "api/pico/getOpenTicket",
                    Request = $"api/pico/getOpenTicket?{nameof(machineGuid)}={machineGuid}",
                    Response = JsonConvert.SerializeObject(ticket),
                    MachineId = -1,
                    Created = DateTime.Now,
                    CreatedBy = apiKey,
                    DeviceGuid = new Guid(machineGuid)
                });

                return Json(ticket);
            }
            catch (Exception ex)
            {
                // Log
                await _apiLogService.AddAsync(new Models.ApiLog
                {
                    Api = "api/pico/getOpenTicket",
                    Request = $"api/pico/getOpenTicket?{nameof(machineGuid)}={machineGuid}",
                    Response = $"400 Bad Request {ex}",
                    MachineId = -1,
                    Created = DateTime.Now,
                    CreatedBy = apiKey,
                    DeviceGuid = new Guid(machineGuid)
                });

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Ping(string machineGuid)
        {
            var apiKey = $"api-key:{Request.Headers.GetValues("api-key").FirstOrDefault()}";

            // Log
            await _apiLogService.AddAsync(new Models.ApiLog
            {
                Api = "api/pico/ping",
                Request = $"api/pico/ping?{nameof(machineGuid)}={machineGuid}",
                Response = "ok",
                MachineId = -1,
                Created = DateTime.Now,
                CreatedBy = apiKey,
                DeviceGuid = new Guid(machineGuid)
            });

            var settings = await _settingService.GetAllAsync();
            var settingFilter =  settings.Where(x=>x.IsPico).ToArray();
            return Json(settingFilter);
        }
    }
}
