using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace NetCoreWeather
{
	public class WeatherClient 
	{
		private readonly HttpClient httpClient;

		private readonly ServiceSettings settings;

		public WeatherClient(HttpClient httpClient, IOptions<ServiceSettings> options) 
		{
			this.httpClient = httpClient;
			settings = options.Value;
		}

		public record Weather(string description);
		public record Main(decimal temp);
		
		public record Forecast(Weather[] weather, Main main, long dt);

		// Creating a method task for getting the call
		public async Task<Forecast> GetCurrentWeatherAsync(string city) 
		{
			var forecast = await httpClient.GetFromJsonAsync<Forecast>($"https://{settings.OpenWeatherHost}/data/2.5/weather?q={city}&appid={settings.ApiKey}&units=metric");
			return forecast;
		}
	}
}