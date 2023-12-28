namespace DjStore.Entities
{
    public class Basket
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<BasketItems> BasketItems { get; set; } = new List<BasketItems>();

        public void AddItem(Product product, int quantity)
        {
            if (BasketItems.All(item => item.ProductId != product.Id))
            {
                BasketItems.Add(new BasketItems { Product = product, Quantity = quantity });
            }
            var existingItems = BasketItems.FirstOrDefault(item => item.ProductId == product.Id);
            if (existingItems != null) existingItems.Quantity += quantity;
        }

        public void RemoveItem(int productId, int quantity)
        {
            var basketItem = BasketItems.FirstOrDefault(item => item.ProductId == productId);
            if (basketItem == null) return;
            basketItem.Quantity -= quantity;
            if (basketItem.Quantity == 0)
            {
                BasketItems.Remove(basketItem);
            }
        }
    }


}