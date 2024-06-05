using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA_Task
{
    public static class MyProcessHelper
    {
        public static int CalculateTimespanInMinutes(Process proc)
        {
            TimeSpan procRuntimeTotal = DateTime.Now - proc.StartTime;
            return procRuntimeTotal.Minutes;
        }

        /// <summary>
        /// Kills a process based on the maxLifetime parameter, current time - process start time < maxLifetime 
        /// for the process to live
        /// </summary>
        /// <param name="proc">The target process</param>
        /// <param name="maxLifetime">Amount of minutes a process is allowed to live</param>
        public static void KillProcess(Process proc, int maxLifetime)
        {
            try
            {
                int runtime = CalculateTimespanInMinutes(proc);
                if (runtime >= maxLifetime)
                {
                    ConsolePrinter.PrintKill($"id:[{proc.Id}] runtime:[{runtime}min]");
                    // we write the kill to log
                    MyLogger.WriteToLog($"[killed] processName:[{proc.ProcessName}] id:[{proc.Id}] startTime:[{proc.StartTime}] runtime:[{runtime}min]");
                    proc.Kill();
                }
            }
            catch (Exception ex)
            {
                ConsolePrinter.PrintError(ex.ToString());
            }
        }

        /// <summary>
        /// Wrapper to use foreach to loop through the target processes
        /// </summary>
        /// <param name="procs">Target processes</param>
        /// <param name="maxLifetime">Amount of minutes a process is allowed to live</param>
        public static void KillProcesses(Process[] procs, int maxLifetime)
        {
            foreach (Process proc in procs)
            {
                KillProcess(proc, maxLifetime);
            }
        }

        /// <summary>
        /// Prints the process information to the console
        /// </summary>
        /// <param name="proc"></param>
        public static void PrintProcessInfo(Process proc)
        {
            ConsolePrinter.PrintInfo($"id:[{proc.Id}] startTime:[{proc.StartTime}] runtime:[{CalculateTimespanInMinutes(proc)}min]");
        }

        /// <summary>
        /// Wrapper for the PrintProcessInfo function to loop through all the processes
        /// </summary>
        /// <param name="procs"></param>
        public static void PrintProcesses(Process[] procs)
        {
            foreach (Process process in procs)
            {
                PrintProcessInfo(process);
            }
        }
    }
}
