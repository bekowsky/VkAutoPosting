using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
   public class ConsoleLogger : ILogger
    {
        public void LogErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void LogInfoMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.ResetColor(); 
        }

        public void LogWarningMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow; 
            Console.WriteLine(message);
            Console.ResetColor(); 
        }

        public void LogSuccessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
