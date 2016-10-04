using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SavingVariables.Models;
using System.Threading.Tasks;
using SavingVariables.DAL;
using System.Data.Entity;

namespace SavingVariables
{
    public class Expression
    {
        Variable forexpress = new Variable();
        Command storeexpression = new Command();


        public string lastq { get; set; }
        //Check to make sure the user is inputing the expression correctly
        public bool MatchCheck(string input)
        {
            //Create the pattern that the user must follow when entering his or her expression
            string pattern = @"^([a-zA-z]{1}\s*[=]\s*\d+)$";
            Match thismatch = Regex.Match(input, pattern);

            if (thismatch.Success)
            {
                return true;
            }

            else return false;

        }

        public bool CheckForVariableCall(string input)
        {
            //Create the pattern that the user must follow when entering his or her expression
            string wordpattern = @"^([a-zA-z]{1})$";
            Match wordmatch = Regex.Match(input, wordpattern);

            if (wordmatch.Success)
            {
                return true;
            }
            else return false;

        }

        public bool ClearRepoCheck(string usercommand)
        {

            if (usercommand == "clear all" || usercommand == "remove all" || usercommand == "delete all")
            {
                return true;
                //clear List of variables.
            } else return false;
            
        }

        public bool CheckForLastq(string usercommand)
        {
            if (usercommand == "lastq")
            {
                return true;

            }
            else return false;
        }

        public bool CheckToShowVariables(string usercommand)
        {
            if (usercommand == "show all")
            {
                return true;

            }
            else return false;
        }

        public bool CheckForClearVariableCommand(string usercommand)
        {
         
            if (usercommand == "clear" + " " + uservariable || usercommand == "delete" + " " + uservariable || usercommand == "remove" + " " + uservariable)
            {
                /*do a condition for evaluating if the user wants to clear variable from data.*/
                return true;
            }
            else return false;
        }

        public char ReturnVariable(string userinput)
        {
            CommandExtraction(userinput);
            if (storeexpression.MatchCommand(firstword, secondword) == secondword)
            {
                uservariable = System.Convert.ToChar(secondword);
                return uservariable;

            }
            else return ' ';

        }

        public string CommandExtraction(string userexpression)
        {
                string commandPattern = @"^(?<command>[\w]+)\s*(?<command2>[\w]+)$";
                Match matchcommand = Regex.Match(userexpression, commandPattern);
                firstword = matchcommand.Groups["command"].Value;
                secondword = matchcommand.Groups["command2"].Value;
                string command = firstword + " " + secondword;
                storeexpression.MatchCommand(firstword, secondword);
            return command;
        }


        //Use a Regex expression to make sure you extract the variable and value in each user expression and attach them to the variable and value property of the DbSet.
        public Variable ExtractVariableAndValue(string input)
        {
            
                string variableExpressionPattern = @"^(?<variable>[a-zA-Z]{1})\s*(?<operator>[=])\s*(?<variablevalue>\-*[\d]+)$";
                Match matchvariable = Regex.Match(input, variableExpressionPattern);
                variable = matchvariable.Groups["variable"].Value;
                variableValue = Convert.ToInt32(matchvariable.Groups["variablevalue"].Value);
                forexpress.VariableName = variable;
                forexpress.Value = variableValue;
            return forexpress;       
        }
        
        public string ExtractVariableForCall(string input)
        {
            string commandForVariablePattern = @"^(?<variable>[a-zA-z]{1})$";
            Match matchvariable = Regex.Match(input, commandForVariablePattern);
            variable = matchvariable.Groups["variable"].Value;
            return variable;

        }


        //public variables to use to save constants and their values
        public string variable { get; set; }

        public int variableValue { get; set; }

        public string firstword { get; set; }
        public string secondword { get; set; }

        public char uservariable { get; set; }
    }
}
