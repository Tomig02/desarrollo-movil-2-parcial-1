using parcial_1._Services;
using parcial_1_tests.Testing;

namespace parcial_1_tests.Unit;

public class DogApiServiceTests
{
    [Fact]
    public async Task GetBreedsAsync_MapsAndSortsBreeds()
    {
        var handler = new StubHttpMessageHandler(TestData.BreedsJson);
        using var client = handler.CreateClient();
        var service = new DogApiService(client);

        var breeds = await service.GetBreedsAsync();

        Assert.Equal(2, breeds.Count);
        Assert.Equal("Akita", breeds[0].Name);
        Assert.Equal("1", breeds[0].Id);
        Assert.Equal("Zebra Dog", breeds[1].Name);
        Assert.Equal("2", breeds[1].Id);
        Assert.Single(handler.Requests);
        Assert.Contains("page%5Bsize%5D=1000", handler.Requests[0].OriginalString);
    }

    [Fact]
    public async Task GetBreedByIdAsync_MapsDetails()
    {
        var handler = new StubHttpMessageHandler(TestData.BreedDetailJson);
        using var client = handler.CreateClient();
        var service = new DogApiService(client);

        var details = await service.GetBreedByIdAsync("akita");

        Assert.Equal("akita", details.Id);
        Assert.Equal("Akita", details.Name);
        Assert.Equal("A spirited dog.", details.Description);
        Assert.Equal(10, details.LifeMin);
        Assert.Equal(15, details.LifeMax);
        Assert.Equal(30, details.MaleWeightMin);
        Assert.Equal(40, details.FemaleWeightMax);
        Assert.Equal(60, details.MaleHeightMin);
        Assert.Equal(65, details.FemaleHeightMax);
        Assert.Equal(false, details.Hypoallergenic);
        Assert.Equal("Japan", details.OriginCountry);
        Assert.Equal("Asia", details.OriginRegion);
        Assert.EndsWith("/breeds/akita", handler.Requests[0].AbsolutePath);
    }
}
