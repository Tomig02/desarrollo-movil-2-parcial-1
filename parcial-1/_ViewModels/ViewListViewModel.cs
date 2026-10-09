using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using parcial_1._Models;
using parcial_1._Services;
using parcial_1._Views;
using System.Collections.ObjectModel;

namespace parcial_1._ViewModels;

public partial class ViewListViewModel : ObservableObject
{
    private readonly DogApiService _dogApiService;
    public ObservableCollection<DogBreed> Breeds { get; } = [];

    [ObservableProperty] private bool isBusy;
    [ObservableProperty] private string? errorMessage;
    [ObservableProperty] private DogBreed? selectedBreed;

    private readonly IServiceProvider _services;

    public ViewListViewModel( DogApiService dogApiService, IServiceProvider services)
    {
        _dogApiService = dogApiService;
        _services = services;
    }

    [RelayCommand]
    private async Task LoadBreedsAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            ErrorMessage = null;

            var breeds = await _dogApiService.GetBreedsAsync();

            Breeds.Clear();

            foreach (var breed in breeds)
                Breeds.Add(breed);
        }
        catch (HttpRequestException)
        {
            ErrorMessage = "No se pudieron cargar las razas. Revisá tu conexión.";
        }
        catch (TaskCanceledException)
        {
            ErrorMessage = "La solicitud tardó demasiado. Intentá nuevamente.";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void SelectBreed(DogBreed? breed)
    {
        if (breed is not null)
            SelectedBreed = breed;
    }

    [RelayCommand]
    private async Task Search()
    {
        if (SelectedBreed is null)
        {
            ErrorMessage = "Primero seleccioná una raza.";
            return;
        }

        ErrorMessage = null;

        await Shell.Current.GoToAsync(
            $"{nameof(BreedDetailsView)}?breedId={Uri.EscapeDataString(SelectedBreed.Id)}");
    }
}