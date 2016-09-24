using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavingVariables.Models;
using SavingVariables.DAL;

namespace SavingVariables
{
    class Program
    {
        static void Main(string[] args)
        {
            Expression newexpress = new Expression();
            VariableContext context = new VariableContext();
            VariableRepository repo = new VariableRepository(context);

            
            


            while (true)
            {
                string prompt = ">> ";
                string request = "What would you like to do?";
                Console.WriteLine(request);
                Console.Write(prompt);
                string userinput = Console.ReadLine().ToLower();

                newexpress.CheckForVariableCall(userinput);
                newexpress.MatchCheck(userinput);
                newexpress.CheckForCommand(userinput);

                if (newexpress.MatchCheck(userinput) == true)
                {
                    var uservariable = newexpress.ExtractConstantAndValue(userinput);
                    newexpress.lastq = userinput;
                    repo.AddVariable(uservariable);
                }
                else if (newexpress.CheckForVariableCall(userinput) == true)
                {
                    var returnedVariableValue = repo.FindAndReturnVariableValue(userinput);

                    Console.WriteLine(returnedVariableValue);
                }
                else if (newexpress.CheckForCommand(userinput) == true)
                {
                    //Do a command
                }
                else Console.WriteLine("Please Enter a Valid Command");
            }
        }
    }
}
