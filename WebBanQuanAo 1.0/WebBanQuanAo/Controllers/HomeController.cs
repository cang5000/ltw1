using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebBanQuanAo.Data;
using WebBanQuanAo.Models;


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
            var products = _context.Products.ToList(); // Lấy danh sách sản phẩm từ cơ sở dữ liệu
            return View(products); // Trả về view và truyền danh sách sản phẩm
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
