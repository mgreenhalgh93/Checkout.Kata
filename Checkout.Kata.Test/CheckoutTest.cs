using System;
using Xunit;

namespace Checkout.Kata.Test
{
    public class CheckoutTest
    {
        private readonly Checkout _checkout;

        public CheckoutTest()
        {
            _checkout = new Checkout();
        }

        [Fact]
        public void ItemScannedShowsInBasketTotal()
        {
            //Arrange
            var item = new Item();

            //Act
            _checkout.Scan(item);

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
            var item1 = new Item();
            var item2 = new Item();

            //Act
            _checkout.Scan(item1);
            _checkout.Scan(item2);

            var result = _checkout.Basket.Count;

            //Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void ItemAddedToBasketReturnsCorrectTotal()
        {
            //Arrange
            var item = new Item { Sku = "A", Price = 10m };

            //Act
            _checkout.Scan(item);

            var result = _checkout.Total();

            //Arrange
            Assert.Equal(10m, result);
        }
    }
}
