using System.Net;
using parcial_1._Services;
using parcial_1._ViewModels;
using parcial_1_tests.Testing;

namespace parcial_1_tests.Unit;

public class BreedDetailsViewModelTests
{
    [Fact]
    public async Task SettingBreedId_LoadsBreedDetails()
    {
        var handler = new StubHttpMessageHandler(TestData.BreedDetailJson);
        using var client = handler.CreateClient();
        var viewModel = new BreedDetailsViewModel(new DogApiService(client));

        viewModel.BreedId = "akita";

        Assert.True(await AsyncHelpers.WaitForAsync(() => viewModel.BreedDetails is not null));
        Assert.Equal("Akita", viewModel.BreedDetails!.Name);
        Assert.Equal(15, viewModel.BreedDetails.LifeMax);
        Assert.False(viewModel.IsBusy);
        Assert.Null(viewModel.ErrorMessage);
        Assert.EndsWith("/breeds/akita", handler.Requests[0].AbsolutePath);
    }

    [Fact]
    public async Task ApiFailure_SetsErrorMessage()
    {
        using var client = new StubHttpMessageHandler(HttpStatusCode.InternalServerError).CreateClient();
        var viewModel = new BreedDetailsViewModel(new DogApiService(client));

        viewModel.BreedId = "akita";

        Assert.True(await AsyncHelpers.WaitForAsync(() => viewModel.ErrorMessage is not null));
        Assert.Equal("No se pudieron cargar los detalles de la raza.", viewModel.ErrorMessage);
        Assert.False(viewModel.IsBusy);
    }
}
