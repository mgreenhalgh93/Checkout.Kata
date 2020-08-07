﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkout.Kata
{
    public class Checkout
    {
        public Checkout()
        {
            Basket = new List<Item>();
        }

        public List<Item> Basket { get; set; }

        public decimal Total()
        {
            return Basket.Sum(p => p.Price);
        }

        public void Scan(Item item)
        {
            if (item == null)
                throw new NullReferenceException();

            Basket.Add(item);
        }
    }
}
