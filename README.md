# ProcessKiller

### QA_Task - Command Line project

Utility to close a specified process once it reaches the specified lifespan (in minutes).

#### How to use the script

```bash
./QA_Task <process_name> <lifespan_in_minutes> <recheck_frequency>
```

Process name is the name of the process we want to kill

Lifespan in minutes is the amount of minutes a program is allowed to live, if the target raeches it, it will get killed.

Recheck frequency is the amount of minutes it takes for the script to recheck the lifespan and processes with the specified name.

Upon killing a process it will be logged in the log.txt file located right in the folder where the script is executed.

I added some colors to the console output for a better visual experience.
![cmd picture](cmdSS.png?raw=true "Title")

### QA_Task_Test - NUnit project

This is a NUnit test project for **some** of the project functions that I found more important than others.

**MyLogger functions that I tested**

```csharp
    /// <summary>
    /// Writes to the log file
    /// </summary>
    /// <param name="message"></param>
    public static void WriteToLog(string message)
    {
        using (StreamWriter sw = new StreamWriter(logFilePath, true))
        {
            sw.WriteLine($" {DateTime.Now} - {message} ");
        }
    }

    /// <summary>
    /// Creates the log file in the directory where the executable is located
    /// </summary>
    public static void CreateLogFile()
    {
        if (!File.Exists(logFilePath))
        {
            using (StreamWriter sw = new StreamWriter(logFilePath, false))
            {
                ConsolePrinter.PrintInfo($"log file created at: {logFilePath}");
            }
        }
    }
```

You can find the tests by clicking those links [CreateLogFileTest](https://github.com/izecheru/ProcessKiller/blob/9022b0dce89419d4e1b2814076e1e2e296307e96/QA_Task_Test/MyLoggerTests.cs#L27) and [WriteToLogTest](https://github.com/izecheru/ProcessKiller/blob/9022b0dce89419d4e1b2814076e1e2e296307e96/QA_Task_Test/MyLoggerTests.cs#L34).

**MyProcessHelper functions that I tested**

```csharp
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
        public static void KillProcesses(this Process[] procs, int maxLifetime)
        {
            foreach (Process proc in procs)
            {
                proc.KillProcess(maxLifetime);
            }
        }
```
Here I chose not to write a test for the array wrapper of kill process because I found it redundant since the kill one process one works fine.

```csharp
 public static void KillProcesses(Process[] procs, int maxLifetime)
```

You can find the tests by clicking those links [CalculateTimespanInMinutes](https://github.com/izecheru/ProcessKiller/blob/9022b0dce89419d4e1b2814076e1e2e296307e96/QA_Task_Test/ProcessHelperTests.cs#L15) and [KillProcess](https://github.com/izecheru/ProcessKiller/blob/9022b0dce89419d4e1b2814076e1e2e296307e96/QA_Task_Test/ProcessHelperTests.cs#L38).

