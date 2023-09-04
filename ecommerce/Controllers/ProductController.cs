using ecommerce.Areas.Identity.Data;
using ecommerce.Models.e_commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
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
        public IActionResult New(Product product, string Stat)
        {
            ViewBag.Stat = Stat;
            ViewBag.UserID = _userManager.GetUserId(this.User);
            foreach (var user in _userManager.Users)
            {
                if (user.Id == ViewBag.UserId)
                {
                    ViewBag.UserName = user.Name;
                    ViewBag.UserEmail = user.Email;
                }
            }
            ViewBag.Title = "Add your product";
            return View("Form", product);
        }

        public IActionResult Edit(int id, string Stat)
        {
            ViewBag.Stat = Stat;
            ViewBag.UserID = _userManager.GetUserId(this.User);
            Product product = Db.Products.FirstOrDefault(p => p.Id == id);
            foreach (var user in _userManager.Users)
            {
                if (user.Id == ViewBag.UserId)
                {
                    ViewBag.UserName = user.Name;
                    ViewBag.UserEmail = user.Email;
                }
            }
            ViewBag.Title = "Edit your product";
            return View("Form", product);
        }
        [HttpPost]
        public IActionResult Save(Product product, string Stat)
        {
            ViewBag.Stat = Stat;
            if (ModelState.IsValid)
            {
                if (product.Image != null)
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

        public IActionResult DeletePage(int id)
        {
            Product product = Db.Products.FirstOrDefault(x => x.Id == id);
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            Product product = Db.Products.FirstOrDefault(x => x.Id == id);
            Db.Products.Remove(product);
            Db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult BuyPage(int id)
        {
            Product product = Db.Products.FirstOrDefault(x => x.Id == id);
            ViewBag.Quantity = 0;
            return View(product);
        }

        [HttpPost]
        public IActionResult Buy(int id, int Quantity, int T)
        {
            if (T == 0)
            {
                foreach (Product product in Db.Products)
                {
                    if (product.Id == id)
                    {
                        product.Quantity -= Quantity;
                        if (product.Quantity == 0)
                        {
                            Db.Remove(product);
                        }
                    }
                }
                Db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.UserID = _userManager.GetUserId(this.User);
                Product NewProduct = new Product();
                foreach (Product p in Db.Products)
                {
                    if (p.Id == id)
                    {
                        NewProduct.Name = p.Name;
                        NewProduct.Price = p.Price;
                        NewProduct.SellerId = p.SellerId;
                        NewProduct.SellerName = p.SellerName;
                        NewProduct.Description = p.Description;
                        NewProduct.CartId = ViewBag.UserId;
                        NewProduct.ImageURL = p.ImageURL;
                        NewProduct.Quantity = Quantity;
                        NewProduct.CartProduct = true;
                        NewProduct.OriginalId = p.Id;
                    }
                }
                foreach (var p in Db.Cart)
                {
                    if (p.ImageURL == NewProduct.ImageURL && p.CartProduct == true && p.CartId == ViewBag.UserID)
                    {
                        NewProduct = p;
                        NewProduct.Quantity += Quantity;
                        break;
                    }
                }
                Db.Cart.Update(NewProduct);
                Db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
