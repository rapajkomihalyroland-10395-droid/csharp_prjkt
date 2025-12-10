using Microsoft.Extensions.Logging;
using csharpPrjkt.Services;
using csharpPrjkt.ViewModels;
using csharpPrjkt.Views;

namespace csharpPrjkt
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
            
            // Services
            builder.Services.AddSingleton<DatabaseService>();

            // ViewModels
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<BookDetailsViewModel>();

            // Pages
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<BookDetailsPage>();

            return builder.Build();
        }
    }
}
