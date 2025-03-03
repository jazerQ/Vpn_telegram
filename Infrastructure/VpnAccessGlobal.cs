using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.VpnLibrary.apiRoutes.Get;
using Microsoft.AspNetCore.Http;
using VpnLibrary;
using VpnLibrary.apiRoutes.Post;

namespace Infrastructure.VpnLibrary
{
    public class VpnAccessGlobal
    {
        private readonly StaticInfo _staticInfo = new StaticInfo();
        private HttpHeaders _header;
        private readonly HttpClient _client;
        private readonly IInbounds _inbounds;
        public VpnAccessGlobal(IHttpClientFactory clientFactory, IInbounds inbounds)
        {
            _client = clientFactory.CreateClient(StaticInfo.MainPath);
            _inbounds = inbounds;
        }
        private async Task UpdateHeaders() 
        {
            Login login = new Login();
            _header = await login.GetHeaders(_client);
        }
        public async Task GetListInbounds() 
        {
            try
            {
                await _inbounds.GetAllInbounds(_header);
            }
            catch (NullReferenceException ex)
            {
                try
                {
                    await UpdateHeaders();
                    await _inbounds.GetAllInbounds(_header);
                }
                catch (Exception exep) 
                {
                    Console.WriteLine(exep.Message);
                    throw;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
