namespace WeatherAppWithWPF.Models
{
    public class WeatherInfo
    {
        public string Location { get; set; }
        public double Temperature { get; set; }
        public string Description { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public string Icon { get; set; }
        // Thêm các properties mới
        public double FeelsLike { get; set; }
        public double Pressure { get; set; }
        public double Visibility { get; set; }
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }
    }
}