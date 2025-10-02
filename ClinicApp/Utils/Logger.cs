using System;
using System.IO;

namespace VetClinic.Utils
{
    public static class Logger
    {
        private static readonly string logFile = "vetclinic_log.txt";

        public static void LogInfo(string message)
        {
            string log = $"[INFO] {DateTime.Now}: {message}";
            Console.WriteLine(log);
            File.AppendAllText(logFile, log + Environment.NewLine);
        }

        public static void LogError(string message, Exception ex)
        {
            string log = $"[ERROR] {DateTime.Now}: {message} | Exception: {ex.Message}";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(log);
            Console.ResetColor();
            File.AppendAllText(logFile, log + Environment.NewLine);
        }
    }
}
