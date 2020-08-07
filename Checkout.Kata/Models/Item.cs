namespace Checkout.Kata.Models
{
    public class Item
    {
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public bool Discounted { get; set; }
    }
}