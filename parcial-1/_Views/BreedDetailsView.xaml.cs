using parcial_1._ViewModels;

namespace parcial_1._Views;

public partial class BreedDetailsView : ContentPage
{
    public BreedDetailsView(BreedDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}