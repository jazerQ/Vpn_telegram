using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Io;
using Infrastructure.VpnLibrary.Extensions;
using VpnLibrary;
using VpnLibrary.apiRoutes.Post;

namespace Infrastructure.VpnLibrary.apiRoutes.Get
{
    public class Inbounds : IInbounds
    {
        private readonly HttpClient _httpClient;
        public Inbounds(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(StaticInfo.MainPath);


        }
        public async Task GetAllInbounds(HttpHeaders headers)
        {
            
            HttpRequestMessage httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, $"{StaticInfo.MainPath}/panel/api/inbounds/list");
            await httpRequest.GetHeaders(headers);
            httpRequest.Headers.Add("Accept", "application/json");
            var response = await _httpClient.SendAsync(httpRequest);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

        }

    }
}
