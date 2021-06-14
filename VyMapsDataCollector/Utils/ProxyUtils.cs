using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VyMapsDataCollector.Utils
{
    public static class ProxyUtils
    {
        public static bool UseProxy = true;
        public static bool UseTorNetworkAsProxy = true;
        public static bool ProxyFirstRun = true;
        public static bool TorFirstRun = true;

        public static string ProxyList = "";
        public static string ForcedProxy = "";

        public static int currentTryCount = 0;
        public static int maxTryCount = 10000;
        public static int LastWorkingProxy = 0;
        public static bool Worked = false;
        public static int TimeOutSeconds = 6;

        public static HttpClientHandler Handler { get; set; }

        public static async Task<HttpClient> CreateNewWorkingClientWithProxy(this HttpClient client)
        {
            while (!Worked)
            {
                string proxyUrl = "";

                if (!string.IsNullOrWhiteSpace(ForcedProxy))
                {
                    proxyUrl = ForcedProxy;
                    Worked = true;
                }
                else
                {
                    if (LastWorkingProxy != 0)
                    {
                        proxyUrl = ProxyList.Split('\n')[LastWorkingProxy].Trim();
                    }
                    else
                    {
                        proxyUrl = ProxyList.Split('\n')[currentTryCount].Trim();
                    }

                }
                // First create a proxy object
                var proxy = new WebProxy
                {
                    //Address = new Uri($"http://{proxyHost}:{proxyPort}"),
                    Address = new Uri($"http://{proxyUrl}"),
                    BypassProxyOnLocal = false,
                    UseDefaultCredentials = false,

                    //// *** These creds are given to the proxy server, not the web server ***
                    //Credentials = new NetworkCredential(
                    //    userName: proxyUserName,
                    //    password: proxyPassword)
                };

                // Now create a client handler which uses that proxy
                var httpClientHandler = new HttpClientHandler
                {
                    Proxy = proxy,
                };

                // Finally, create the HTTP client object
                client = new HttpClient(handler: httpClientHandler, disposeHandler: true);
                client.Timeout = TimeSpan.FromSeconds(TimeOutSeconds);
                //testTheProxy
                try
                {
                    var response = await client.GetStringAsync("https://vymaps.com/");

                    Logger.WriteLog($"{currentTryCount + 1}/{ProxyList.Split('\n').Length - 1} Proxy Çalıştı {proxy.Address}");
                    Logger.WriteLog($"{currentTryCount + 1}/{ProxyList.Split('\n').Length - 1} Son Çalışan Proxy: {currentTryCount} ({proxy.Address})");
                    Handler = httpClientHandler;
                    LastWorkingProxy = currentTryCount;
                    currentTryCount = 0;
                    Worked = true;
                }
                catch (Exception e)
                {
                    Logger.WriteLog($"{currentTryCount + 1}/{ProxyList.Split('\n').Length - 1} {proxy.Address} => Proxy Çalışmadı !");
                    Logger.WriteLog($"Message : {e.Message}");
                    if (e.InnerException != null) Logger.WriteLog($"InnerEx : {e.InnerException.Message}");

                    if (currentTryCount >= ProxyList.Split('\n').Length - 1 || currentTryCount >= maxTryCount)
                    {
                        Logger.WriteLog("Tüm Proxyleri Denedim Hiç Biri Çalışmadı !");
                        LastWorkingProxy = 0;
                        Worked = true;
                        client = new HttpClient();
                    }
                    else
                    {
                        Logger.WriteLog($"{currentTryCount + 1}/{ProxyList.Split('\n').Length - 1} Yeni Proxy Deniyorum");
                    }
                    currentTryCount++;
                    if (!string.IsNullOrWhiteSpace(ForcedProxy))
                    {
                        "Girdiğiniz Proxy Çalışmıyor Listeyi Deneyin !".WriteLog();
                        Worked = true;
                    }
                    else
                    {
                        Worked = false;
                    }
                }
            }

            return client;
        }
    }
}
