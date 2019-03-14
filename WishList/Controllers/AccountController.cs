namespace WishList.Controllers
{
    #region Usings

    using System;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using WishList.Models;
    using WishList.Models.AccountViewModels;

    #endregion

    [Authorize]
    public class AccountController : Controller
    {
        #region Champs

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly UserManager<ApplicationUser> _userManager;

        #endregion

        #region Constructeurs et destructeurs

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #endregion

        #region Méthodes publiques

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _userManager.CreateAsync(new ApplicationUser { UserName = model.Email, Email = model.Email }, model.Password).Result;
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Password", error.Description);
                }

                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false).Result;

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalide login attempt");
                return View(model);
            }

            return RedirectToAction("Index", "Item");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
