using Microsoft.Extensions.Logging;
using SmartHomeMonitor.Services;
using SmartHomeMonitor.Models;

namespace SmartHomeMonitor;

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
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		// Register Repository Services as Singletons
		builder.Services.AddSingleton<IRepository<SensorReading>, SensorReadingRepository>();
		builder.Services.AddSingleton<IRepository<Models.Device>, DeviceRepository>();

		return builder.Build();
	}
}
