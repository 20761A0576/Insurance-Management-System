using InsuranceProviderManagementSystem.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using InsuranceProviderManagementSystem.Business_Object;
using InsuranceProviderManagementSystem.Repository;
using Microsoft.IdentityModel.Tokens;
using InsuranceProviderManagementSystem.Services;

namespace InsuranceProviderManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InsuranceProviderManagementSystemContext _context;
        private const int DailyQuotaMinutes = 4;
        private readonly UserService _userService = new UserService();

        //public HomeController(InsuranceProviderManagementSystemContext context)
        //{
        //    _context = context;
        //}

        public HomeController(ILogger<HomeController> logger, InsuranceProviderManagementSystemContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {


            string? userId = HttpContext.Session.GetString("UserId");
            //var userSession = _context.UserSessionLogs.FirstOrDefault(u => u.UserId == userId);
            //if (userId == null)
            //{
            //    ViewBag.css = "id";
            //    return RedirectToAction("Login", "Login");
            //}
            //int timeSpentToday = (int)(DateTime.Now - userSession.LoginTime).TotalMinutes;
            //userSession.Duration += timeSpentToday;
            //userSession.LoginTime = DateTime.Now;
            //if (userSession.Duration >= DailyQuotaMinutes)
            //{
            //    _context.SaveChanges();
            //    ModelState.AddModelError("", "You have exhausted your daily quota.");
            //    return View("QuotaExceeded");
            //}
            _context.SaveChanges();
            // Use userId as needed
            ViewBag.UserId = userId;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //[HttpGet]
        //public IActionResult Login() => View();

        //[HttpPost]
        //public async Task<IActionResult> Login(string username, string password)
        //{
        //    //var user = (from ud in _context.UserDetails
        //    //            where ud.UserName == username && ud.Password == password
        //    //            select ud);
        //    var user = _context.UserDetails.First(user => user.UserName == username && user.Password == password);

        //    if (user == null)
        //    {
        //        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //        return View();
        //    }

        //    var claims = new List<System.Security.Claims.Claim>
        //    {
        //        new System.Security.Claims.Claim(ClaimTypes.Name, user.UserName),
        //        new System.Security.Claims.Claim(ClaimTypes.Role, user.Role)
        //    };
        //    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        //    return RedirectToAction("Index", "Home");
        //}

        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return RedirectToAction("Login");
        //}
    }
}
