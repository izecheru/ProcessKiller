using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions;
using QA_Task;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA_Task_Test
{
    public class ProcessHelperTests
    {
        [Test]
        public void CalculateTimespanInMinutes_ProcessStartedOneMinuteAgo()
        {
            // Arrange
            Process testProcess = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            testProcess.StartInfo = startInfo;
            testProcess.Start();
            Thread.Sleep(60*1000); 

            int runtimeMinutes = MyProcessHelper.CalculateTimespanInMinutes(testProcess);

            Assert.That(1, Is.EqualTo(runtimeMinutes));

            testProcess.Kill();
            testProcess.Dispose();
        }

        [Test]
        public static void KillProcess_KillAfterOneMinute()
        {
            Process testProcess = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            testProcess.StartInfo = startInfo;
            testProcess.Start();
            Thread.Sleep(60 * 1000);

            MyProcessHelper.KillProcess(testProcess, 1);
            Assert.That(0, Is.EqualTo(Process.GetProcessesByName("cmd").Count()));

            testProcess.Kill();
            testProcess.Dispose();
        }
    }
}
