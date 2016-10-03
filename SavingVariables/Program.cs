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

                bool CheckExpression = newexpress.MatchCheck(userinput);
                bool CheckVariableCall = newexpress.CheckForVariableCall(userinput);
                bool CheckClearRepo = newexpress.ClearRepoCheck(userinput);
                bool CheckLastq = newexpress.CheckForLastq(userinput);
                bool CheckShowVariables = newexpress.CheckToShowVariables(userinput);
               char variable = newexpress.ReturnVariable(userinput);
                newexpress.CommandExtraction(userinput);

           
                if (newexpress.CheckForClearVariableCommand(userinput))
                {
                    //Find the variable that needs to be removed and remove it.
                    var removed_variable = repo.FindAndReturnVariable(variable);
                    repo.RemoveVariable(removed_variable);
                    Console.WriteLine(newexpress.uservariable + " has been removed");
                }
                else if (CheckExpression == true)
                {
                    var uservariable = newexpress.ExtractVariableAndValue(userinput);
                    newexpress.lastq = userinput;
                    List<Variable> ContainedVariables =  repo.GetAllVariables();
                     if (ContainedVariables.Contains(uservariable))
                    {
                        Console.WriteLine("Error! " + uservariable.VariableName + " is already defined!");
                    } 
                        repo.AddVariable(uservariable);
                        Console.WriteLine("Variable " + uservariable.VariableName + " has now been saved");
                                   
                }
                else if (CheckVariableCall == true)
                {
                    var returnedVariable = repo.FindAndReturnVariable(userinput);
                    if (returnedVariable == null)
                    {
                        Console.WriteLine("This variable has not been assigned");
                    }
                    else
                    {
                        var returnedVariableValue = returnedVariable.Value;
                        Console.WriteLine(returnedVariableValue);
                    }

                }
                else if (CheckShowVariables == true)
                {
                    repo.ShowVariables();
                }
                else if (CheckClearRepo == true)
                    {
                    
                        //This will clear all the variables in the database.
                        repo.ClearAllVariables();
                        Console.WriteLine("Repo has now been cleared");
                    }
                else if (CheckLastq == true)
                    {
                        Console.WriteLine(newexpress.lastq);
                    }
                
                else if (userinput == "exit" || userinput == "quit")
                {
                    Console.WriteLine("Bye");
                    Environment.Exit(1);
                }
                else
                {
                    newexpress.lastq = userinput;
                    Console.WriteLine("Please Enter a Valid Command");
                }
            }
        }
    }
}
