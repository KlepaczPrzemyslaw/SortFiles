using System;
using System.IO;

namespace SortFiles
{
    public static class Logger
    {
        private static readonly string _pathToLogs =
            $"{AppDomain.CurrentDomain.BaseDirectory}\\Logs.txt";

        private static readonly string _pathToCriticalLogs =
            $"{AppDomain.CurrentDomain.BaseDirectory}\\CriticalLogs.txt";

        private static string LogDate =>
            $"{DateTime.UtcNow:o} UTC =>";

        /// <summary>
        /// If "Logs.txt" file does not exist, it will be created
        /// </summary>
        public static void InitLogFile()
        {
            // Create logs file if it does not exist
            if (!File.Exists(_pathToLogs))
            {
                using (StreamWriter streamWriter = File.CreateText(_pathToLogs))
                {
                    streamWriter.WriteLine($"{LogDate} Logs will be inserted there!");
                }
            }
        }

        public static void Log(string message)
        {
            using (StreamWriter streamWriter = File.AppendText(_pathToLogs))
            {
                streamWriter.WriteLine($"{LogDate} Message: [{message}]");
            }
        }

        public static void LogError(string errorMessage)
        {
            using (StreamWriter streamWriter = File.AppendText(_pathToLogs))
            {
                streamWriter.WriteLine($"{LogDate} ERROR! => [{errorMessage}] <= !ERROR");
            }
        }

        public static void LogCriticalError(Exception ex, string message)
        {
            if (!File.Exists(_pathToCriticalLogs))
            {
                using (StreamWriter streamWriter = File.CreateText(_pathToCriticalLogs))
                {
                    streamWriter.WriteLine($"{LogDate} => CRITICAL error has been thrown! => Dev message: [{message}] => " +
                        $"Please read exception message: [{ex?.Message}] and inner message: [{ex?.InnerException?.Message}] and contact developer!");
                }
            }
            else
            {
                using (StreamWriter streamWriter = File.AppendText(_pathToCriticalLogs))
                {
                    streamWriter.WriteLine($"{LogDate} => CRITICAL error has been thrown! => Dev message: [{message}] => " +
                        $"Please read exception message: [{ex?.Message}] and inner message: [{ex?.InnerException?.Message}] and contact developer!");
                }
            }
        }
    }
}
