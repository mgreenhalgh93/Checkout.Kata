using System;
using System.Collections.Generic;
using System.Linq;
using Checkout.Kata.Models;

namespace Checkout.Kata
{
    public class Checkout
    {
        public Checkout()
        {
            Basket = new List<Item>();
            Discounts = new List<Discount>();
        }

        public List<Item> Basket { get; private set; }
        public List<Discount> Discounts { get; private set; }

        public decimal Total()
        {
            if(Basket != null && !Basket.Any())
                throw new NullReferenceException();

            CalculateTotal();
            return Basket.Sum(p => p.Price);
        }

        public void Scan(List<Item> items)
        {
            if (items != null && !items.Any())
                throw new NullReferenceException();

            foreach (Item i in items)
            {
                Scan(i);
            }
        }

        private void Scan(Item item)
        {
            if (item == null)
                throw new NullReferenceException();

            Basket.Add(item);
        }

        public void AddDiscounts(List<Discount> discounts)
        {
            if (discounts != null && !discounts.Any())
                throw new NullReferenceException();

            foreach (Discount i in discounts)
            {
                AddDiscount(i);
            }
        }

        private void CalculateTotal()
        {
            foreach(var discount in Discounts)
            {
                ApplyDiscount(discount);
            }
        }

        private void ApplyDiscount(Discount discount)
        {
            while(DiscountItems(discount).Count >= discount.Quantity)
            {
                var remainingItems = DiscountItems(discount);

                for (int i = 0; i < discount.Quantity; i++)
                {
                    remainingItems[i].Price = 0m;
                    remainingItems[i].Discounted = true;
                }

                remainingItems[0].Price = discount.Value;
            }
        }

        private void AddDiscount(Discount discount)
        {
            if (discount == null)
                throw new NullReferenceException();

            Discounts.Add(discount);
        }

        private List<Item> DiscountItems(Discount discount)
        {
            return Basket.FindAll(i => i.Sku == discount.Sku && !i.Discounted);
        }

 
    }
}
