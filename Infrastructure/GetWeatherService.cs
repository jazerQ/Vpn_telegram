using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class GetWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly GetLatLonData _latLonData;
        public GetWeatherService(IHttpClientFactory httpClientFactory, GetLatLonData latLonData)
        {
            _httpClientFactory = httpClientFactory;
            _latLonData = latLonData;
        }                                 //lat - широта    lon - долгота
        public async Task GetWeather(string city, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient();
            (string, string) latlon = await _latLonData.Get(city);
            
            httpClient.DefaultRequestHeaders.Add("X-Yandex-Weather-Key", Environment.GetEnvironmentVariable("YandexToken"));
            //httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "deflate, gzip");
            using HttpResponseMessage response = await httpClient.GetAsync($"https://api.weather.yandex.ru/v2/forecast?lat={latlon.Item1}&lon={latlon.Item2}", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync(cancellationToken);
                Console.WriteLine(content);
                Console.WriteLine(latlon);
            }


        }
    }
}
