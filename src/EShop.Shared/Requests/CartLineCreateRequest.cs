namespace EShop.Shared.Requests
{
    public class CartLineCreateRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
