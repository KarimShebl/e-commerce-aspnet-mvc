using ecommerce.Areas.Identity.Data;
using ecommerce.Models;
using ecommerce.Models.e_commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext Db;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, DbContextOptions<AppDbContext> options)
        {
            _logger = logger;
            this._userManager = userManager;
            Db = new AppDbContext(options);
        }

        public IActionResult Index()
        {
            ViewBag.UserId = _userManager.GetUserId(this.User);
            ViewBag.Type = 0;
            
            ViewBag.Products = Db.Products.ToList();
            foreach (var user in _userManager.Users)
            {
                if (user.Id == ViewBag.UserId)
                {
                    ViewBag.Type = user.Type;
                }    
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}