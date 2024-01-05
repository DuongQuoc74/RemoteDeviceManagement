using AutoMapper;
using Jabil.Pico.Common.ViewModels;
using Jabil.Pico.Web.BLL.Services;
using Jabil.Pico.Web.Common;
using Jabil.Pico.Web.Models;
using Jabil.Pico.Web.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace Jabil.Pico.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRoleService _userRoleService;
        public UserController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [RoleAttribute(RoleId = EnumRole.Admin)]
        // GET: User
        public async Task<ActionResult> Index()
        {
            var users = await _userRoleService.GetAllAsync();
            return View(new UserRoleListVM { UserRoles = users });
        }

        [RoleAttribute(RoleId = EnumRole.Admin)]
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.SuccessMsg = TempData["result"];
            return View();
        }

        [RoleAttribute(RoleId = EnumRole.Admin)]
        [HttpPost]
        public async Task<ActionResult> Add(UserRoleVM vm)
        {         
             var check = await _userRoleService.AddAsync(vm);
            if (!check)
            {
                TempData["result"] = $"The '{vm.EmployeeNumber}' already exists!";
                return RedirectToAction("Add");
            }
           
            return RedirectToAction("Index");
        }

        [RoleAttribute(RoleId = EnumRole.Admin)]
        [HttpPost]
        public async Task<ActionResult> Activate(int userId)
        {
             await _userRoleService.ActivateAsync(userId);
            return RedirectToAction("Index");
        }

        [RoleAttribute(RoleId = EnumRole.Admin)]
        [HttpPost]
        public async Task<ActionResult> Deactivate(int userId)
        {
            await _userRoleService.DeactivateAsync(userId);
            return RedirectToAction("Index");
        }

        [RoleAttribute(RoleId = EnumRole.Admin)]
        [HttpPost]
        public async Task<ActionResult> Promote(int userId)
        {
            await _userRoleService.PromoteAsync(userId);
            return RedirectToAction("Index");
        }

        [RoleAttribute(RoleId = EnumRole.Admin)]
        [HttpPost]
        public async Task<ActionResult> Demote(int userId)
        {
            await _userRoleService.DemoteAsync(userId);
            return RedirectToAction("Index");
        }

        [RoleAttribute(RoleId = EnumRole.Admin)]
        [HttpPost]
        public async Task<ActionResult>Delete(int userId)
        {
            await _userRoleService.DeleteAsync(userId);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string username, int code = 0)
        {
            return View(new LoginVM { Username = username, ErrorCode = code });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(LoginVM login)
        {
            //if (LdapAuthenticator.NovellAuthenticate(login.Username, login.Password))
            {
                var user = await _userRoleService.GetByNameAsync(login.Username);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(login.Username, false);
                    return RedirectToAction("Index", "Device");
                }
                return RedirectToAction("Unauthorised", "Home");      
            }
            login.Password = "";
            login.ErrorCode = 1;
            return RedirectToAction("Login", new { username = login.Username, code = 1 });
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

    }
}