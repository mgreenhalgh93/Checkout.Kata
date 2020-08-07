namespace Checkout.Kata.Models
{
    public class Discount
    {
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public decimal? Value { get; set; }
        public int? Percentage { get; set; }
    }
}