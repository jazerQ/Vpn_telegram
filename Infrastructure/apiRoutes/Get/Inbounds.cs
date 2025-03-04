using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Io;
using Core.Models;
using Infrastructure.VpnLibrary.Extensions;
using Newtonsoft.Json;
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
            var response = await _httpClient.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

        }
        public async Task GetInboundById(HttpHeaders headers, int inboundId) 
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, $"{StaticInfo.MainPath}/panel/api/inbounds/get/{inboundId}");
            await httpRequest.GetHeaders(headers);
            var response = await _httpClient.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
        public async Task GetInboundByEmail(HttpHeaders headers, string email) 
        {
            HttpRequestMessage request = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, $"{StaticInfo.MainPath}/panel/api/inbounds/getClientTraffics/{email}");
            await request.GetHeaders(headers);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
        public async Task GetInboundByUserId(HttpHeaders headers, string userId) 
        {
            HttpRequestMessage request = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, $"{StaticInfo.MainPath}/panel/api/inbounds/getClientTrafficsById/{userId}");
            await request.GetHeaders(headers);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
        public async Task AddToInbound(HttpHeaders headers, string username) 
        {
            
            //List<Client> clients = new List<Client>() { client };
            //Clients clients1 = new Clients { clients = clients };
            //string clientsJson = JsonConvert.SerializeObject(clients1);
            //Console.WriteLine(clientsJson);
            //AddClientToInboundModel addClientToInbound = new AddClientToInboundModel
            //{
            //    id = 1,
            //    settings = clientsJson
                
            //};
            //string secondJson = JsonConvert.SerializeObject(addClientToInbound);
            //Console.WriteLine("\n\n\n\n\n\n\n");
            //Console.WriteLine(secondJson);
            //string json = JsonConvert.SerializeObject(addClientToInbound);                                             ///
            //HttpRequestMessage request = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, $"{StaticInfo.MainPath}/panel/api/inbounds/addClient")
            //{
            //    Content = new StringContent(secondJson, Encoding.UTF8, "application/json")
            //};
            //await request.GetHeaders(headers);
            //HttpResponseMessage response = await _httpClient.SendAsync(request);
            //response.EnsureSuccessStatusCode();
            Console.WriteLine("Mock");
        
        }
                                                                                            
    }
}
