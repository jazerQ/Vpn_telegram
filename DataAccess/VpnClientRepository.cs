using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Core.Abstractions;
using Core.Entities;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class VpnClientRepository : IVpnClientRepository
    {
        private readonly TelegramDbContext _context;
        private readonly ITelegramUserRepository _tgRepository;
        public VpnClientRepository(TelegramDbContext context, ITelegramUserRepository telegramUserRepository)
        {
            _context = context;
            _tgRepository = telegramUserRepository;
        }
        public async Task<VpnClient> GetEntityById(string telegramId, CancellationToken cancellationToken)
        {
            try
            {
                var vpn = _context.VpnClient.FirstOrDefault(vpn => vpn.telegramId.ToString() == telegramId) ?? throw new NullReferenceException("не нашел");
                return vpn;
            }
            catch (NullReferenceException ex)
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
        public async Task WriteEntity(Client client, TelegramUser tgUser, bool isPrimaryUser, CancellationToken cancellationToken)
        {
            
            tgUser.VpnClientId = client.id;

            var VpnClientEntity = new VpnClient
            {
                enable = client.enable,
                expiryTime = client.expiryTime,
                flow = client.flow,
                totalGB = client.totalGB,
                id = client.id,
                isPrimaryUser = isPrimaryUser,
                limitIp = client.limitIp,
                reset = client.reset,
                subId = client.subId,
                telegramId = long.Parse(client.email),
                tgId = "",
                
                ConnectionString = $"vless://{client.id}@150.241.67.48:443?type=tcp&security=reality&pbk=93IW1CnDZgrPxCGpnvpE2Eh8lsnrBVGZ3azk8hYurB0&fp=chrome&sni=tvdvdstore.com&sid=4ca5bc1e&spx=%2F&flow={client.flow}#RusLan-{client.email}"
            };
            await _tgRepository.AddUser(tgUser, cancellationToken);
            await _context.VpnClient.AddAsync(VpnClientEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<string> GetConnectionString(long tgId, CancellationToken cancellationToken)
        {
            try
            {
                var connectionString = await _context.VpnClient.Where(vpn => vpn.telegramId == tgId)
                                                               .Select(vpn => vpn.ConnectionString)
                                                               .SingleOrDefaultAsync(cancellationToken);
                if (connectionString == null) throw new SqlNullValueException("не нашел твою строку подключения");
                return connectionString;

            }
            catch (SqlNullValueException ex)
            {
                throw;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task<bool> IsHaveVpn(long tgId, CancellationToken cancellationToken) 
        {
            try 
            {
                var vpnClient = await _context.VpnClient.Where(vpn => vpn.telegramId == tgId).FirstOrDefaultAsync(cancellationToken);
                return vpnClient != null;
            }catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            
        }
        public async Task<TimeSpan> GetRemainderTime(long tgId, CancellationToken cancellationToken) 
        {
            var expiryTime = await _context.VpnClient.Where(vpn => vpn.telegramId == tgId).Select(vpn => vpn.expiryTime).SingleOrDefaultAsync(cancellationToken);
            DateTime expiryDateTime = DateTimeOffset.FromUnixTimeMilliseconds(expiryTime).DateTime;
            return expiryDateTime - DateTime.Now;
        }
    }


}
