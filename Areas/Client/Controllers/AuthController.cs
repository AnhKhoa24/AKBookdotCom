using AKBookdotCom.Contacts;
using AKBookdotCom.Models.Support;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AKBookdotCom.Areas.Client.Controllers
{
    [Area("Client")]
    public class AuthController : Controller
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth", new { area = "Client" });
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            var reg = await _service.Login(model);
            if (reg.Flag)
            {
                if(reg.Role!.Trim() == "Admin")
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { area = "Client" });
                }
            }
            else
            {
                return View(reg);
            }
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Register(Register model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                return View(model);
            }
            var reg = await _service.Register(model);
            if(reg.Flag)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, reg.Message);
                return View(model);
            }    
        }
    }
}
