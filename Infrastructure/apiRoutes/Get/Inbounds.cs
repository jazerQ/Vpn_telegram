using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Io;
using Core;
using Core.Abstractions;
using Core.Entities;
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
        private readonly IVpnClientService _vpnClientService;
        public Inbounds(IHttpClientFactory httpClientFactory, IVpnClientService vpnClientService)
        {
            _httpClient = httpClientFactory.CreateClient(StaticInfo.MainPath);
            _vpnClientService = vpnClientService;

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
        public async Task AddSimpleToInbound(HttpHeaders headers, TelegramUser user, CancellationToken cancellationToken)
        {
            Client client = new Client
            {
                id = Guid.NewGuid(),
                email = user.Id.ToString(),
                expiryTime = ((DateTimeOffset)DateTime.Now.AddMinutes(30)).ToUnixTimeMilliseconds(),
                flow = "xtls-rprx-vision",
                enable = true,
                limitIp = 1,
                totalGB = 1073741824,
                subId = SubIdService.Generate()
            };
            
            AddClientToInboundModel addClientToInbound = AddClientToInboundModel.GetClient(client);
            await _vpnClientService.WriteEntity(client, user, false, cancellationToken);
            await AddToInbound(headers, addClientToInbound, cancellationToken);
        }
        public async Task AddPrimaryToInbound(HttpHeaders headers, TelegramUser user, CancellationToken cancellationToken) 
        {
            Client client = new Client
            {
                id = Guid.NewGuid(),
                email = user.Id.ToString(),
                flow = "xtls-rprx-vision",
                expiryTime = ((DateTimeOffset)DateTime.Now.AddDays(30)).ToUnixTimeMilliseconds(),
                enable = true,
                limitIp = 2,
                totalGB = 0,
                subId = SubIdService.Generate()
            };
            AddClientToInboundModel addClientToInbound = AddClientToInboundModel.GetClient(client);
            await _vpnClientService.WriteEntity(client, user, true, cancellationToken);
            await AddToInbound(headers, addClientToInbound, cancellationToken);
            
        }
        public async Task AddToInbound(HttpHeaders headers, AddClientToInboundModel client, CancellationToken cancellationToken)
        {
            string json = JsonConvert.SerializeObject(client);
            Console.WriteLine(json);
            HttpRequestMessage request = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, $"{StaticInfo.MainPath}/panel/api/inbounds/addClient")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            await request.GetHeaders(headers);
            HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync(cancellationToken));
        }
    }
}
