using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Abstractions;
using Core.Entities;
using Core.Models;
using DataAccess;

namespace Application
{
    public class VpnClientService : IVpnClientService
    {
        private readonly IVpnClientRepository _repository;

        public VpnClientService(IVpnClientRepository repository)
        {
            _repository = repository;
        }
        public async Task WriteEntity(Client client, TelegramUser tgUser, bool isPrimaryUser, CancellationToken cancellationToken)
        {
            await _repository.WriteEntity(client, tgUser, isPrimaryUser, cancellationToken);
        }
        public async Task<string> GetConnectionString(long tgId, CancellationToken cancellationToken) 
        {
            try
            {
                return await _repository.GetConnectionString(tgId, cancellationToken);
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
        public async Task<bool> IsHaveVpn(long tgId, CancellationToken cancellationToken) 
        {
            return await _repository.IsHaveVpn(tgId, cancellationToken);
        
        }
        public async Task<TimeSpan> GetRemainderTime(long tgId, CancellationToken cancellationToken) 
        {
            return await _repository.GetRemainderTime(tgId, cancellationToken);

        }
    }
}
