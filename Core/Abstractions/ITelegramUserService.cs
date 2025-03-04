
using Core.Entities;

namespace Core.Abstractions;

public interface ITelegramUserService
{
    Task<string> GetNameById(long id, CancellationToken cancellationToken);
    Task AddUser(TelegramUser user, CancellationToken cancellationToken);
    Task<Guid> GetVpnId(long id, CancellationToken cancellationToken);
}