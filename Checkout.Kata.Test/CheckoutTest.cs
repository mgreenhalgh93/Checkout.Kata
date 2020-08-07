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
    }
}
