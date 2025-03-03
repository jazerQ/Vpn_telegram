using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VpnLibrary.apiRoutes.Post
{
    public class Login
    {
        public async Task<HttpHeaders> GetHeaders(HttpClient httpClient)
        {
            HttpRequestMessage requestMessagePost = new HttpRequestMessage(HttpMethod.Post, $"{StaticInfo.MainPath}/login");
            var collection = new List<KeyValuePair<string, string>>();
            collection.Add(new("username", StaticInfo.Username));
            collection.Add(new("password", StaticInfo.Password));
            var form = new FormUrlEncodedContent(collection);
            requestMessagePost.Content = form;
            var response = await httpClient.SendAsync(requestMessagePost);
            response.EnsureSuccessStatusCode();
            return response.Headers;
        }
    }
}
