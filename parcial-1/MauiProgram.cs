using Microsoft.Extensions.Logging;
using parcial_1._Services;
using parcial_1._ViewModels;
using parcial_1._Views;

// Dentro de CreateMauiApp(), antes de Build():


namespace parcial_1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
               
#if DEBUG   
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<DogApiService>();
            builder.Services.AddTransient<ViewListViewModel>();
            builder.Services.AddTransient<ViewList>();
            builder.Services.AddTransient<BreedDetailsViewModel>();
            builder.Services.AddTransient<BreedDetailsView>();
            return builder.Build();
        }
    }
}
