using System;
using System.IO;

namespace VetClinic.Utils
{
    // Utility class for logging application events and errors to both the console and a log file
    public static class Logger
    {
        // Path where the log file is saved
        private static readonly string logPath = "vetclinic_log.txt";

        // Records general information messages
        public static void LogInfo(string message)
        {
            Log("[INFO]", message);
        }

        // Records error messages along with exception details
        public static void LogError(string message, Exception ex)
        {
            Log("[ERROR]", $"{message} - Exception: {ex.Message}");
        }

        // Internal method that formats and writes log entries
        private static void Log(string level, string message)
        {
            string logEntry = $"{level} {DateTime.Now}: {message}";
            Console.WriteLine(logEntry);
            File.AppendAllText(logPath, logEntry + Environment.NewLine);
        }
    }
}
