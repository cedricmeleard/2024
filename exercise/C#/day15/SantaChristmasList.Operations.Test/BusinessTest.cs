namespace SantaChristmasList.Operations.Test;

public class BusinessTest
{
    private const string BarCode = "123";

    private readonly Factory _factory = new();
    private readonly Inventory _inventory = new();

    private readonly Child _john = new("John");
    private readonly ManufacturedGift _manufacturedGift = new(BarCode);
    private readonly Business _sut;
    private readonly Gift _toy = new("Toy");
    private readonly WishList _wishList = new();

    public BusinessTest()
    {
        _sut = new Business(_factory, _inventory, _wishList);
    }

    [Fact]
    public void Gift_ShouldBeLoadedIntoSleigh()
    {
        _wishList.Add(_john, _toy);
        _factory.Add(_toy, _manufacturedGift);
        _inventory.Add(BarCode, _toy);

        var sleigh = _sut.LoadGiftsInSleigh(_john);

        sleigh.GetGiftFor(_john)
            .Should()
            .Be($"Gift: {_toy.Name} has been loaded!");
    }

    public class Failures
    {
        private const string NotNiceThisYearError = "Missing gift: Child wasn't nice this year!";
        private const string GiftMisplacedByElvesMessage = "Missing gift: The gift has probably been misplaced by the elves!";
        private readonly Factory _factory = new();
        private readonly Inventory _inventory = new();

        private readonly Child _john = new("John");
        private readonly ManufacturedGift _manufacturedGift = new(BarCode);
        private readonly Business _sut;
        private readonly Gift _toy = new("Toy");
        private readonly WishList _wishList = new();

        public Failures()
        {
            _sut = new Business(_factory, _inventory, _wishList);
        }

        [Fact]
        public void Gift_ShouldNotBeLoaded_GivenChildIsNotOnWishList()
        {
            var sleigh = _sut.LoadGiftsInSleigh(_john);

            sleigh.GetGiftFor(_john)
                .Should()
                .Be(NotNiceThisYearError);
        }

        [Fact]
        public void Gift_ShouldNotBeLoaded_GivenToyWasNotManufactured()
        {
            _wishList.Add(_john, _toy);

            var sleigh = _sut.LoadGiftsInSleigh(_john);

            sleigh.GetGiftFor(_john)
                .Should()
                .Be(NotNiceThisYearError);
        }

        [Fact]
        public void Gift_ShouldNotBeLoaded_GivenToyWasMisplaced()
        {
            _wishList.Add(_john, _toy);
            _factory.Add(_toy, _manufacturedGift);

            var sleigh = _sut.LoadGiftsInSleigh(_john);

            sleigh.GetGiftFor(_john)
                .Should()
                .Be(GiftMisplacedByElvesMessage);
        }
    }
}