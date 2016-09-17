using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables
{
    class Program
    {
        static void Main(string[] args)
        {
            string prompt = ">> ";
            string request = "What would you like to do?";
            Console.WriteLine(request);
            Console.Write(prompt);
            string usercommand = Console.ReadLine();
            if (usercommand == "exit")
            {
                Environment.Exit(0);
            }
            else if (usercommand == "quit")
            {
                Environment.Exit(0);
            }
            else usercommand = Console.ReadLine();
        }
    }
}
