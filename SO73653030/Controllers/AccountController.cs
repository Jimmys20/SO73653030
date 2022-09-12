using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SO73653030.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsHelper.Claim_UserRole, "user")
            };

            var claimsIdentity = new ClaimsIdentity(claims, "password");

            await HttpContext.SignInAsync("cookie", new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }
    }
}
