
using System.Net.Http.Headers;

namespace Infrastructure.VpnLibrary.apiRoutes.Get
{
    public interface IInbounds
    {
        Task GetAllInbounds(HttpHeaders headers);
        Task GetInboundById(HttpHeaders headers, int inboundId);
        Task GetInboundByEmail(HttpHeaders headers, string email);
        Task GetInboundByUserId(HttpHeaders headers, string userId);
        Task AddToInbound(HttpHeaders headers, string username);
    }
}