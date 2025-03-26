using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAppWithWPF.Helpers
{
    public static class Constants
    {
        static Constants()
        {
            // Khởi tạo ConfigurationHelper
           ConfigurationHelper.Initialize();
        }

        public static string API_KEY { get; set; } = string.Empty;
        public static string BASE_URL { get; set; } = string.Empty;
    }
}