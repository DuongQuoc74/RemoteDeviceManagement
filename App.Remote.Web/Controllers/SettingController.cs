using Jabil.Pico.Common.ViewModels;
using Jabil.Pico.Web.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Jabil.Pico.Web.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {

        private readonly ISettingService _settingService;
        public SettingController(ISettingService settingService) 
        { 
            _settingService = settingService;
        }

        [Route("Setting")]
        public async Task<ActionResult> Index()
        {
            var settings = await _settingService.GetAllNowAsync();
            return View(settings);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string name)
        {
            var setting = await _settingService.GetByNameAsync(name);
            return Json(new { data = setting },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(SettingVM request)
        {
            var result = await _settingService.UpdateAsync(request);
            if(!result)
                return Json(new {success = false});
            return RedirectToAction("Index","Setting");
        }
    }
}