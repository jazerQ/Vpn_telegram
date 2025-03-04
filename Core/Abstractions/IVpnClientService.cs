using Core.Entities;
using Core.Models;

namespace Core.Abstractions
{
    public interface IVpnClientService
    {
        Task WriteEntity(Client client, TelegramUser tgUser, bool isPrimaryUser, CancellationToken cancellationToken);
        Task<string> GetConnectionString(long tgId, CancellationToken cancellationToken);
        Task<bool> IsHaveVpn(long tgId, CancellationToken cancellationToken);
        Task<TimeSpan> GetRemainderTime(long tgId, CancellationToken cancellationToken);
    }
}