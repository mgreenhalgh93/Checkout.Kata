﻿using System;
using System.Collections.Generic;
using Checkout.Kata.Models;
using Xunit;

namespace Checkout.Kata.Test
{
    public class CheckoutTest
    {
        private readonly Checkout _checkout;
        private readonly List<Item> _items;
        private readonly List<Discount> _discounts;

        public CheckoutTest()
        {
            _checkout = new Checkout();
            _items = new List<Item>();
            _discounts = new List<Discount>();
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
            _discounts.Add(new Discount { Sku = "B", Quantity = 3, Value = 40m });
            _items.Add(new Item { Sku = "B", Price = 15m });
            _items.Add(new Item { Sku = "B", Price = 15m });
            _items.Add(new Item { Sku = "B", Price = 15m });

            //Act
            _checkout.AddDiscounts(_discounts);
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
            Assert.Throws<NullReferenceException>(() => _checkout.AddDiscounts(null));
        }

        [Fact]
        public void GivenPercentageBasedDiscountIsAppliedCorrectTotalIsReturned()
        {
            //Arrange
            _discounts.Add(new Discount { Sku = "D", Quantity = 2, Value = null, Percentage = 25 });
            _items.Add(new Item { Sku = "D", Price = 55m });
            _items.Add(new Item { Sku = "D", Price = 55m });

            //Act
            _checkout.AddDiscounts(_discounts);
            _checkout.Scan(_items);

            var result = _checkout.Total();

            //Arrange
            Assert.Equal(27.5m, result);
        }
    }
}
