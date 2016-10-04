using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables
{
    //This is the command class that does a program command depending on the userinput. 
    public class Command
    {
        public string clearAll = "clear all";
        public string deleteAll = "delete all";
        public string removeAll = "remove all";
        public string showAll = "show all";
        public string lastq = "lastq";
        public string MatchCommand(string firstcommand, string secondcommand)
        {
            if (firstcommand + " " + secondcommand == showAll)
            {
                return showAll;
            }
            else if (firstcommand + " " + secondcommand == clearAll)
            {
                return clearAll;
            }
            else if (firstcommand + " " + secondcommand == deleteAll)
            {
                return deleteAll;
            }
            else if (firstcommand + " " + secondcommand == removeAll)
            {
                return removeAll;
            }
            else if ((firstcommand == "clear" || firstcommand == "delete" || firstcommand == "remove") && (secondcommand.Count() == 1))
            { 
                return secondcommand;
            }
            else if (firstcommand + secondcommand == lastq)
            {
                return lastq;
            }
            else return "Not A Valid Command";
        }
        
        
    }
}
