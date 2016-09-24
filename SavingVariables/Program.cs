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
            Command forprogram = new Command();
            VariableContext context = new VariableContext();
            VariableRepository repo = new VariableRepository(context);
           
            
            


            while (true)
            {
                string prompt = ">> ";
                string request = "What would you like to do?";
                Console.WriteLine(request);
                Console.Write(prompt);
                string userinput = Console.ReadLine().ToLower();


                if (newexpress.MatchCheck(userinput))
                {
                    var uservariable = newexpress.ExtractVariableAndValue(userinput);
                    newexpress.lastq = userinput;
                    repo.AddVariable(uservariable);
                    Console.WriteLine("Variable has now been saved");
                }
                else if (newexpress.CheckForVariableCall(userinput))
                {
                    var returnedVariable = repo.FindAndReturnVariable(userinput);
                    var returnedVariableValue = returnedVariable.Value;

                    Console.WriteLine(returnedVariableValue);
                }
                else if (newexpress.CheckForCommand(userinput))
                {
                    newexpress.CommandExtraction(userinput);
                   
                    if (userinput == "clear all" || userinput == "remove all" || userinput == "delete all")
                    {
                        //This will clear all the variables in the database.
                        repo.ClearAllVariables();
                        Console.WriteLine("Repo has now been cleared");
                    }
                    else if (userinput.Contains(newexpress.uservariable))
                    {
                        //Find the variable that needs to be removed and remove it.
                       var removed_variable = repo.FindAndReturnVariable(userinput);
                        repo.RemoveVariable(removed_variable);
                    }
                }
                else if (userinput == "exit" || userinput == "quit")
                {
                    Console.WriteLine("Bye");
                    Environment.Exit(1);
                }
                else Console.WriteLine("Please Enter a Valid Command");
            }
        }
    }
}
