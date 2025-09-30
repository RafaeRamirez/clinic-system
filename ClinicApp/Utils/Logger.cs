using System;
using System.IO;

namespace ClinicApp.Utils
{
    public static class Logger
    {
        private static readonly string logFile = "error_log.txt";

        public static void LogError(string message, Exception ex)
        {
            string formattedMessage = $"[{DateTime.Now}] ERROR: {message}\nDetails: {ex.Message}\n";

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(formattedMessage);
            Console.ResetColor();

            File.AppendAllText(logFile, formattedMessage + Environment.NewLine);
        }

        public static void LogInfo(string message)
        {
            string formattedMessage = $"[{DateTime.Now}] INFO: {message}";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(formattedMessage);
            Console.ResetColor();

            File.AppendAllText(logFile, formattedMessage + Environment.NewLine);
        }
    }
}
