using System;
using Xunit;

namespace Checkout.Kata.Test
{
    public class CheckoutTest
    {
        [Fact]
        public void ItemScannedShowsInBasketTotal()
        {
            //Arrange
            var checkout = new Checkout();
            var item = new Item();

            //Act
            checkout.Scan(item);

            var result = checkout.Basket.Count;

            //Assert
            Assert.Equal(1, result);
        }
    }
}
