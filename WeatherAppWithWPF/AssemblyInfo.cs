using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WeatherAppWithWPF.Services;
using WeatherAppWithWPF.ViewModels;
using WeatherAppWithWPF.Views;

namespace WeatherAppWithWPF
{
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IWeatherService, WeatherService>();
            services.AddSingleton<WeatherViewModel>();
            services.AddSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.DataContext = serviceProvider.GetService<WeatherViewModel>();
            mainWindow.Show();
        }
    }
}