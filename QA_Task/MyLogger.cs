namespace QA_Task
{
    public static class MyLogger
    {
        // path to the log file
        private static string logFilePath = AppDomain.CurrentDomain.BaseDirectory + "log.txt";

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
                    ConsolePrinter.PrintMessage($"log file created at: {logFilePath}", ConsolePrinter.MessageType.Info);
                }
            }
        }
    }
}
