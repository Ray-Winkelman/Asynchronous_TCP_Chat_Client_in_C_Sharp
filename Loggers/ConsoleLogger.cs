using System;
using Interfaces;
using System.Diagnostics;

namespace Logger
{
    public class ConsoleLogger : ILoggingService
    {
        public void Log(string message)
        {
            Debug.WriteLine(GetTimestamp(DateTime.Now) + message);
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyy/MM/dd HH:mm:ss (fff)") + " :  ";
        }
    }
}