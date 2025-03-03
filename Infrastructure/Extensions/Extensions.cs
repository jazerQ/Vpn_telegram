using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VpnLibrary;

namespace Infrastructure.VpnLibrary.Extensions
{
    public static class Extensions
    {
        public static async Task GetHeaders(this HttpRequestMessage requestMessage,HttpHeaders headers)
        {
            foreach (var keyvalue in headers)
            {
                requestMessage.Headers.Append(keyvalue);
            }
        }
    }
}
