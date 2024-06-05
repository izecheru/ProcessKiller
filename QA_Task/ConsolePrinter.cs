using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA_Task
{
    public static class ConsolePrinter
    {

        /// <summary>
        /// Printing info to the console
        /// </summary>
        /// <param name="message"></param>
        public static void PrintInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($" {DateTime.Now} [inf] {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Printing errors to the console
        /// </summary>
        /// <param name="message"></param>
        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($" {DateTime.Now} [err] {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Printing process kills to the console
        /// </summary>
        /// <param name="message"></param>
        public static void PrintKill(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($" {DateTime.Now} [killed] {message}");
            Console.ResetColor();
        }
    }
}
