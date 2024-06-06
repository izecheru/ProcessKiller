using System.Diagnostics;

namespace QA_Task
{
    internal class Program
    {
        static bool quit = false;
        static int monitoringFreqMilliseconds = 0;

        static int ConvertToMilliseconds(int minutes)
        {
            return minutes * 60 * 1000;
        }

        static void ListenForKey()
        {
            while (true)
            {
                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Q)
                {
                    ConsolePrinter.PrintError("Quitting the program...");
                    quit = true;
                    break;
                }
            }
        }

        static void Main(string[] args)
        {
            int maxLifetime = 0;
            int monitoringFreq = 0;
            int cicles = 0;

            MyLogger.CreateLogFile();

            switch (args.Length)
            {
                case 3:
                    try
                    {
                        if (!int.TryParse(args[1], out maxLifetime))
                        {
                            ConsolePrinter.PrintError($"[{args[1]}] wrong format");
                            Environment.Exit(1);
                        }
                        if (!int.TryParse(args[2], out monitoringFreq))
                        {
                            ConsolePrinter.PrintError($"[{args[2]}] wrong format");
                            Environment.Exit(1);
                        }

                        // we start a new thread for the keyboard listener so we 
                        // don't use sleep on this one and risk to not be able to 
                        // send input to the program
                        Thread quitListener = new Thread(ListenForKey);
                        quitListener.Start();

                        ConsolePrinter.PrintInfo("Listening...");
                        monitoringFreqMilliseconds = ConvertToMilliseconds(monitoringFreq);
                        string targetProc = args[0].ToLower();

                        // run as long as we dont press q
                        while (!quit)
                        {
                            ConsolePrinter.PrintInfo($"checking for running processes with name: {targetProc}");
                            Process[] targets = Process.GetProcessesByName(targetProc);

                            // print found processes 
                            targets.PrintProcesses();
                            // kill the processes that have a runtime bigger or equal to the maxLifetime variable
                            targets.KillProcesses(maxLifetime);

                            for (int i = 0; i < monitoringFreqMilliseconds / 100; i++)
                            {
                                if (quit)
                                    break;
                                Thread.Sleep(100);
                            }

                            // a variable to know how many cycles of checking for a process we've been through
                            // not used at the moment, added for debugging purposes
                            ++cicles;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    break;
                default:
                    Console.WriteLine("eg usage: ./QA_Task processName maxLifetime frequency");
                    Console.WriteLine("\nprocessName <string>:\tThe name of the process that must be killed\nmaxLifetime <int>:\tThe amount of minutes a process is allowed to live\nfrequency <int>:\tThe frequency in minutes at which we verify the processes");
                    break;
            }
        }
    }
}
