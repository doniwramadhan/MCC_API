using Client.Contracts;
using Microsoft.AspNetCore.Mvc;
using APIMCC.DTOs.Accounts;
using NuGet.Protocol.Core.Types;

namespace Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public IActionResult Login()
        {
            return View();
        }


        //public async Task<IActionResult> Login(LoginDto login)
        //{
        //    var result = await _accountRepository.Login(login);
        //    if (result.Code == 200)
        //    {
        //        HttpContext.Session.SetString("JWToken", result.Data.Token);
        //        return RedirectToAction("Index", "Home");
        //    }
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var result = await _accountRepository.Login(login);
            if (result is null)
            {
                return RedirectToAction("Error", "Home");
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
            else if (result.Code == 200)
            {
                HttpContext.Session.SetString("JWToken", result.Data.Token);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
