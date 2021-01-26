using System;

namespace harbor.Helpers
{
    public class ConsoleHelper
    {

        /// <summary>
        /// Ask the user a question
        /// </summary>
        /// <param name="Label"></param>
        /// <returns></returns>
        public static string Ask(string Label)
        {
            Console.WriteLine($"\n{Label}");
            return Console.ReadLine();
        }

        /// <summary>
        /// Ask the user to select a option
        /// </summary>
        /// <param name="Label"></param>
        /// <param name="options"></param>
        /// <returns>The index number in the options.</returns>
        public static int AskOption(string Label, string[] options)
        {
            Console.WriteLine($"\n{Label}:");

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"[{i}] {options[i]}");
            }
                        
            Console.WriteLine($"\n");
            var userInput = Console.ReadLine();
            return Int32.Parse(userInput);
        }

        /// <summary>
        /// As the user a secret question
        /// </summary>
        /// <param name="Label"></param>
        /// <returns></returns>
        public static string AskSecret(string Label)
        {
            Console.WriteLine($"\n{Label}");
            Console.ForegroundColor = ConsoleColor.Black;
            var userInput = Console.ReadLine();
            Console.ResetColor();
            return userInput;
        }

        /// <summary>
        /// Print info message to user
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static void PrintInfo(string Message)
        {
            Console.ResetColor();            
            Console.WriteLine("\n" + Message);
            Console.ResetColor();
        }

        /// <summary>
        /// Print error message to user
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static void PrintError(string Message)
        {
            Console.ResetColor();     
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;       
            Console.WriteLine("\n" + Message);
            Console.ResetColor();            
            Console.WriteLine();                       
        }

        /// <summary>
        /// Print success message to user
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static void PrintSuccess(string Message)
        {
            Console.ResetColor();     
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;                   
            Console.WriteLine("\n" + Message);
            Console.ResetColor();            
            Console.WriteLine();          
        }

        /// <summary>
        /// Ask the user to confirm a action
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static bool Confirm(string Message)
        {
            var result = ConsoleHelper.Ask(Message);
            if (result != "y") {
                return false;
            }
            return true;
        }
    }
}