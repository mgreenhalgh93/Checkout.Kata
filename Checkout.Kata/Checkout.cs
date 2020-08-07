using System;
using System.Collections.Generic;

namespace Checkout.Kata
{
    public class Checkout
    {
        public Checkout()
        {
            Basket = new List<Item>();
        }

        public List<Item> Basket { get; set; }

        public void Scan(Item item)
        {
            Basket.Add(item);
        }
    }
}
