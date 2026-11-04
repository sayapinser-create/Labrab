using System;
using System.IO;
using ПИС2;
using Xunit;

namespace ПИС2.Tests
{
    public class LoggerTests
    {
        private static readonly string TestLogPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
            "logTest.txt");

        public LoggerTests()
        {
            if (File.Exists(TestLogPath))
                File.Delete(TestLogPath);

            Logger.SetLogPath(TestLogPath);
        }

        [Fact]
        public void Logger_Write()
        {
            string marker = "TEST_" + Guid.NewGuid();
            Logger.LogData(marker);

            Assert.True(File.Exists(TestLogPath), "Файл logTest.txt не создан");

            string content = File.ReadAllText(TestLogPath);
            Assert.Contains(marker, content);
        }
    }
}