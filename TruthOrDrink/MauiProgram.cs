using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TruthOrDrink.Data;
using TruthOrDrink.Models;
using TruthOrDrink.Views;
using ZXing.Net.Maui.Controls;

namespace TruthOrDrink
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseBarcodeReader()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<TruthOrDrinkDatabase>();
            builder.Services.AddSingleton<Game>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<DataGenerator>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
