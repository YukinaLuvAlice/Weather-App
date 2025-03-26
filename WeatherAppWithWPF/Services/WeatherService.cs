using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;
using WeatherAppWithWPF.Helpers;
using WeatherAppWithWPF.Models;

namespace WeatherAppWithWPF.Services
{
    public class WeatherService : IWeatherService
    {
        public async Task<WeatherInfo> GetWeatherAsync(string city)
        {
            try
            {
                // Sử dụng API v2.5 vì nó ổn định hơn
                var client = new RestClient("https://api.openweathermap.org/data/2.5/weather");
                var request = new RestRequest();

                // Thêm parameters
                request.AddParameter("q", city);
                request.AddParameter("appid", Constants.API_KEY);
                request.AddParameter("units", "metric");  // Để lấy nhiệt độ theo độ C
                request.AddParameter("lang", "vi");       // Tiếng Việt

                var response = await client.ExecuteAsync(request);

                // Kiểm tra response
                if (!response.IsSuccessful)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        throw new Exception("Invalid API key. Please check your API key in Constants.cs");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        throw new Exception($"City '{city}' not found");
                    }
                    throw new Exception($"API Error: {response.ErrorMessage ?? response.StatusDescription}");
                }

                if (string.IsNullOrEmpty(response.Content))
                {
                    throw new Exception("Empty response from weather service");
                }

                var json = JObject.Parse(response.Content);

                // Kiểm tra xem có lỗi từ API không
                if (json["cod"] != null && json["cod"].ToString() != "200")
                {
                    throw new Exception(json["message"]?.ToString() ?? "Unknown error from weather service");
                }

                return new WeatherInfo
                {
                    Location = json["name"].ToString(),
                    Temperature = double.Parse(json["main"]["temp"].ToString()),
                    Description = json["weather"][0]["description"].ToString(),
                    Humidity = double.Parse(json["main"]["humidity"].ToString()),
                    WindSpeed = double.Parse(json["wind"]["speed"].ToString()),
                    Icon = json["weather"][0]["icon"].ToString(),
                    FeelsLike = double.Parse(json["main"]["feels_like"].ToString()),
                    Pressure = double.Parse(json["main"]["pressure"].ToString()),
                    Visibility = json.Value<int>("visibility") / 1000.0, // Convert to km
                };
            }
            catch (Exception ex)
            {
                // Log lỗi chi tiết để debug
                Console.WriteLine($"Error details: {ex}");

                // Trả về message lỗi thân thiện với người dùng
                if (ex.Message.Contains("API key"))
                {
                    throw new Exception("Invalid API key. Please check your configuration.");
                }
                if (ex.Message.Contains("not found"))
                {
                    throw new Exception($"Could not find weather data for '{city}'");
                }
                throw new Exception($"Could not get weather data: {ex.Message}");
            }
        }
    }
}