using CommunityToolkit.Mvvm.ComponentModel;
using parcial_1._Models;
using parcial_1._Services;
using System.Windows.Input;

namespace parcial_1._ViewModels;

[QueryProperty(nameof(BreedId), "breedId")]
public partial class BreedDetailsViewModel : ObservableObject
{
    private readonly DogApiService _dogApiService;

    [ObservableProperty]
    private string breedId = string.Empty;

    [ObservableProperty]
    private DogBreedDetails? breedDetails;

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string? errorMessage;

    public BreedDetailsViewModel(DogApiService dogApiService)
    {
        _dogApiService = dogApiService;
    }

    partial void OnBreedIdChanged(string value)
    {
        _ = LoadDetailsAsync(value);
    }

    private async Task LoadDetailsAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return;

        try
        {
            IsBusy = true;
            ErrorMessage = null;

            BreedDetails = await _dogApiService.GetBreedByIdAsync(id);
        }
        catch (Exception)
        {
            ErrorMessage = "No se pudieron cargar los detalles de la raza.";
        }
        finally
        {
            IsBusy = false;
        }
    }
}