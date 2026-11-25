using parcial_1._Models;
using parcial_1._Services;
using parcial_1._ViewModels;
using parcial_1_tests.Testing;

namespace parcial_1_tests.Unit;

public class ViewListViewModelTests
{
    [Fact]
    public async Task LoadBreedsCommand_PopulatesBreeds()
    {
        using var client = new StubHttpMessageHandler(TestData.BreedsJson).CreateClient();
        var viewModel = CreateViewModel(client);

        await viewModel.LoadBreedsCommand.ExecuteAsync(null);

        Assert.Equal(2, viewModel.Breeds.Count);
        Assert.Equal("Akita", viewModel.Breeds[0].Name);
        Assert.Equal("Zebra Dog", viewModel.Breeds[1].Name);
        Assert.False(viewModel.IsBusy);
        Assert.Null(viewModel.ErrorMessage);
    }

    [Fact]
    public void SelectBreedCommand_SetsSelectedBreed()
    {
        using var client = new StubHttpMessageHandler(TestData.BreedsJson).CreateClient();
        var viewModel = CreateViewModel(client);
        var breed = new DogBreed { Id = "1", Name = "Akita" };

        viewModel.SelectBreedCommand.Execute(breed);

        Assert.Same(breed, viewModel.SelectedBreed);
    }

    [Fact]
    public void SearchCommand_WithoutSelection_SetsErrorMessage()
    {
        using var client = new StubHttpMessageHandler(TestData.BreedsJson).CreateClient();
        var viewModel = CreateViewModel(client);

        viewModel.SearchCommand.Execute(null);

        Assert.Equal("Primero seleccioná una raza.", viewModel.ErrorMessage);
    }

    private static ViewListViewModel CreateViewModel(HttpClient client) =>
        new(new DogApiService(client), new StubServiceProvider());
}
