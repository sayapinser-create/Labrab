using System;
using System.IO;

namespace ПИС2
{
    public static class Logger
    {
        private static string logPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
            "log.txt");

        public static string LogPath
        {
            get { return logPath; }
        }

        public static void SetLogPath(string path)
        {
            logPath = path;
        }

        public static void LogData(string message)
        {
            try
            {
                string dir = Path.GetDirectoryName(logPath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
                File.AppendAllText(logPath, line + Environment.NewLine);
            }
            catch
            {
            }
        }
    }
}