
using System.Net.Http.Headers;
using Core.Entities;
using Core.Models;

namespace Infrastructure.VpnLibrary.apiRoutes.Get
{
    public interface IInbounds
    {
        Task GetAllInbounds(HttpHeaders headers);
        Task GetInboundById(HttpHeaders headers, int inboundId);
        Task GetInboundByEmail(HttpHeaders headers, string email);
        Task GetInboundByUserId(HttpHeaders headers, string userId);
        Task AddToInbound(HttpHeaders headers, AddClientToInboundModel client, CancellationToken cancellationToken);
        Task AddPrimaryToInbound(HttpHeaders headers, TelegramUser user, CancellationToken cancellationToken);
        Task AddSimpleToInbound(HttpHeaders headers, TelegramUser user, CancellationToken cancellationToken);
        Task UpdateIntoInbound(HttpHeaders headers, AddClientToInboundModel client, Guid clientId, CancellationToken cancellationToken);
        Task UpdateClientIntoInbound(HttpHeaders headers, TelegramUser user, CancellationToken cancellationToken);
    }
}