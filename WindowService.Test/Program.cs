using IdentityModel.Client;
using L.PathogenCore;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WindowService.Test
{
    class Program
    {
        static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            //创建请求对象
            var request = InfectionManager.CreateRequest(new InfectionConfig() { Url = "https://www.biquge.cc/html/9/9378/18114748.html" });
            //获取请求响应
            var pagePathogen = InfectionManager.GetResponse(request);
            Console.WriteLine(pagePathogen.PageSource);
            Console.ReadKey();
            //
            //var disco = await DiscoveryClient.GetAsync("http://localhost:8889");
            //var tokenClient = new TokenClient(disco.TokenEndpoint, "PathogenWindowService", "secret");
            //var tokenResponse = await tokenClient.RequestClientCredentialsAsync("aapi1");

            //var client = new HttpClient();

            //client.SetBearerToken(tokenResponse.AccessToken);
            //var respone = await client.GetAsync("http://localhost:8888/api/Chrome");
            //if (!respone.IsSuccessStatusCode)
            //{
            //    Console.WriteLine(respone.StatusCode);
            //}
            //else
            //{
            //    var content = await respone.Content.ReadAsStringAsync();
            //    Console.WriteLine(JArray.Parse(content));
            //}

            // request token
            //var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
            //var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "aapi1");

            //if (tokenResponse.IsError)
            //{
            //    Console.WriteLine(tokenResponse.Error);
            //    return;
            //}
        }
    }
}
