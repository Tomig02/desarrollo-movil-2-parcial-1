using parcial_1._Views;
using parcial_1._ViewModels;

namespace parcial_1;

public partial class AppShell : Shell
{
    public AppShell(IServiceProvider services)
    {
        InitializeComponent();

        var content = new ShellContent
        {
            Title = "Razas",
            Route = "MainPage",
            ContentTemplate = new DataTemplate(() =>
            {
                var view = services.GetRequiredService<ViewList>();

                if (view.BindingContext is ViewListViewModel vm)
                    vm.LoadBreedsCommand.Execute(null);

                return view;
            })
        };

        Routing.RegisterRoute(
            nameof(BreedDetailsView),
            typeof(BreedDetailsView));

        Items.Add(new FlyoutItem
        {
            Title = "Razas",
            Items = { content }
        });
    }
}