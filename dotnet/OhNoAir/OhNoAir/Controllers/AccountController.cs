using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OhNoAir.Data;
using OhNoAir.Models;
using System.Security.Claims;

namespace OhNoAir.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Register(AccountViewModel accountModel)
        {

            if(!string.IsNullOrEmpty(accountModel?.Account?.UserName) 
                && !string.IsNullOrEmpty(accountModel?.Account?.Password))
            {
                Account existingAccount = 
                    _context.Account.Where(a => a.UserName == accountModel.Account.UserName).FirstOrDefault();

                if(existingAccount != null)
                {
                    accountModel.Error = "Please choose another username";
                    accountModel.Success = false;
                    return View(accountModel);
                }
                else
                {
                    Account newAccount = new Account();
                    newAccount.UserName = accountModel.Account.UserName;
                    newAccount.Password = accountModel.Account.Password;
                    newAccount.IsAdmin = false;
                    _context.Account.Add(newAccount);
                    _context.SaveChanges();

                    accountModel.Success = true;
                    return View(accountModel);

                }
            }



            return View();
        }

        public async Task<IActionResult> Login(AccountViewModel accountModel)
        {


            if (!string.IsNullOrEmpty(accountModel?.Account?.UserName)
                && !string.IsNullOrEmpty(accountModel?.Account?.Password))
            {
                Account existingAccount =
                    _context.Account.Where(a => a.UserName == accountModel.Account.UserName
                                            && a.Password == accountModel.Account.Password).FirstOrDefault();

                if (existingAccount == null)
                {
                    accountModel.Error = "User Name or Password is not correct";
                    return View(accountModel);
                }
                else
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, accountModel.Account.UserName));
                    claims.Add(new Claim("AccountID", existingAccount.AccountID.ToString()));

                    ClaimsIdentity identity = new ClaimsIdentity(claims, "MyCookieAuth");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                    return RedirectToAction("Index", "Search");
                }

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
