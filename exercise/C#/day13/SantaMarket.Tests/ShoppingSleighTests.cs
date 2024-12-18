using Bogus;
using FluentAssertions;
using SantaMarket.Model;
using Xunit;

namespace SantaMarket.Tests;

public class ShoppingSleighTests
{
    [Fact]
    public void HandleOffXforYers_Should_Return_ADiscounted_Price()
    {
        // Arrange
        var faker = new Faker().Commerce;
        var product = new Product(faker.ProductName(), ProductUnit.Each);
        var offer = new Offer(SpecialOfferType.ThreeForTwo, product, faker.Random.Double());
        
        var expectedDiscount = new Discount(product, "3 for 2", -100);
        
        // Act
        var discount = ShoppingSleigh.HandleOffXforYers(offer, 3, 100, product, 3);

        // Assert
        discount.Should().Be(expectedDiscount);
    }
}