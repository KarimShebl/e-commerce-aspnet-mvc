using ecommerce.Areas.Identity.Data;
using ecommerce.Models.e_commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext Db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ILogger<HomeController> logger, UserManager<User> userManager, DbContextOptions<AppDbContext> options, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            this._userManager = userManager;
            Db = new AppDbContext(options);
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult AddOrEdit(Product product, string Stat)
        {
            ViewBag.Stat = Stat;
            ViewBag.UserID = _userManager.GetUserId(this.User);
            foreach (var user in _userManager.Users)
            {
                if (user.Id == ViewBag.UserId)
                {
                    ViewBag.UserName = user.Name;
                }
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Save(Product product) {
            if (ModelState.IsValid)
            {
                if(product.Image != null)
                {
                    string folder = $"Products/Images/{Guid.NewGuid().ToString()}{product.Image.FileName}";
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    product.ImageURL = folder;
                    product.Image.CopyTo(new FileStream(serverFolder, FileMode.Create));
                }
                Db.Products.Update(product);
                Db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Form", product);
            }
        }
    }
}
