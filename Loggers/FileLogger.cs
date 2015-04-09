using System;
using Interfaces;
using System.IO;

namespace Logger
{
    public class TextFileLogger : ILoggingService
    {
        public void Log(string message)
        {
            var fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";

            if (!File.Exists(fileName))
            {
                using (var writer = File.CreateText(fileName))
                {
                    writer.WriteLine(message);
                }
            }
            else
            {
                using (var writer = File.AppendText(fileName))
                {
                    writer.WriteLine(message);

                }
            }
        }
    }
}
