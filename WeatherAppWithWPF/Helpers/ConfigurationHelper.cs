using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WeatherAppWithWPF.Helpers
{
    public static class ConfigurationHelper
    {
        public static void Initialize()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<App>()
                .Build();

            Constants.API_KEY = configuration["WeatherAPI:Key"];
            Constants.BASE_URL = configuration["WeatherAPI:BaseUrl"];
        }
    }
}
