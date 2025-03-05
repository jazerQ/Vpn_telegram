using Core.Entities;
using Core.Models;

namespace Core.Abstractions
{
    public interface IVpnClientRepository
    {
        Task<VpnClient> GetEntityById(long telegramId, CancellationToken cancellationToken);
        Task WriteEntity(Client client, TelegramUser tgUser, bool isPrimaryUser, CancellationToken cancellationToken);
        Task<string> GetConnectionString(long tgId, CancellationToken cancellationToken);
        Task<bool> IsHaveVpn(long tgId, CancellationToken cancellationToken);
        Task<TimeSpan> GetRemainderTime(long tgId, CancellationToken cancellationToken);
        Task UpdateEntity(Client client, TelegramUser tgUser, bool isPrimaryUser, CancellationToken cancellationToken);
    }
}