using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Knapcode.TorSharp;

namespace VyMapsDataCollector.Utils
{
    public static class TorSharpProxyUtils
    {
        private static TorSharpSettings settings;

        public static TorSharpSettings Settings
        {
            get
            {
                if (settings == null)
                {
                    // configure
                    settings = new TorSharpSettings
                    {
                        ZippedToolsDirectory = Path.Combine(Path.GetTempPath(), "TorZipped"),
                        ExtractedToolsDirectory = Path.Combine(Path.GetTempPath(), "TorExtracted"),
                        PrivoxySettings = { Port = 1337 },
                        TorSettings =
                        {
                            SocksPort = 1338,
                            ControlPort = 1339,
                            ControlPassword = $"{Guid.NewGuid()}",
                        },
                    };
                }
                return settings;
            }
            set { settings = value; }
        }

        private static bool _isFetched = false;

        public static async Task<bool> IsUtilitiesFetched()
        {
            if (!_isFetched)
            {
                try
                {
                    // download tools
                    await new TorSharpToolFetcher(Settings, new HttpClient()).FetchAsync();
                    _isFetched = true;

                }
                catch (Exception e)
                {
                    _isFetched = false;
                    Logger.WriteLog("Tor İçin Gereklilikler İndirilemedi !!!");
                }
            }
            return _isFetched;
        }

        private static bool _isConfigured = false;
        public static async Task<TorSharpProxy> ConfigureAndStart(this TorSharpProxy proxy)
        {
            if (!_isConfigured)
            { 
                await proxy.ConfigureAndStartAsync();
                _isConfigured = true;
            }

            return proxy;
        }

        public static async Task<HttpClient> CreateNewHttpClientWithTorProxy(this HttpClient incoingClient)
        {
            await IsUtilitiesFetched();
            // execute
            var proxy = new TorSharpProxy(Settings);

            var handler = new HttpClientHandler
            {
                Proxy = new WebProxy(new Uri("http://localhost:" + Settings.PrivoxySettings.Port))
            };

            var httpClient = new HttpClient(handler);
            httpClient.Timeout = TimeSpan.FromSeconds(ProxyUtils.TimeOutSeconds);

            //await  proxy.ConfigureAndStartAsync();
            proxy = await proxy.ConfigureAndStart();

            bool isWorked = false;

            $"Tor Ağına Bağlanılıyor...".WriteLog();
            string ip = "";

            $"{ip} IP Adresi Hedef Site Üzerinde Test Ediliyor...".WriteLog();
            while (!isWorked)
            {
                try
                {
                    ip = await httpClient.GetStringAsync("http://api.ipify.org");
                    $"Tor Ağından Alınan IP: {ip}".WriteLog();

                    await httpClient.GetStringAsync("https://vymaps.com/");
                    $"{ip} IP Adresi Hedef Site Üzerinde Çalıştı".WriteLog();
                    isWorked = true;
                }
                catch (Exception e)
                {
                    $"{ip} IP Adresi Çalışmıyor Yeni IP Adresi Talep Ediliyor...".WriteLog();
                    await proxy.GetNewIdentityAsync();
                }

            }

            //Console.WriteLine(await httpClient.GetStringAsync("http://api.ipify.org"));
            //proxy.Stop();

            ProxyUtils.TorFirstRun = false;
            return httpClient;

        }
    }
}
