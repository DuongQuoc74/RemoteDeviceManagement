using AutoMapper;
using Jabil.Pico.Web.BLL.Services;
using Jabil.Pico.Web.Models;
using Jabil.Pico.Common.ViewModels;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace Jabil.Pico.Web.Controllers
{
    [Authorize]
    public class HistoryController : Controller
    {
        readonly ITicketService _ticketService;

        public HistoryController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // GET: History
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var request = new TicketListVM();
            var vm = await _ticketService.GetAllAsync(request.FindFromDate, request.FindFromDate, request.FindMachineName);
            return View(new TicketListVM { Tickets = vm });
        }

        [HttpPost]
        [Route("History/Find")]
        public async Task<ActionResult> Find(TicketListVM ticketListVM)
        {
            var tickets = await _ticketService.GetAllAsync(ticketListVM.FindFromDate,ticketListVM.FindToDate !=null ? ticketListVM.FindToDate.Value.AddDays(1) : ticketListVM.FindToDate, ticketListVM.FindMachineName);
            ticketListVM.Tickets = tickets;
            return View("Index", ticketListVM);
        }
    }
}