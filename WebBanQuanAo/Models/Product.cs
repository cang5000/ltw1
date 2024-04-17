using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebBanQuanAo.Models
{

    public class Product
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        [DisplayName("Tên sản phẩm")]
        public string? Name { get; set; }
		[Range(1, 10000000)]
		[DisplayName("Giá")]
		public decimal Price { get; set; }
        
        [DisplayName("Mô tả")]
        public string? Description { get; set; }
        [DisplayName("Ảnh")]
        public string? ImageUrl { get; set; }
        public List<ProductImage>? Images { get; set; }
        public int CategoryId { get; set; }
        [DisplayName("Loại sản phẩm")]
        public Category? Category { get; set; }
    }
}
