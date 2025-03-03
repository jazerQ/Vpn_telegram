
using System.Net.Http.Headers;

namespace Infrastructure.VpnLibrary.apiRoutes.Get
{
    public interface IInbounds
    {
        Task GetAllInbounds(HttpHeaders headers);
    }
}