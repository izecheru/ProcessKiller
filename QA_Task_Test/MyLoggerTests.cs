using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using NUnit.Framework.Legacy;
using QA_Task;
using System;
using System.IO;

namespace QA_Task.Tests
{
    [TestFixture]
    public class LoggerTests
    {
        private string testLogFilePath;

        [SetUp]
        public void Setup()
        {
            testLogFilePath = AppDomain.CurrentDomain.BaseDirectory + "log.txt";
            if (File.Exists(testLogFilePath))
            {
                File.Delete(testLogFilePath);
            }
        }

        [Test]
        public void CreateLogFile_FileDoesNotExist_LogFileCreated()
        {
            MyLogger.CreateLogFile();
            Assert.That(true,Is.EqualTo(File.Exists(testLogFilePath)));
        }

        [Test]
        public void WriteToLog_MessageProvided_MessageWrittenToLogFile()
        {
            MyLogger.CreateLogFile();
            string testMessage = $"This is a test message";
            MyLogger.WriteToLog(testMessage);

            string logContents = File.ReadAllText(testLogFilePath).Trim();
            Assert.That(logContents, Is.EqualTo($"{DateTime.Now} - "+testMessage));
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(testLogFilePath))
            {
                File.Delete(testLogFilePath);
            }
        }
    }
}
