using System.Collections.Generic;
using WebBanQuanAo.Models; // Thay WebBanQuanAo.Models bằng tên namespace của bạn

namespace WebBanQuanAo.ViewModels
{
    public class ProductCategoryViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }

}
