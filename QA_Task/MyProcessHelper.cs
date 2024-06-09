using System.Diagnostics;

namespace QA_Task
{
    public static class MyProcessHelper
    {
        /// <summary>
        /// Calculates the total runtime of the program, from the startTime to the current time
        /// </summary>
        /// <param name="proc">The target process</param>
        public static int CalculateTimespanInMinutes(this Process proc)
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
        public static void KillProcess(this Process proc, int maxLifetime)
        {
            try
            {
                int runtime = proc.CalculateTimespanInMinutes();
                if (runtime >= maxLifetime)
                {
                    ConsolePrinter.PrintMessage($"id:[{proc.Id}] runtime:[{runtime}min]", ConsolePrinter.MessageType.Kill);
                    // we write the kill to log
                    MyLogger.WriteToLog($"[killed] processName:[{proc.ProcessName}] id:[{proc.Id}] startTime:[{proc.StartTime}] runtime:[{runtime}min]");
                    proc.Kill();
                }
            }
            catch (Exception ex)
            {
                ConsolePrinter.PrintMessage(ex.ToString(), ConsolePrinter.MessageType.Error);
            }
        }

        /// <summary>
        /// Wrapper to use foreach to loop through the target processes
        /// </summary>
        /// <param name="procs">Target processes</param>
        /// <param name="maxLifetime">Amount of minutes a process is allowed to live</param>
        public static void KillProcessesThatExceedsLifetime(this Process[] procs, int maxLifetime)
        {
            foreach (Process proc in procs)
            {
                proc.KillProcess(maxLifetime);
            }
        }

        /// <summary>
        /// Prints the process information to the console
        /// </summary>
        /// <param name="proc"></param>
        public static void PrintProcessInfo(this Process proc)
        {
            ConsolePrinter.PrintMessage($"id:[{proc.Id}] startTime:[{proc.StartTime}] runtime:[{CalculateTimespanInMinutes(proc)}min]", ConsolePrinter.MessageType.Info);
        }

        /// <summary>
        /// Wrapper for the PrintProcessInfo function to loop through all the processes
        /// </summary>
        /// <param name="procs"></param>
        public static void PrintProcesses(this Process[] procs)
        {
            foreach (Process process in procs)
            {
                process.PrintProcessInfo();
            }
        }
    }
}
