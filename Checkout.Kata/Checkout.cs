﻿using System;
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
                if (!discount.Value.HasValue)
                    ApplyPercentageBasedDiscount(discount);
                else
                    ApplyValueBasedDiscount(discount);
            }
        }

        private void ApplyPercentageBasedDiscount(Discount discount)
        {
            var currentPriceSum = DiscountItems(discount).Sum(p => p.Price);

            while (DiscountItems(discount).Count >= discount.Quantity)
            {
                List<Item> remainingItems = ApplyDiscount(discount);
                var newValue = (currentPriceSum / 100) * discount.Percentage;
                remainingItems[0].Price = (decimal)newValue;
            }
        }

        private void ApplyValueBasedDiscount(Discount discount)
        {
            while(DiscountItems(discount).Count >= discount.Quantity)
            {
                List<Item> remainingItems = ApplyDiscount(discount);
                remainingItems[0].Price = discount.Value.Value;
            }
        }

        private List<Item> ApplyDiscount(Discount discount)
        {
            var remainingItems = DiscountItems(discount);

            for (int i = 0; i < discount.Quantity; i++)
            {
                remainingItems[i].Price = 0m;
                remainingItems[i].Discounted = true;
            }

            return remainingItems;
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
