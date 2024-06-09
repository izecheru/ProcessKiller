
namespace QA_Task
{
    public static class ConsolePrinter
    {

        public enum MessageType
        {
            Info,
            Kill,
            Error
        }

        /// <summary>
        /// Printing info to the console
        /// </summary>
        /// <param name="message"></param>
        public static void PrintMessage(string message, MessageType type)
        {
            switch (type)
            {
                case MessageType.Info:
                    {

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($" {DateTime.Now} [inf] {message}");
                        Console.ResetColor();
                        break;
                    }

                case MessageType.Kill:
                    {

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($" {DateTime.Now} [kill] {message}");
                        Console.ResetColor();
                        break;
                    }
                case MessageType.Error:
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($" {DateTime.Now} [err] {message}");
                        Console.ResetColor();
                        break;
                    }
            }
        }
    }
}
