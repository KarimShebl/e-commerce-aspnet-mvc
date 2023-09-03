using ecommerce.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext Db;

        public UserController(ILogger<HomeController> logger, UserManager<User> userManager, DbContextOptions<AppDbContext> options)
        {
            _logger = logger;
            this._userManager = userManager;
            Db = new AppDbContext(options);
        }
        public IActionResult GetUSer(string id)
        {
            User user = Db.Users.FirstOrDefault(x => x.Id == id);
            ViewBag.UserId = _userManager.GetUserId(this.User);
            ViewBag.Products = Db.Products.ToList();
            return View(user);
        }

        public IActionResult MyProducts()
        {
            ViewBag.UserId = _userManager.GetUserId(this.User);
            foreach (var us in Db.Users)
            {
                if(us.Id == ViewBag.UserId)
                {
                    ViewBag.User = us;
                }
            }
            ViewBag.Products = Db.Products.ToList();
            return View();
        }
    }
}
