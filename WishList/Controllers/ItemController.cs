namespace WishList.Controllers
{
    #region Usings

    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using WishList.Data;
    using WishList.Models;

    #endregion

    [Authorize]
    public class ItemController : Controller
    {
        #region Champs

        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        #endregion

        #region Constructeurs et destructeurs

        public ItemController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #endregion

        #region Méthodes publiques

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(Item item)
        {
            item.User = _userManager.GetUserAsync(HttpContext.User).Result;
            _context.Items.Add(item);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var item = _context.Items.FirstOrDefault(e => e.Id == id) ?? throw new ArgumentNullException("_context.Items.FirstOrDefault(e => e.Id == id)");
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            if (user != item.User)
            {
                return Unauthorized();
            }
            
            _context.Items.Remove(item);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            var model = _context.Items.Where(item => item.User.Id == user.Id).ToList();

            return View("Index", model);
        }

        #endregion
    }
}
