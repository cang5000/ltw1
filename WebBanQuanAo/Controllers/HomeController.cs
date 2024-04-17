using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebBanQuanAo.Data;
using WebBanQuanAo.Models;
using WebBanQuanAo.ViewModels;


namespace WebBanQuanAo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }



        public IActionResult Index()
        {
            var viewModel = new ProductCategoryViewModel
            {
                Products = _context.Products.ToList(),
                Categories = _context.Categories.ToList()
            };

            return View(viewModel);
        }

        public IActionResult Find(string timKiem)
        {


            if (!string.IsNullOrEmpty(timKiem))
            {
                var matchingProducts = _context.Products
                    .Where(p => p.Name.Contains(timKiem))
                    .ToList();
                return View(matchingProducts);
            }

            var allProducts = _context.Products.ToList();
            return View(allProducts);
            
        }

        public IActionResult ProductsByCategory(int categoryId)
        {
            var products = _context.Products
                                    .Where(p => p.CategoryId == categoryId)
                                    .ToList();

            var viewModel = new ProductCategoryViewModel
            {
                Products = products,
                // Đảm bảo rằng bạn đã thiết lập Categories trong ViewModel nếu cần thiết
            };

            return PartialView("_Products", viewModel);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
