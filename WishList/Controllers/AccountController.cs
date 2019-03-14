namespace WishList.Controllers
{
    #region Usings

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using WishList.Models;

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
    }
}
