using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VyMapsDataCollector.Models;
//using HtmlAgilityPack;

namespace VyMapsDataCollector.Utils
{
    public static class Bll
    {
        public static bool BypassCountries = false;
        public static bool BypassDistricts = false;
        public static bool BypassSectors = false;
        public static bool BypassCompanies = false;

        public static bool ResumeFromLast = true;

        private static Setting currentSetting;

        public static Setting CurrentSetting
        {
            get
            {
                if (currentSetting == null)
                {
                    vymapsDBEntities db = new vymapsDBEntities();
                    var settingFromDb = db.Settings.FirstOrDefault();
                    if (db.Settings.FirstOrDefault() == null)
                    {
                        Setting settingToAdd = new Setting();
                        db.Settings.Add(settingToAdd);
                        db.SaveChanges();
                        //call Again !
                        currentSetting = CurrentSetting;
                    }
                    else
                    {
                        currentSetting = settingFromDb;
                    }
                }
                return currentSetting;
            }
            set { currentSetting = value; }
        }


        public static myHttpClient myClient = new myHttpClient()
        {
            //BaseAddress = new Uri("https://vymaps.com/")
        };


        public static int ParallelThreadLimit = 1;
        public static int CurrentReqCount = 0;

        public static async Task<bool> BeginDataCollect()
        {
            if (ProxyUtils.TorFirstRun)
            {
                "Anlaşılan Tor İlk Sefer İçin Henüz Hazır Değil Lütfen Bekleyin...".WriteLog();
                myClient.Client = await myClient.Client.CreateNewHttpClientWithTorProxy();
                "Tor Hazırlandı".WriteLog();
            }



            CurrentReqCount = 0;
            vymapsDBEntities db = new vymapsDBEntities();

            //ilk önce ülke kaydını çekelim
            "Veri Toplama İşlemi Başlatıldı.".WriteLog();
            //string responseBody = "";

            //Ülke Çekelim
            await TaskUtil.StartSTATask(() =>
            {
                if (!BypassCountries)
                {
                    string responseBody = "";
                    "Önce Ülke Kayıtları Çekiliyor...".WriteLog();

                    Task innerTask = Task.Run(async () =>
                    {
                        responseBody = await myClient.myGetStringAsync("https://vymaps.com/");
                    });

                    while (!innerTask.IsCompleted)
                    {
                        Thread.Sleep(100);
                        //Task.Delay(100);
                    }

                    Logger.WriteLog("Ülkeler Geldi.");
                    GetCountriesFromDocument(responseBody.StringToWebBrowserDocument());
                }
                else
                {
                    "Ülkeler ByPass Edildi.".WriteLog();
                }
            });

            //Ülkelere Ait Bölgeleri Çekelim
            await TaskUtil.StartSTATask(() =>
            {
                if (!BypassDistricts)
                {
                    List<Country> countries;
                    if (ResumeFromLast)
                    {
                        countries = db.Countries.Where(x => x.Name.Contains(currentSetting.LastCountryName)).ToList();
                        Logger.WriteLog($"{countries.FirstOrDefault().Name} Ülkesi İçin Bölgeler Çekiliyor");
                    }
                    else
                    {
                        Logger.WriteLog("Her Ülke İçin Bölgeler Çekiliyor");
                        countries = db.Countries.ToList();
                    }

                    int lastIteration = 0;

                    for (int i = lastIteration; i < countries.Count(); i++)
                    //for (int i = lastIteration; i < 1; i++)
                    {
                        //while (currentReqCount > parallelLimit)
                        //{
                        //    Task.Delay(100);
                        //}
                        try
                        {

                            var i1 = i;
                            //Task a = TaskUtil.StartSTATask(() =>
                            //{
                            CurrentReqCount++;
                            var country = countries[i1];

                            string responStringAsync = "";

                            Task innerTask = Task.Run(async () =>
                            {
                                $"{country.Name} Ülkesinin Bölgeleri İçin Veri Bekleniyor...".WriteLog();
                                responStringAsync = await myClient.myGetStringAsync(country.Url);
                                $"{country.Name} İçin Bölgeler Geldi".WriteLog();
                            });
                            if (innerTask.IsFaulted)
                            {

                            }

                            while (!innerTask.IsCompleted)
                            {
                                //Task.Delay(100);
                                Thread.Sleep(100);
                            }


                            HtmlDocument doc = responStringAsync.StringToWebBrowserDocument();

                            GetDistrictsFromDocument(doc, country);


                            // Task dbSave = Task.Run(async () =>
                            //{
                            //    try
                            //    {
                            CurrentSetting.LastCountryName = country.Name;
                            db.Entry(CurrentSetting).State = EntityState.Modified;
                            db.SaveChanges();

                            //    }
                            //    catch (Exception e)
                            //    {
                            //        Console.WriteLine(e);
                            //    }

                            //});
                            // while (!dbSave.IsCompleted)
                            // {
                            //     Task.Delay(100);
                            // }

                            $"{country.Name} Bölgeleri Okunan Son Ülke Olarak Kaydedildi".WriteLog();
                            CurrentReqCount--;

                            //}
                            //);
                            //if (a.IsFaulted)
                            //{
                            //    var ex = a.Exception;
                            //    currentReqCount--;
                            //}

                            //if (a.IsCompleted)
                            //{
                            //    currentReqCount--;
                            //}
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            CurrentReqCount--;
                            //throw;
                        }

                    }
                }
                else
                {
                    "Ülkeler İçin Bölgeler ByPass Edildi.".WriteLog();
                }
            });

            //Bölgelere Ait Sektörleri Çekelim
            await TaskUtil.StartSTATask(() =>
            {
                if (!BypassSectors)
                {
                    List<Country> countries = db.Countries.ToList();
                    List<District> districts = new List<District>();
                    if (ResumeFromLast)
                    {
                        countries = db.Countries.Where(x => x.Name.Contains(currentSetting.LastCountryName)).ToList();
                        Logger.WriteLog($"{countries.FirstOrDefault()?.Name} Ülkesinin Bölgeleri İçin ");

                        foreach (Country country in countries)
                        {
                            districts.AddRange(country.Districts);
                        }

                    }
                    else
                    {
                        Logger.WriteLog("Her Bölge İçin Sektörler Çekiliyor");

                    }

                    int lastIteration = 0;

                    for (int i = lastIteration; i < districts.Count(); i++)
                    {
                        var district = districts[i];
                        $"{district.Country.Name} / {district.Name} Bölgesinin Sektörleri İçin Veri Bekleniyor...".WriteLog();


                        string responseBody = "";

                        Task innerTask = TaskUtil.StartSTATask(async () =>
                        {
                            responseBody = await myClient.myGetStringAsync(district.Url);
                        });

                        while (!innerTask.IsCompleted || string.IsNullOrWhiteSpace(responseBody))
                        {
                            //Task.Delay(100);
                            Thread.Sleep(100);
                        }

                        $"{district.Country.Name} / {district.Name} İçin Sektrler Geldi".WriteLog();

                        GetSectorsFromDocument(responseBody.StringToWebBrowserDocument(), district);

                        CurrentSetting.LastDistrictName = district.Name;
                        db.Entry(currentSetting).State = EntityState.Modified;
                        db.SaveChanges();

                        $"Sektörler İçin {district.Country.Name} / {district.Name} Son Okunan Bölge Olarak Kaydedildi".WriteLog();
                    }
                }
                else
                {
                    "Bölgeler İçin Sektörler ByPass Edildi.".WriteLog();
                }
            });

            List<Task> taskList = new List<Task>();

            //Sektörlere Ait Şirkelteri Çekelim...
            Task t = TaskUtil.StartSTATask(async () =>
            {

                if (!BypassCompanies)
                {

                    List<Country> countries = new List<Country>();
                    List<District> districts = new List<District>();
                    List<District> nihai_districts = new List<District>();
                    List<Sector> sectors = new List<Sector>();
                    List<Sector> nihai_sectors = new List<Sector>();
                    if (ResumeFromLast)
                    {
                        countries = db.Countries.Where(x => x.Name.Contains(currentSetting.LastCountryName)).ToList();

                        Logger.WriteLog("Kalınan Yerden Tüm Sektorler İçin Firmalar Çekiliyor");

                        foreach (Country country in countries)
                        {
                            districts.AddRange(country.Districts);
                        }

                        nihai_districts.AddRange(districts);
                        //son kalınan bölgeye kadarki bölgeleri çıkart
                        foreach (District district in nihai_districts)
                        {
                            if (!string.IsNullOrWhiteSpace(CurrentSetting.LastDistrictName))
                            {
                                if (district.Name.Contains(currentSetting.LastDistrictName))
                                {
                                    break;
                                }
                                districts.Remove(district);
                            }
                        }

                        foreach (District district in districts)
                        {
                            sectors.AddRange(district.Sectors);
                        }

                        nihai_sectors.AddRange(sectors);
                        //son kalınan sektöre kadar ki bölgeleri çıkart
                        foreach (Sector sector in nihai_sectors)
                        {
                            if (!string.IsNullOrWhiteSpace(CurrentSetting.LastSectorName))
                            {
                                if (sector.Name.Contains(currentSetting.LastSectorName))
                                {
                                    break;
                                }
                                sectors.Remove(sector);
                            }
                        }

                        //Logger.WriteLog($"{countries.FirstOrDefault()?.Name} Ülkesinin Bölgeleri İçin ");

                    }
                    else
                    {
                        countries = db.Countries.ToList();
                        Logger.WriteLog("Her Sektor İçin Firmalar Çekiliyor");
                    }


                    //int lastIteration = 0;
                    int i = 0;
                    while (i < sectors.Count)
                    //for (int i = 0; i < sectors.Count(); i++)
                    {


                        Task paralelStaTask = TaskUtil.StartSTATask(() =>
                        {

                            CurrentReqCount++;
                            string responseBody = "";

                            i++;
                            #region Sektröe ait şirketleri alan kod

                            var sector = sectors[i];

                            //test
                            try
                            {
                                if (sector == sectors[i - 1])
                                {

                                }
                            }
                            catch (Exception)
                            {
                            }


                            $"{sector.Name} sektörünün Şirket kayıtlarınin 1. Sayfası İçin Veri Bekleniyor..."
                                .WriteLog();

                            responseBody = "";
                            Task task = Task.Run(async () =>
                            {
                                responseBody = await myClient.myGetStringAsync(sector.Url);
                            });
                            //while (!task.IsCompleted)
                            //{
                            //    Task.Delay(100);
                            //    //Thread.Sleep(100);
                            //}
                            task.Wait();

                            $"{sector.District.Country.Name}/{sector.District.Name}/{sector.Name} Sektörü İçin Firmalar Geldi"
                                .WriteLog();

                            int pageNumber = 1;

                            HtmlDocument convertedDoc = null;
                            Task innerTask = TaskUtil.StartSTATask(() =>
                            {
                                convertedDoc = responseBody.StringToWebBrowserDocument();
                                pageNumber = GetPageNumberFromDocument(convertedDoc);
                                //Task test = Task.Run(async () =>
                                //{
                                /*await*/
                                GetCompaniesFromDocument(convertedDoc, sector);
                                //});
                                //while (!test.IsCompleted)
                                //{
                                //    Task.Delay(100);
                                //}
                            });
                            while (!innerTask.IsCompleted || string.IsNullOrWhiteSpace(responseBody) ||
                                   convertedDoc == null)
                            {
                                //Task.Delay(100);
                                Thread.Sleep(100);
                            }

                            //await GetCompaniesFromDocument(convertedDoc, sector);

                            for (int j = 2; j <= pageNumber; j++)
                            {
                                $"{sector.District.Country.Name}/{sector.District.Name}/{sector.Name} Sektörü İçin {j}. Sayfa İçin İstek Atılıyor..."
                                    .WriteLog();

                                responseBody = "";
                                Task Itask = Task.Run(async () =>
                                {
                                    responseBody = await myClient.myGetStringAsync($"{sector.Url}{j}");
                                });
                                //while (!Itask.IsCompleted)
                                //{
                                //    //Task.Delay(100);
                                //    Thread.Sleep(100);
                                //}
                                Itask.Wait();

                                HtmlDocument innerConvertedDoc = null;

                                Task innerTaskForPage = TaskUtil.StartSTATask(() =>
                                {
                                    innerConvertedDoc = responseBody.StringToWebBrowserDocument();
                                });
                                while (!innerTaskForPage.IsCompleted || string.IsNullOrWhiteSpace(responseBody) ||
                                       innerConvertedDoc == null)
                                {
                                    //Task.Delay(100);
                                    Thread.Sleep(100);
                                }

                                //Task tTask = Task.Run(async () =>
                                //{
                                /*await */
                                GetCompaniesFromDocument(innerConvertedDoc, sector);
                                //});
                                //while (!tTask.IsCompleted)
                                //{
                                //    Task.Delay(100);
                                //}
                            }

                            CurrentReqCount--;
                            responseBody = "";

                            CurrentSetting.LastSectorName = sector.Name;
                            db.Entry(currentSetting).State = EntityState.Modified;
                            db.SaveChanges();
                            $"Şirketler İçin {sector.Name} Şirketleri Okunan Son Okunan Sektör Olarak Kaydedildi"
                                .WriteLog();


                            #endregion

                        });


                        while (CurrentReqCount >= ParallelThreadLimit)
                        {
                            $"Paralel İşlem Limitine Ulaşıldı Mevcut İşlemlerin Bitmesi Bekleniyor ({CurrentReqCount} / {ParallelThreadLimit})".WriteLog();
                            //Thread.Sleep(5000);
                            await Task.Delay(5000);
                        }
                    }
                }
                else
                {
                    "Sektörler İçin Şirketler ByPass Edildi.".WriteLog();
                }

            });

            t.Wait();

            while (CurrentReqCount > 1)
            {
                await Task.Delay(10000);
            }

            Logger.WriteLog("Tüm İşlemler Bitti !");
            return true;

        }

        public static void GetCountriesFromDocument(this HtmlDocument document)
        {
            vymapsDBEntities db = new vymapsDBEntities();

            Logger.WriteLog("Ülkeler Sayfadan Okunuyor...");

            List<HtmlElement> fourColumns = new List<HtmlElement>();

            foreach (HtmlElement div in document.GetElementsByTagName("div"))
            {
                if (div.GetAttribute("className") == "four columns")
                {
                    fourColumns.Add(div);
                }
            }

            Logger.WriteLog("Ülklerin Bulundupu divler Okundu");

            List<HtmlElement> countryLinks = new List<HtmlElement>();

            Logger.WriteLog("İçinde Link Olmayan Divler Tespit Ediliyor");

            for (int i = 0; i < fourColumns.Count; i++)
            {
                HtmlElement div = fourColumns[i];

                if (div.GetElementsByTagName("a").Count > 0)
                {
                    $"{i}. Div Link İçeriyor".WriteLog();
                    HtmlElement link = div.GetElementsByTagName("a")[0];
                    countryLinks.Add(link);
                    $"{i}. Link Kaydedildi => {link.InnerText}".WriteLog();
                }
                else
                {
                    $"{i}. Div Link İçermiyor".WriteLog();
                }
            }

            $"{countryLinks.Count} Adet Ülke Linki Okundu Veri Tabanına Kaydediliyor...".WriteLog();

            foreach (HtmlElement elLink in countryLinks)
            {
                Country country = new Country();
                country.Name = elLink.InnerText;
                country.Url = elLink.GetAttribute("href");
                country.Url = country.Url.Replace("about://vymaps.com", "https://vymaps.com");

                if (!db.Countries.Any(x => x.Name == country.Name))
                {
                    if (string.IsNullOrWhiteSpace(country.Name) || string.IsNullOrWhiteSpace(country.Url))
                    {
                        $"{country.Name} : {country.Url} Ülke Adı Veya Url Boş !!!".WriteLog();
                    }
                    else
                    {
                        db.Countries.Add(country);
                        $"{country.Name} Ülke Veri Tabanına Eklendi".WriteLog();
                    }
                }
                else
                {
                    $"{country.Name} Zaten Veri Tabanında Mevcut Ülke Kaydı Yapılmadı".WriteLog();
                }
            }

            db.SaveChanges();
            "Tüm Ülke Kayıtları Veri Tabanına Eklendi".WriteLog();
        }


        public static void GetDistrictsFromDocument(this HtmlDocument document, Country country)
        {
            vymapsDBEntities db = new vymapsDBEntities();

            Logger.WriteLog($"{country.Name} İçin Bölgeler Sayfadan Okunuyor...");

            List<HtmlElement> fourColumns = new List<HtmlElement>();

            foreach (HtmlElement div in document.GetElementsByTagName("div"))
            {
                if (div.GetAttribute("className") == "four columns")
                {
                    fourColumns.Add(div);
                }
            }

            Logger.WriteLog($"{country.Name} İçin Bölgelerin Bulunduğu divler Okundu");

            List<HtmlElement> districtLinks = new List<HtmlElement>();

            Logger.WriteLog("İçinde Link Olmayan Divler Tespit Ediliyor");

            for (int i = 0; i < fourColumns.Count; i++)
            {
                HtmlElement div = fourColumns[i];

                if (div.GetElementsByTagName("a").Count > 0)
                {
                    $"{i}. Div Link İçeriyor".WriteLog();
                    HtmlElement link = div.GetElementsByTagName("a")[0];
                    districtLinks.Add(link);
                    $"{i}. Link Kaydedildi => {link.InnerText}".WriteLog();
                }
                else
                {
                    $"{i}. Div Link İçermiyor".WriteLog();
                }
            }

            $"{country.Name} İçin {districtLinks.Count} Adet Bölge Linki Okundu Veri Tabanına Kaydediliyor...".WriteLog();

            foreach (HtmlElement elLink in districtLinks)
            {
                District district = new District();
                district.Name = elLink.InnerText;
                district.Url = elLink.GetAttribute("href");
                district.CountryID = country.ID;
                district.Url = district.Url.Replace("about://vymaps.com", "https://vymaps.com");

                if (!db.Districts.Any(x => x.Name == district.Name && x.CountryID == district.CountryID))
                {
                    if (string.IsNullOrWhiteSpace(district.Name) || string.IsNullOrWhiteSpace(district.Url))
                    {
                        $"{district.Name} : {district.Url} Bölge Adı Veya Url Boş !!!".WriteLog();
                    }
                    else
                    {
                        db.Districts.Add(district);
                        $"{district.Name} Bölge Veri Tabanına Eklendi".WriteLog();
                    }
                }
                else
                {
                    $"{district.Name} Zaten Veri Tabanında Bölge Ülke Kaydı Yapılmadı".WriteLog();
                }
            }

            db.SaveChanges();
            $"'{country.Name}' İçin Tüm Bölge Kayıtları Veri Tabanına Eklendi".WriteLog();
        }

        public static void GetSectorsFromDocument(this HtmlDocument document, District district)
        {
            vymapsDBEntities db = new vymapsDBEntities();

            Logger.WriteLog($"{district.Country.Name} / {district.Name} İçin Sektörler Sayfadan Okunuyor...");

            List<HtmlElement> fourColumns = new List<HtmlElement>();

            foreach (HtmlElement div in document.GetElementsByTagName("div"))
            {
                if (div.GetAttribute("className") == "four columns")
                {
                    fourColumns.Add(div);
                }
            }

            Logger.WriteLog($"{district.Country.Name} / {district.Name} İçin Sektörlerin Bulunduğu divler Okundu");

            List<HtmlElement> sectorLinks = new List<HtmlElement>();

            Logger.WriteLog("İçinde Link Olmayan Divler Tespit Ediliyor");

            for (int i = 0; i < fourColumns.Count; i++)
            {
                HtmlElement div = fourColumns[i];

                if (div.GetElementsByTagName("a").Count > 0)
                {
                    $"{i}. Div Link İçeriyor".WriteLog();
                    HtmlElement link = div.GetElementsByTagName("a")[0];
                    sectorLinks.Add(link);
                    $"{i}. Link Kaydedildi => {link.InnerText}".WriteLog();
                }
                else
                {
                    $"{i}. Div Link İçermiyor".WriteLog();
                }
            }

            $"{district.Country.Name} / {district.Name} Bölgesi İçin {sectorLinks.Count} Adet Sektör Linki Okundu Veri Tabanına Kaydediliyor...".WriteLog();

            foreach (HtmlElement elLink in sectorLinks)
            {
                Sector sector = new Sector();
                sector.Name = elLink.InnerText;
                sector.Url = elLink.GetAttribute("href");
                sector.DistrictID = district.ID;
                sector.Url = sector.Url.Replace("about://vymaps.com", "https://vymaps.com");

                if (!db.Sectors.Any(x => x.Name == sector.Name && x.DistrictID == sector.DistrictID))
                {
                    if (string.IsNullOrWhiteSpace(sector.Name) || string.IsNullOrWhiteSpace(sector.Url))
                    {
                        $"{sector.Name} : {sector.Url} Bölge Adı Veya Url Boş !!!".WriteLog();
                    }
                    else
                    {
                        db.Sectors.Add(sector);
                        $"{district.Country.Name} / {district.Name} / {sector.Name} Sektör Veri Tabanına Eklendi".WriteLog();
                    }
                }
                else
                {
                    $"{district.Country.Name} / {district.Name} / {sector.Name} Zaten Veri Tabanında Mevcut Sektör Kaydı Yapılmadı".WriteLog();
                }
            }
            db.SaveChanges();
            $"'{district.Country.Name} / {district.Name}' Bölgesi İçin Tüm Sector Kayıtları Veri Tabanına Eklendi".WriteLog();
        }

        public static int GetPageNumberFromDocument(this HtmlDocument document)
        {
            int valToReturn = 1;
            //sayfa sayısının yazdığı b elementini bulalım
            foreach (HtmlElement el in document.GetElementsByTagName("b"))
            {
                if (el.InnerText.Contains("Found:") && el.InnerText.Contains("Pages"))
                {
                    string innerText = el.InnerText;
                    innerText = innerText.Split(new string[] { "Places," }, StringSplitOptions.None)[1];
                    innerText = innerText.Replace("Pages", "");
                    valToReturn = Convert.ToInt32(innerText.Trim());

                }
            }


            return valToReturn;
        }

        public static void GetCompaniesFromDocument(this HtmlDocument document, Sector sector)
        {
            vymapsDBEntities db = new vymapsDBEntities();

            Logger.WriteLog($"{sector.Name} İçin Firmalar Sayfadan Okunuyor...");

            List<HtmlElement> sixColumns = new List<HtmlElement>();

            foreach (HtmlElement div in document.GetElementsByTagName("div"))
            {
                if (div.GetAttribute("className") == "six columns")
                {
                    sixColumns.Add(div);
                }
            }
            Logger.WriteLog($"{sector.Name} İçin Şirketlerin Bulunduğu 2 Div Okundu");

            List<HtmlElement> firstBs = new List<HtmlElement>();

            foreach (HtmlElement el in sixColumns)
            {
                foreach (HtmlElement pInSixCloumn in el.GetElementsByTagName("p"))
                {
                    if (pInSixCloumn.GetElementsByTagName("b").Count > 0)
                    {
                        HtmlElement firstB = pInSixCloumn.GetElementsByTagName("b")[0];
                        firstBs.Add(firstB);
                    }
                }
            }

            List<HtmlElement> sectorLinks = new List<HtmlElement>();

            Logger.WriteLog("İçinde Link Olmayan Divler Tespit Ediliyor");

            for (int i = 0; i < firstBs.Count; i++)
            {
                HtmlElement b = firstBs[i];

                if (b.GetElementsByTagName("a").Count > 0)
                {
                    $"{i}. b Link İçeriyor".WriteLog();
                    HtmlElement link = b.GetElementsByTagName("a")[0];
                    sectorLinks.Add(link);
                    $"{i}. Link Kaydedildi => {link.InnerText}".WriteLog();
                }
                else
                {
                    $"{i}. b Link İçermiyor".WriteLog();
                }
            }

            $"{sector.Name} Sektörü İçin İçin {sectorLinks.Count} Adet Şirket Linki Okundu Veri Tabanına Kaydediliyor...".WriteLog();

            foreach (HtmlElement elLink in sectorLinks)
            {
                try
                {
                    Company company = new Company();
                    company.Name = elLink.InnerText;
                    company.Url = elLink.GetAttribute("href");
                    company.SectorID = sector.ID;
                    company.Url = company.Url.Replace("about://vymaps.com", "https://vymaps.com");


                    if (!db.Companies.Any(x => x.Name == company.Name && x.SectorID == company.SectorID))
                    //if (!db.Companies.Any(x => x.Url == company.Url))
                    {
                        if (string.IsNullOrWhiteSpace(company.Name) || string.IsNullOrWhiteSpace(company.Url))
                        {
                            $"{company.Name} : {company.Url} Şirket Adı Veya Url Boş !!!".WriteLog();
                        }
                        else
                        {
                            //şimdide companynin tüm detaylarını almak için detay sayfasına gidelim.
                            //burası
                            string companyDetailData = "";
                            Task innerTask = Task.Run(async () =>
                            {
                                companyDetailData = await myClient.myGetStringAsync(company.Url);
                            });
                            while (!innerTask.IsCompleted || string.IsNullOrWhiteSpace(companyDetailData))
                            {
                                //Task.Delay(1000);
                                Thread.Sleep(1000);
                            }
                            company = company.DetailedCompany(companyDetailData.StringToWebBrowserDocument());


                            db.Companies.Add(company);
                            $"{sector.Name} / {company.Name} Şirket Veri Tabanına Eklendi".WriteLog();
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        $"{company.Name} Zaten Veri Tabanında Mevcut Şirket Kaydı Yapılmadı".WriteLog();
                    }
                }
                catch (Exception e)
                {
                    // ignored
                }
            }
            $"'{sector.District.Country.Name} /{sector.District.Name} / {sector.Name}' Sekötrü İçin Tüm Şirket Kayıtları Veri Tabanına Eklendi".WriteLog();
        }

        public static Company DetailedCompany(this Company incomingCompany, HtmlDocument document)
        {
            //string _PlaceType = "";
            //string _PlaceTypeUrl = "";
            //string _Address = "";
            //string _Coordinate = "";
            //string _CoordinateUrl = "";
            //string _What3Words = "";
            //string _What3WordsUrl = "";
            //string _Phone = "";
            //string _Email = "";
            //string _Rating = "";
            //string _Social = "";
            //string _Website = "";
            string _OpeningHours = "";
            string _WhatOtherSay = "";
            string _AboutTheBusiness = "";
            string _Description = "";
            string _ImageLinks = "";
            string _MapLink = "";

            // yerleri değişebiliyor. seven column içerisindkei bu arkadaşların dinamik olarka yerini bulacağız :(
            int _rowNumberPlaceType = -1;
            int _rowNumberAddress = -1;
            int _rowNumberCoordinate = -1;
            int _rowNumberWhat3Words = -1;
            int _rowNumberPhone = -1;
            int _rowNumberEmail = -1;
            int _rowNumberRating = -1;
            int _rowNumberSocial = -1;
            int _rowNumberWebsite = -1;

            //seven column bulalım (çoğu bilgi burda)
            HtmlElement sevenColumn = null;
            foreach (HtmlElement el in document.GetElementsByTagName("div"))
            {
                if (el.GetAttribute("className") == "seven columns")
                {
                    sevenColumn = el;
                    break;
                }
            }

            if (sevenColumn != null && sevenColumn.GetElementsByTagName("table")[0] != null)
            {
                var tableInSevenColumn = sevenColumn.GetElementsByTagName("table")[0];

                #region Tablo İçindeki Verilerin Yerinin Bulalım Ve Alalım

                if (tableInSevenColumn != null)
                {
                    HtmlElement tbody = tableInSevenColumn.GetElementsByTagName("tbody")[0];
                    if (tbody != null)
                    {
                        //yerleri bulalım
                        for (int i = 0; i < tbody.GetElementsByTagName("tr").Count; i++)
                        {
                            HtmlElement row = tbody.GetElementsByTagName("tr")[i];
                            string innerText = row.InnerText;

                            if (innerText.Contains("Place Type") || innerText.Contains("Yer Türleri"))
                            {
                                _rowNumberPlaceType = i;
                            }
                            if (innerText.Contains("Address") || innerText.Contains("Adres"))
                            {
                                _rowNumberAddress = i;
                            }
                            if (innerText.Contains("Coordinate") || innerText.Contains("Koordinat"))
                            {
                                _rowNumberCoordinate = i;
                            }
                            if (innerText.Contains("What3Words"))
                            {
                                _rowNumberWhat3Words = i;
                            }
                            if (innerText.Contains("Phone") || innerText.Contains("Telefon"))
                            {
                                _rowNumberPhone = i;
                            }
                            if (innerText.Contains("Email") || innerText.Contains("E-posta"))
                            {
                                _rowNumberEmail = i;
                            }
                            if (innerText.Contains("Rating") || innerText.Contains("Değerlendirme"))
                            {
                                _rowNumberRating = i;
                            }
                            if (innerText.Contains("Social") || innerText.Contains("Sosyal"))
                            {
                                _rowNumberSocial = i;
                            }
                            if (innerText.Contains("Website") || innerText.Contains("İnternet Sitesi"))
                            {
                                _rowNumberWebsite = i;
                            }

                        }
                    }

                    incomingCompany.PlaceType = _rowNumberPlaceType.GetDataFromTableRowIfExist(tbody, false);
                    incomingCompany.PlaceTypeUrl = _rowNumberPlaceType.GetDataFromTableRowIfExist(tbody, true).Replace("about://vymaps.com", "https://vymaps.com"); ;

                    incomingCompany.Address = _rowNumberAddress.GetDataFromTableRowIfExist(tbody, false);

                    incomingCompany.Coordinate = _rowNumberCoordinate.GetDataFromTableRowIfExist(tbody, false);
                    incomingCompany.CoordinateUrl = _rowNumberCoordinate.GetDataFromTableRowIfExist(tbody, true);

                    incomingCompany.What3Words = _rowNumberWhat3Words.GetDataFromTableRowIfExist(tbody, false);
                    incomingCompany.What3WordsUrl = _rowNumberWhat3Words.GetDataFromTableRowIfExist(tbody, true);

                    incomingCompany.Phone = _rowNumberPhone.GetDataFromTableRowIfExist(tbody, false);
                    incomingCompany.Email = _rowNumberEmail.GetDataFromTableRowIfExist(tbody, false);
                    incomingCompany.Rating = _rowNumberRating.GetDataFromTableRowIfExist(tbody, false);
                    incomingCompany.Social = _rowNumberSocial.GetDataFromTableRowIfExist(tbody, false);
                    incomingCompany.Website = _rowNumberWebsite.GetDataFromTableRowIfExist(tbody, false);
                }


                #endregion

                //diğer verileri alalım

                #region Resimleri Çekelim
                //resimlerin olduğu divi bulalım ( position: relative; height: 300px; text-align: center; )

                //HtmlElement imagesDiv = null;
                //foreach (HtmlElement div in document.GetElementsByTagName("div"))
                //{
                //    if (div.Style != null && div.Style.Contains("position: relative; height: 300px; text-align: center;"))
                //    {
                //        imagesDiv = div;
                //        break; ;
                //    }
                //}

                //if (imagesDiv != null)
                //{
                //    foreach (HtmlElement imgEl in imagesDiv.GetElementsByTagName("img"))
                //    {
                //        string link = imgEl.GetAttribute("src");
                //        _ImageLinks += $" {link}";
                //    }
                //}
                foreach (HtmlElement img in document.GetElementsByTagName("img"))
                {
                    if (img.GetAttribute("className") == "lozad")
                    {
                        string link = img.GetAttribute("data-src");
                        _ImageLinks += $" {link}";
                    }
                }

                #endregion

                #region OpeningHours Çekelim

                HtmlElement openingHoursSpan = null;
                foreach (HtmlElement span in document.GetElementsByTagName("span"))
                {
                    if (span.InnerHtml.Contains("Monday:") || span.InnerHtml.Contains("Pazartesi:"))
                    {
                        openingHoursSpan = span;
                        break;
                    }
                }

                if (openingHoursSpan != null) _OpeningHours = openingHoursSpan.InnerText;
                #endregion

                #region twelveCloumnları alalım (about the business description / what other says)

                foreach (HtmlElement div in document.GetElementsByTagName("div"))
                {
                    if (div.GetAttribute("className") == "twelve columns")
                    {
                        if (div.InnerText == null)
                        {
                            continue;
                        }
                        if (div.InnerText.Contains("What Other Say:") || div.InnerText.Contains("Başka Ne Der:"))
                        {
                            _WhatOtherSay = div.InnerText.Replace("What Other Say:", "");
                            _WhatOtherSay = div.InnerText.Replace("Başka Ne Der:", "");
                        }
                        if (div.InnerText.Contains("About The Business:") || div.InnerText.Contains("Iş Hakkında:"))
                        {
                            _AboutTheBusiness = div.InnerText.Replace("About The Business:", "");
                            _AboutTheBusiness = div.InnerText.Replace("Iş Hakkında:", "");
                        }
                        if (div.InnerText.Contains("Description:") || div.InnerText.Contains("Açıklama:"))
                        {
                            _Description = div.InnerText.Replace("Description:", "");
                            _Description = div.InnerText.Replace("Açıklama:", "");
                        }
                    }

                }

                #endregion

                #region MapLinki Alalım

                HtmlElement a = document.GetElementById("map_id");
                if (a != null)
                {
                    //HtmlElement b = a.GetElementsByTagName("div")[0];
                    //if (b != null)
                    //{
                    HtmlElement c = a.GetElementsByTagName("object")[0];
                    if (c != null)
                    {

                        _MapLink = c.GetAttribute("data");
                    }
                    //}

                }
                #endregion

                incomingCompany.MapLink = _MapLink;
                incomingCompany.ImageLinks = _ImageLinks;
                incomingCompany.OpeningHours = _OpeningHours;
                incomingCompany.Description = _Description;
                incomingCompany.AboutTheBusiness = _AboutTheBusiness;
                incomingCompany.WhatOtherSay = _WhatOtherSay;




            }

            return incomingCompany;
        }

        static string GetDataFromTableRowIfExist(this int rowNumber, HtmlElement tbody, bool getFirstAsHrefAttr)
        {
            string data = "";

            if (rowNumber > -1)
            {
                try
                {
                    if (getFirstAsHrefAttr)
                    {
                        data = tbody.GetElementsByTagName("tr")[rowNumber].GetElementsByTagName("td")[2].GetElementsByTagName("a")[0].GetAttribute("href");
                    }
                    else
                    {
                        data = tbody.GetElementsByTagName("tr")[rowNumber].GetElementsByTagName("td")[2].InnerText;
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }


            return data;
        }
    }
}
