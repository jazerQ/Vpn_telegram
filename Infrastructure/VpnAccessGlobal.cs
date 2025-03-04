using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using Infrastructure.VpnLibrary.apiRoutes.Get;
using Microsoft.AspNetCore.Http;
using Telegram.Bot.Types;
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
        private readonly IVpnClientService _vpnService;
        public VpnAccessGlobal(IHttpClientFactory clientFactory, IInbounds inbounds, IVpnClientService vpnClientService)
        {
            _client = clientFactory.CreateClient(StaticInfo.MainPath);
            _inbounds = inbounds;
            _vpnService = vpnClientService;
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
        public async Task AddSimpleUserToInbound(TelegramUser user, CancellationToken cancellationToken)
        {
            try
            {
               
                if (await _vpnService.IsHaveVpn(user.Id, cancellationToken)) throw new AlreadyHaveException("у вас уже есть строка подключения");
                
                await _inbounds.AddSimpleToInbound(_header, user, cancellationToken);
            }
            catch (AlreadyHaveException ex) 
            {
                var time = await GetRemainderTime(user.Id, cancellationToken);
                if (time.Nanoseconds < -1)
                {
                    throw new VpnTimeIsOverException("ваша пробная подписка закончилась, продлите ее купив полную");
                }
                throw;
                
            }
            catch (NullReferenceException ex)
            {
                try
                {
                    await UpdateHeaders();
                    await _inbounds.AddSimpleToInbound(_header, user, cancellationToken);
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
        public async Task AddPrimaryToInbound(TelegramUser key, CancellationToken cancellationToken)
        {
            try
            {
                if (await _vpnService.IsHaveVpn(key.Id, cancellationToken)) throw new AlreadyHaveException("у вас уже есть строка подключения");

                await _inbounds.AddPrimaryToInbound(_header, key, cancellationToken);
            }
            catch (AlreadyHaveException ex)
            {
                throw;

            }
            catch (NullReferenceException ex)
            {
                try
                {
                    await UpdateHeaders();
                    await _inbounds.AddPrimaryToInbound(_header, key, cancellationToken);
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
        public async Task<string> GetConnectionString(long tgId, CancellationToken cancellationToken)
        {
            try
            {
                return await _vpnService.GetConnectionString(tgId, cancellationToken);
            }
            catch (SqlNullValueException ex)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<TimeSpan> GetRemainderTime(long tgId, CancellationToken cancellationToken) 
        {
            try 
            {
                return await _vpnService.GetRemainderTime(tgId, cancellationToken);   
            }
            catch (SqlNullValueException ex)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

