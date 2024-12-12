global using Xunit;
using System.Collections;
using FluentAssertions;
using Gifts.Adapters;
using Gifts.Domain;

namespace Gifts.Tests;

public class SantaTest
{
    private static readonly Toy Playstation = new("playstation");
    private static readonly Toy Ball = new("ball");
    private static readonly Toy Plush = new("plush");
    private readonly Santa _santa = new(new ChildrenRepository());

    [Theory]
    [ClassData(typeof(GiftDistributionTestData))]
    public void GivenNaughtyChildWhenDistributingGiftsThenChildReceivesThirdChoice(Behavior behavior, Toy toy)
    {
        var bobby = AChild("bobby", behavior);
        _santa.AddChild(bobby);

        var got = _santa.ChooseToyForChild("bobby");

        got.Should().Be(toy);
    }

    [Fact]
    public void GivenNonExistingChildWhenDistributingGiftsThenExceptionThrown()
    {
        var bobby = AChild("bobby", Behavior.VeryNice);
        _santa.AddChild(bobby);

        var chooseToyForChild = () => _santa.ChooseToyForChild("alice");
        chooseToyForChild.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("No such child found");
    }

    private static Child AChild(string name, Behavior behavior)
    {
        var bobby = new Child(name, behavior);
        bobby.SetWishList(Playstation, Plush, Ball);
        return bobby;
    }

    private class GiftDistributionTestData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data =
        [
            new object[] { Behavior.Naughty, Ball },
            new object[] { Behavior.Nice, Plush },
            new object[] { Behavior.VeryNice, Playstation }
        ];

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}