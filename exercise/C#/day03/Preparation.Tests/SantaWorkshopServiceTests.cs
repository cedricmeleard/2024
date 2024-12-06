using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;

namespace Preparation.Tests;

public record GiftData(string Name, double Weight, string Color, string Material);
public class SantaWorkshopServiceTests
{
    private const string RecommendedAge = "recommendedAge";
    private readonly SantaWorkshopService _service = new();
    
    private static Arbitrary<GiftData> ArbitraryGiftDatas
        => (from giftName in Arb.Generate<string>()
                from weight in Gen.Choose(1, 5)
                from color in Arb.Generate<string>()
                from material in Arb.Generate<string>()
                select new GiftData(giftName, weight, color, material))
            .ToArbitrary();
    
    
    [Property]
    public void PrepareGift_WithValidToy_ShouldInstantiateIt()
    {
        Prop.ForAll(
                ArbitraryGiftDatas,
                arb => _service
                    .PrepareGift(arb.Name, arb.Weight, arb.Color, arb.Material)
                    .Should()
                    .NotBeNull())
            .QuickCheckThrowOnFailure();
    }

    [Property]
    public void RetrieveAttributeOnGift(int attribute)
    {
        Prop.ForAll(
            ArbitraryGiftDatas,
            (data) =>
            {
                var gift = _service.PrepareGift(data.Name, data.Weight, data.Color, data.Material);
                gift.AddAttribute(RecommendedAge, attribute.ToString());
                gift.RecommendedAge()
                    .Should()
                    .Be(attribute);
            })
            .QuickCheckThrowOnFailure();
    }
    
    [Property]
    public void RetrieveAttributeOnGift_When_Attribute_IsNot_An_Int(string attribute)
    {
        Prop.ForAll(
                ArbitraryGiftDatas,
                (data) => (!int.TryParse(attribute, out _))
                    .Implies(() =>
                    {
                        var gift = _service.PrepareGift(data.Name, data.Weight, data.Color, data.Material);
                        gift.AddAttribute(RecommendedAge, attribute);
                        gift.RecommendedAge()
                            .Should()
                            .Be(0);    
                    })).QuickCheckThrowOnFailure();
    }
    
    [Property]
    public void FailsForATooHeavyGift()
    {
        var heavyWeights = Gen.Choose(51, 1000)
            .Select(x => x / 10.0)
            .ToArbitrary();

        Prop.ForAll(
                ArbitraryGiftDatas,
                heavyWeights,
                (data, weight) => {
                        var prepareGift = () 
                            => _service.PrepareGift(data.Name, weight, data.Color, data.Material);

                        prepareGift.Should()
                            .Throw<ArgumentException>()
                            .WithMessage("Gift is too heavy for Santa's sleigh");    
                    })
            .QuickCheckThrowOnFailure();
    }
}