using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAppWithWPF.Models;

namespace WeatherAppWithWPF.Services
{
    public interface IWeatherService
    {
        Task<WeatherInfo> GetWeatherAsync(string city);  // Sửa tên method
    }
}
