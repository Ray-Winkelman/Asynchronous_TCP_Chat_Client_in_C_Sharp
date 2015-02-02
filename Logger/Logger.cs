using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class Log
    {
        string filename = DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ".log";

        public void LogLine(string message)
        {
            System.IO.File.AppendAllText(filename, DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + ":  " + message + Environment.NewLine);
        }
    }
}
