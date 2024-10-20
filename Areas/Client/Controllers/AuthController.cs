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

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
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

        [HttpPost]
        public async Task<IActionResult>Register(Register model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reg = await _service.Register(model);
            if(reg.Flag)
            {
                return Ok(reg);
            }else
            {
                return BadRequest(reg);
            }    
        }
    }
}
