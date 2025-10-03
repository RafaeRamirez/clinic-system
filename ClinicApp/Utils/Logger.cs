using System;
using System.IO;

namespace VetClinic.Utils
{
    public static class Logger
    {
        private static readonly string logPath = "vetclinic_log.txt";

        public static void LogInfo(string message)
        {
            Log("[INFO]", message);
        }

        public static void LogError(string message, Exception ex)
        {
            Log("[ERROR]", $"{message} - Exception: {ex.Message}");
        }

        private static void Log(string level, string message)
        {
            string logEntry = $"{level} {DateTime.Now}: {message}";
            Console.WriteLine(logEntry);
            File.AppendAllText(logPath, logEntry + Environment.NewLine);
        }
    }
}
