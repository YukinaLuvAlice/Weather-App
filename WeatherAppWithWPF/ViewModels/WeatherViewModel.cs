using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherAppWithWPF.Commands;
using WeatherAppWithWPF.Models;
using WeatherAppWithWPF.Services;

namespace WeatherAppWithWPF.ViewModels
{
    public class WeatherViewModel : BaseViewModel
    {
        private readonly IWeatherService _weatherService;
        private WeatherInfo _weatherInfo;
        private string _cityName;
        private bool _isLoading;
        private string _errorMessage;

        public WeatherViewModel(IWeatherService weatherService)
        {
            _weatherService = weatherService;
            GetWeatherCommand = new AsyncRelayCommand(ExecuteGetWeather, CanExecuteGetWeather);
        }

        public ICommand GetWeatherCommand { get; }

        public WeatherInfo WeatherInfo
        {
            get => _weatherInfo;
            set => SetProperty(ref _weatherInfo, value);
        }

        public string CityName
        {
            get => _cityName;
            set => SetProperty(ref _cityName, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        private bool CanExecuteGetWeather()
        {
            return !string.IsNullOrWhiteSpace(CityName) && !IsLoading;
        }

        private async Task ExecuteGetWeather()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;
                WeatherInfo = await _weatherService.GetWeatherAsync(CityName);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                WeatherInfo = null;
            }
            finally
            {
                IsLoading = false;
            }
        }
        }
}
