using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebBanQuanAo.Models;
using WebBanQuanAo.Repository;
public static class SessionExtensions
{
    public static T Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
    }

    public static void Set<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

}
namespace WebBanQuanAo.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ShoppingCartController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart") ?? new ShoppingCart();
            return View(cart);
        }

        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            var cart = HttpContext.Session.Get<ShoppingCart>("Cart") ?? new ShoppingCart();
            cart.AddItem(product, quantity);
            HttpContext.Session.Set("Cart", cart);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart");
            if (cart != null)
            {
                cart.RemoveItem(productId);
                HttpContext.Session.Set("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart");
            if (cart == null || cart.Items.Count == 0)
            {
                return RedirectToAction("Index");
            }
            var totalPrice = cart.Items.Sum(item => item.Price * item.Quantity); // Tính toán tổng giá tiền của giỏ hàng
            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                TotalPrice = totalPrice, // Gán giá trị cho TotalPrice của đơn hàng
                OrderDetails = cart.Items.Select(item => new OrderDetail
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };
            return View(order);
        }

        public IActionResult ProcessOrder()
        {
            // Lấy thông tin đơn hàng từ Session hoặc từ cơ sở dữ liệu
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart");

            // Kiểm tra xem giỏ hàng có sản phẩm không
            if (cart == null || cart.Items.Count == 0)
            {
                // Nếu không có sản phẩm trong giỏ hàng, chuyển hướng người dùng đến trang giỏ hàng
                return RedirectToAction("Index", "ShoppingCart");
            }

            try
            {
                // Xử lý lưu đơn hàng vào cơ sở dữ liệu
                // Ví dụ: bạn có thể sử dụng repository để lưu đơn hàng vào cơ sở dữ liệu
                // orderRepository.SaveOrder(cart.ToOrder());

                // Sau khi lưu đơn hàng thành công, xóa giỏ hàng khỏi Session
                HttpContext.Session.Remove("Cart");

                // Trả về view OrderProcessed.cshtml với model là đơn hàng đã được xử lý
                return View("OrderProcessed", cart.ToOrder());
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ (nếu cần)
                // Ví dụ: ghi log, hiển thị thông báo lỗi, ...

                // Chuyển hướng người dùng đến trang thông báo lỗi
                return RedirectToAction("Error");
            }
        }



        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            // Xử lý lưu đơn hàng vào cơ sở dữ liệu
            // Sau đó, chuyển hướng sang trang thông báo đặt hàng thành công hoặc thông tin đơn hàng
            return RedirectToAction("OrderDetails", new { id = order.Id });
        }

        public IActionResult OrderDetails(int id)
        {
            // Lấy thông tin đơn hàng từ cơ sở dữ liệu dựa trên id
            var order = new Order(); // Thay thế dòng này bằng logic lấy thông tin đơn hàng từ cơ sở dữ liệu
            return View(order);
        }

        public IActionResult Error()
        {
            // Xử lý lỗi và hiển thị view hoặc thông báo lỗi tùy thuộc vào yêu cầu của bạn
            return View();
        }

    }
}
