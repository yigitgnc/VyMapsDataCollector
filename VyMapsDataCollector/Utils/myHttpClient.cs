using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VyMapsDataCollector.Utils
{
    public class myHttpClient
    {
        public HttpClient Client;
        public myHttpClient()
        {
            Client = new HttpClient();
        }
        public myHttpClient(HttpClientHandler handler, bool disposeHandler)
        {
            Client = new HttpClient(handler, disposeHandler);
        }

        Random rnd = new Random();

        public async Task<string> myGetStringAsync(string requestUri)
        {
            string testStr = "Hatalı !";

            if (ProxyUtils.UseTorNetworkAsProxy)
            {
                try
                {
                    while (ProxyUtils.TorFirstRun)
                    {
                        await Task.Delay(1000);
                    }

                    testStr = await Client.GetStringAsync(requestUri);
                }
                catch
                {
                    //random bir süre bekle
                    //await Task.Delay(rnd.Next(1000, 10000));
                    Client = await Client.CreateNewHttpClientWithTorProxy();
                    //random bir süre bekle
                    //await Task.Delay(rnd.Next(1000, 10000));
                    testStr = await this.myGetStringAsync(requestUri);
                }
            }
            else if (ProxyUtils.UseProxy)
            {
                try
                {
                    Client = await Client.CreateNewWorkingClientWithProxy();
                    testStr = await Client.GetStringAsync(requestUri);
                }
                catch
                {
                    Client = await Client.CreateNewWorkingClientWithProxy();
                    testStr = await this.myGetStringAsync(requestUri);

                }
            }
            else
            {
                try
                {
                    testStr = await Client.GetStringAsync(requestUri);

                }
                catch (Exception e)
                {
                    Logger.WriteLog("İstek Atılrıken Hata ! Proxy Kullanmayı Dene.");
                }
            }

            return testStr;
        }
    }
}
