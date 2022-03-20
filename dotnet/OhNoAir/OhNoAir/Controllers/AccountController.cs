using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OhNoAir.Models;
using System.Security.Claims;

namespace OhNoAir.Controllers
{
    public class AccountController : Controller
    {
        public async Task<IActionResult> Login(AccountViewModel accountModel)
        {
            if(accountModel?.Account?.UserName == "test1" && accountModel?.Account?.Password == "pass1")
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, accountModel?.Account?.UserName));
                claims.Add(new Claim("AccountID", "1"));

                ClaimsIdentity identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return RedirectToAction("Index", "Search");
            }
            else if (accountModel?.Account?.UserName == "admin" && accountModel?.Account?.Password == "pass2")
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, accountModel?.Account?.UserName));
                claims.Add(new Claim("AccountID", "2"));
                claims.Add(new Claim("IsAdmin", "true"));

                ClaimsIdentity identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return RedirectToAction("Index", "Search");
            }
            else if(!string.IsNullOrEmpty(accountModel?.Account?.UserName) || !string.IsNullOrEmpty(accountModel?.Account?.Password))
            {
                accountModel.Error = "User Name or Password is not correct";
                return View(accountModel);
            }



            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Index", "Search");
        }
    }
}
