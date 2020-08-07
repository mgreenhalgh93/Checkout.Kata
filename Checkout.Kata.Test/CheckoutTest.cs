using System;
using System.Collections.Generic;
using Checkout.Kata.Models;
using Xunit;

namespace Checkout.Kata.Test
{
    public class CheckoutTest
    {
        private readonly Checkout _checkout;
        private readonly List<Item> _items;

        public CheckoutTest()
        {
            _checkout = new Checkout();
            _items = new List<Item>();
        }

        [Fact]
        public void ItemScannedShowsInBasketTotal()
        {
            //Arrange
            _items.Add(new Item());

            //Act
            _checkout.Scan(_items);

            var result = _checkout.Basket.Count;

            //Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void IfItemIsNullThrowsException()
        {
            //Arrange

            //Act & Assert
            Assert.Throws<NullReferenceException>(() => _checkout.Scan(null));
        }

        [Fact]
        public void MultipleScannedItemsReturnsBasketTotal()
        {
            //Arrange
            _items.Add(new Item());
            _items.Add(new Item());

            //Act
            _checkout.Scan(_items);

            var result = _checkout.Basket.Count;

            //Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void ItemAddedToBasketReturnsCorrectTotal()
        {
            //Arrange
            _items.Add( new Item { Sku = "A", Price = 10m });

            //Act
            _checkout.Scan(_items);

            var result = _checkout.Total();

            //Arrange
            Assert.Equal(10m, result);
        }

        [Fact]
        public void BasketEmptyThrowsNullExceptionWhenReturningResult()
        {
            //Arrange

            //Act & Assert
            Assert.Throws<NullReferenceException>(() => _checkout.Total());
        }

        [Fact]
        public void MultipleItemsAddedToBasketReturnsCorrectTotal()
        {
            //Arrange
            _items.Add(new Item { Sku = "A", Price = 10m });
            _items.Add(new Item { Sku = "C", Price = 40m });

            //Act
            _checkout.Scan(_items);

            var result = _checkout.Total();

            //Arrange
            Assert.Equal(50m, result);
        }

        [Fact]
        public void ItemsListEmptyThrowsNullExceptionWhenReturningResult()
        {
            //Arrange

            //Act & Assert
            Assert.Throws<NullReferenceException>(() => _checkout.Scan(_items));
        }

        [Fact]
        public void GivenDiscountIsAppliedToItemCorrectTotalIsReturned()
        {
            //Arrange
            var discount = new Discount { Sku = "B", Quantity = 3, Value = 40m };
            _items.Add(new Item { Sku = "B", Price = 15m });
            _items.Add(new Item { Sku = "B", Price = 15m });
            _items.Add(new Item { Sku = "B", Price = 15m });

            //Act
            _checkout.AddDiscount(discount);
            _checkout.Scan(_items);

            var result = _checkout.Total();

            //Arrange
            Assert.Equal(40m, result);
        }

        [Fact]
        public void DiscountEmptyThrowsNullExceptionWhenTryingToAdd()
        {
            //Arrange

            //Act & Assert
            Assert.Throws<NullReferenceException>(() => _checkout.AddDiscount(null));
        }
    }
}
