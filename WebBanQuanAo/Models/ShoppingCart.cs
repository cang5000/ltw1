namespace WebBanQuanAo.Models
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public void AddItem(Product product, int quantity)
        {
            var cartItem = new CartItem
            {
                ImageUrl = product.ImageUrl,
                Images = product.Images,
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = quantity
            };

            var existingItem = Items.FirstOrDefault(i => i.ProductId == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                Items.Add(cartItem);
            }
        }


        public void RemoveItem(int productId)
        {
            Items.RemoveAll(i => i.ProductId == productId);
        }

        public Order ToOrder()
        {
            var order = new Order();
            order.OrderDate = DateTime.UtcNow;
            order.TotalPrice = CalculateTotalPrice(); // Gọi phương thức CalculateTotalPrice() để tính tổng giá trị
            order.OrderDetails = Items.Select(item => new OrderDetail
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price
            }).ToList();
            return order;
        }


        public decimal CalculateTotalPrice()
        {
            decimal totalPrice = 0;
            foreach (var item in Items)
            {
                totalPrice += item.Price * item.Quantity;
            }
            return totalPrice;
        }
    }
}
