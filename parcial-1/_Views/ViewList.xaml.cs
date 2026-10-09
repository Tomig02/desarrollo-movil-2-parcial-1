
using parcial_1._ViewModels;

namespace parcial_1._Views;

public partial class ViewList : ContentPage
{
    private readonly ViewListViewModel _viewModel;

    public ViewList(ViewListViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadBreedsCommand.ExecuteAsync(null);
    }
}