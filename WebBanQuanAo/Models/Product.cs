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
<<<<<<< HEAD
		[Range(1, 10000000)]
		[DisplayName("Giá")]
		public decimal Price { get; set; }
        
=======
        [Range(1, 10000)]
        [DisplayName("Giá")]
        public decimal Price { get; set; }
>>>>>>> d1acac34d32653a8a4d3448f67628ee4c650203a
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
