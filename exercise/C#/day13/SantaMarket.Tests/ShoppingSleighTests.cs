using Bogus;
using SantaMarket.Model;
using Xunit;

namespace SantaMarket.Tests;

public class ShoppingSleighTests
{
    [Fact]
    public void HandleOffXforYers_Should_Return_ADiscounted_Price()
    {
        // Instanciate a product with a Product Unit Each
        // Instanciate an offer with a quantity of 3 with a price of 100$
        // Assert that it returns a Discount of 100$
        var product = new Product(new Faker().Commerce.ProductName(), ProductUnit.Each);
        
        
        var offer = new Offer(SpecialOfferType.ThreeForTwo, product, new Faker().Random.Double());
    }
}