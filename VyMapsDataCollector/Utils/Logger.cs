using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VyMapsDataCollector.Utils
{
    public static class Logger
    {
        //public static StringBuilder _LogSB = new StringBuilder();

        public static void WriteLog(this string logTxt)
        {
            logTxt = $"{DateTime.Now}: {logTxt}";
            //_LogSB.AppendLine(logTxt);
            Console.WriteLine(logTxt);
        }
    }
}
