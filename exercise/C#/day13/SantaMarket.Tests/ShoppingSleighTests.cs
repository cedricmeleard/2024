using Bogus;
using SantaMarket.Model;
using Xunit;

namespace SantaMarket.Tests;

public class ShoppingSleighTests
{
    [Fact]
    public void HandleOffXforYers_Should_Return_ADiscounted_Price()
    {
        // Instanciate an offer with a quantity of 3 with a price of 100$
        // Instanciate a product with a quantity of 3
        // Assert that it returns a Discount of 100$
        var product = new Product(new Faker().Commerce.ProductName(), ProductUnit.Each);
        
        
    }
}