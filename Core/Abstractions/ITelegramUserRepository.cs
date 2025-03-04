using Core.Entities;

namespace Core.Abstractions
{
    public interface ITelegramUserRepository
    {
        Task AddUser(TelegramUser user, CancellationToken cancellationToken);
        Task Update(long id, string name, string firstname, string lastname, string shortname, Guid vpnId, CancellationToken cancellationToken);
        Task<string> GetNameById(long id, CancellationToken cancellationToken);
        Task<Guid> GetVpnId(long id, CancellationToken cancellationToken);
    }
}