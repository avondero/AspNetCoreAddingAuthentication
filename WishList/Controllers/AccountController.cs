namespace WishList.Controllers
{
    #region Usings

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
    }
}
