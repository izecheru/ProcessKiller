namespace QA_Task
{

    public static class QuitListener
    {
        public static bool quit = false;
        public static int monitoringFreqMilliseconds = 0;

        /// <summary>
        /// Listens to Q key press and stops the program once it detects it
        /// </summary>
        public static void ListenForKey()
        {
            while (true)
            {
                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Q)
                {
                    ConsolePrinter.PrintMessage("Quitting the program...", ConsolePrinter.MessageType.Info);
                    quit = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Sleeps the main thread
        /// </summary>
        public static void SleepTheMainThread()
        {
            for (int i = 0; i < monitoringFreqMilliseconds / 100; i++)
            {
                if (quit)
                    break;
                Thread.Sleep(100);
            }
        }
    }
}