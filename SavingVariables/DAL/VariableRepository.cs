using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavingVariables.Models;

namespace SavingVariables.DAL
{
    public class VariableRepository
    {

        //create a private data member for your Db context to be injected into a contructor 
        private VariableContext context { get; }

        //This constructor will take the DbContext as an parameter and set it to a private data member that is an instance of your DbContext
        //This will make it to where the repository can put the work of adding, removing, etc on the DbContext instead of the repository itself.
        public VariableRepository (VariableContext mycontext)
        {
            context = mycontext;
        }

        //This method is designed to return all of my variables as a list
        public List<Variable> GetAllVariables()
        {
            //create a return statement that gets me all my variables.
            return context.Variables.ToList();
        }

        //Get the new instantiation of the Variable object and add it to the DbSet in your DbContext. Then save the changes.
        public void AddVariable(Variable uservariable)
        {
            context.Variables.Add(uservariable);
            context.SaveChanges();
        }

        public void RemoveVariable(Variable userVariable)
        {
            context.Variables.Remove(userVariable);
            context.SaveChanges();
        }

        public int FindAndReturnVariableValue(string uservariable)
        {
            //Get all of the variables in the list and return the value of the variable that matches the uservariable that the user commands.

            Variable found_variable = context.Variables.FirstOrDefault(v => v.VariableName.ToLower() == uservariable.ToLower());

            return found_variable.Value;
        }
    }
}
