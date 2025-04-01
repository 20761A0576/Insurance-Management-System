using InsuranceProviderManagementSystem.Models;
using InsuranceProviderManagementSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp;

namespace InsuranceProviderManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserService _userService = new UserService();
        public InsuranceProviderManagementSystemContext context = new InsuranceProviderManagementSystemContext();

        public ActionResult Login()
        {
            ViewBag.css = "id";
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (_userService.ValidateUser(username, password))
            {
                // Assuming UserId = 1 for this example
                int userId = 1;

                if (_userService.CanAccessToday(username))
                {
                    _userService.CreateSession(userId);
                    HttpContext.Session.SetString("UserId", username);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "You have exhausted your daily quota");
                    ViewBag.message = "You have exhausted your daily quota";
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid login");
                ViewBag.message = "Invalid login";
            }
            ViewBag.css = "id";
            return View();
        }

        public ActionResult Logout()
        {
            string? userId = HttpContext.Session.GetString("UserId");
            var userSession = context.UserSessionLogs.FirstOrDefault(u => u.UserId == userId);
            if (userId == null)
            {
                ViewBag.css = "id";
                return RedirectToAction("Login", "Login");
            }
            int timeSpentToday = (int)(DateTime.Now - userSession.LoginTime).TotalMinutes;
            userSession.Duration += timeSpentToday;
            HttpContext.Session.Clear();
            ViewBag.css = "id";
            return RedirectToAction("Login");
        }
    }
}
