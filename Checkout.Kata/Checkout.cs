using System;
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
            if(Basket != null && !Basket.Any())
                throw new NullReferenceException();

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
    }
}
