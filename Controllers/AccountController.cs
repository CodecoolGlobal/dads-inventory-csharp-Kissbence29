using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DadsInventory.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace DadsInventory.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _repository;

        public AccountController(IUserRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Checking if username and password is empty or not
            if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
            {
                ViewBag.Message = "Empty value is not acceptable!";
                return View();
            }
            // Learn about Claims Principle -> https://docs.microsoft.com/en-us/dotnet/api/system.security.claims.claimsprincipal?view=netframework-4.8            

            // Here you can fetch your user information from the database
            var User = await _repository.AuthenticateAsync(username, password);
            if (User == null)
            {
                ViewBag.Message = "Username or Password is invalid!";
                return View();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, User.Username)
            };
            claims.Add(new Claim(ClaimTypes.Role, User.Role));
            // Learn more about ClaimsIdentity -> https://docs.microsoft.com/en-us/dotnet/api/system.security.claims.claimsidentity?view=netframework-4.8

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
            );
            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
