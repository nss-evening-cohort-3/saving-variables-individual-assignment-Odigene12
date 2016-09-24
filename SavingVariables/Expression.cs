using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SavingVariables.Models;
using System.Threading.Tasks;
using SavingVariables.DAL;

namespace SavingVariables
{
    public class Expression
    {
        Variable forexpress = new Variable();
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

        public bool CheckForCommand(string userinput)
        {
            char userCharacter;

            if (userinput == "quit" || userinput == "exit")
            {
                Environment.Exit(1);
            }
            else if (userinput == "clear all" || userinput == "remove all" || userinput == "delete all")
            {
                //clear List of variables.
            }
            else if (userinput == "show all")
            {
                //get all variables, loop through them and write the variables and their values in the console. 
            }
            else if ()
            {
                /*do a condition for evaluating if the user wants to clear variable from data.*/
            }
            else if (userinput == "lastq")
            {
                return true;
            }
            else return false;
        }

        public Variable ExtractConstantAndValue(string input)
        {
            if (MatchCheck(input) == true)
            {
                string variableExpressionPattern = @"^(?<variable>[a-zA-Z]{1})\s*(?<operator>[=])\s*(?<variablevalue>\-*[\d+])$";
                Match matchvariable = Regex.Match(input, variableExpressionPattern);
                variable = matchvariable.Groups["variable"].Value;
                variableValue = Convert.ToInt32(matchvariable.Groups["variablevalue"].Value);
                forexpress.VariableName = variable;
                forexpress.Value = variableValue;
                return forexpress;
            }
            else if (CheckForVariableCall(input) == true)
            {
                string commandForVariablePattern = @"^(?<variable>[a-zA-z]{1})$";
                Match matchvariable = Regex.Match(input, commandForVariablePattern);
                variable = matchvariable.Groups["variable"].Value;
                return forexpress;
            }
            else return null;

        }


        //public variables to use to save constants and their values
        public string variable { get; set; }

        public int variableValue { get; set; }

    }
}
