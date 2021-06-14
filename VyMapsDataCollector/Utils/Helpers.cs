﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using HtmlAgilityPack;

namespace VyMapsDataCollector.Utils
{
    public static class Helpers
    {
        public static HtmlDocument StringToWebBrowserDocument(this string html)
        {
            HtmlDocument docToReturn = null;
            try
            {
                //TaskUtil.StartSTATask(() =>
                //{
                WebBrowser browser = new WebBrowser();
                browser.ScriptErrorsSuppressed = true;
                browser.DocumentText = html;
                if (browser.Document != null)
                {
                    browser.Document.OpenNew(true);
                    browser.Document.Write(html);
                    //});
                    //browser.Refresh();
                    docToReturn = browser.Document;
                }
            }
            catch (Exception e)
            {
                StringToWebBrowserDocument(html);
                //return null;
            }

            return docToReturn;
        }

        public static System.Windows.Forms.HtmlDocument StringToWebBrowserDocumenta(this string html)
        {
            HtmlDocument doc = null;
            var th = new Thread(async () =>
             {
                 WebBrowser browser = new WebBrowser();
                 browser.ScriptErrorsSuppressed = true;
                 browser.DocumentText = html;
                 browser.Document.OpenNew(true);
                 browser.Document.Write(html);
                 while (doc == null)
                 {
                     doc = browser.Document;
                     await Task.Delay(100);
                 }
             });

            th.SetApartmentState(ApartmentState.STA);
            th.Start();

            while (doc == null)
            {
                Task.Delay(1000);
            }



            return doc;
        }

        public static HtmlAgilityPack.HtmlDocument StringToWebBrowserDocumentHtmlAgilityPack(this string html)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            return doc;
        }

     


    }
}