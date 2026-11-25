using System.Net;
using parcial_1._Services;
using parcial_1._ViewModels;
using parcial_1_tests.Testing;

namespace parcial_1_tests.Integration;

public class BreedFlowIntegrationTests
{
    [Fact]
    public async Task LoadThenSelect_UsesApiDataForSelection()
    {
        var handler = new StubHttpMessageHandler(TestData.BreedsJson);
        using var client = handler.CreateClient();
        var viewModel = new ViewListViewModel(new DogApiService(client), new StubServiceProvider());

        await viewModel.LoadBreedsCommand.ExecuteAsync(null);
        viewModel.SelectBreedCommand.Execute(viewModel.Breeds[0]);

        Assert.Equal("Akita", viewModel.SelectedBreed?.Name);
        Assert.Equal("1", viewModel.SelectedBreed?.Id);
        Assert.Single(handler.Requests);
    }

    [Fact]
    public async Task DetailsViewModel_LoadsBreedFromApi()
    {
        var handler = new StubHttpMessageHandler(TestData.BreedDetailJson);
        using var client = handler.CreateClient();
        var viewModel = new BreedDetailsViewModel(new DogApiService(client));

        viewModel.BreedId = "akita";

        Assert.True(await AsyncHelpers.WaitForAsync(() => viewModel.BreedDetails is not null));
        Assert.Equal("akita", viewModel.BreedDetails!.Id);
        Assert.Equal("Japan", viewModel.BreedDetails.OriginCountry);
        Assert.EndsWith("/breeds/akita", handler.Requests[0].AbsolutePath);
    }

    [Fact]
    public async Task ApiFailure_SurfacesAsListErrorMessage()
    {
        using var client = new StubHttpMessageHandler(HttpStatusCode.InternalServerError).CreateClient();
        var viewModel = new ViewListViewModel(new DogApiService(client), new StubServiceProvider());

        await viewModel.LoadBreedsCommand.ExecuteAsync(null);

        Assert.Equal("No se pudieron cargar las razas. Revisá tu conexión.", viewModel.ErrorMessage);
        Assert.Empty(viewModel.Breeds);
        Assert.False(viewModel.IsBusy);
    }
}
