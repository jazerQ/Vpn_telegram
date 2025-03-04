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
        public async Task GetInboundById(int inboundId) 
        {
            try
            {
                await _inbounds.GetInboundById(_header, inboundId);
            }
            catch (NullReferenceException ex)
            {
                try
                {
                    await UpdateHeaders();
                    await _inbounds.GetInboundById(_header, inboundId);
                }
                catch (Exception exep)
                {
                    Console.WriteLine(exep.Message);
                    throw;
                }
            }
            catch (BadHttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task GetInboundByEmail(string email) 
        {
            try
            {
                await _inbounds.GetInboundByEmail(_header, email);
            }
            catch (NullReferenceException ex)
            {
                try
                {
                    await UpdateHeaders();
                    await _inbounds.GetInboundByEmail(_header, email);
                }
                catch (Exception exep)
                {
                    Console.WriteLine(exep.Message);
                    throw;
                }
            }
            catch (BadHttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task GetInboundByUserId(string id) 
        {
            try
            {
                await _inbounds.GetInboundByUserId(_header, id);
            }
            catch (NullReferenceException ex)
            {
                try
                {
                    await UpdateHeaders();
                    await _inbounds.GetInboundByUserId(_header, id);
                }
                catch (Exception exep)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            catch (BadHttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task AddUserToInbound(string key) 
        {
            try
            {
                await _inbounds.AddToInbound(_header, key);
            }
            catch (NullReferenceException ex)
            {
                try
                {
                    await UpdateHeaders();
                    await _inbounds.AddToInbound(_header, key);
                }
                catch (Exception exep)
                {
                    Console.WriteLine(exep.Message);
                    throw;
                }
            }
            catch (BadHttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            
            }
        }
    }
}

