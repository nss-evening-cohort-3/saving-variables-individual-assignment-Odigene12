using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables.Models
{
    public class Variable
    {
        //This is the primary key for the Model
        [Key]
        public int VariableId { get; set; }

        //The variable is going to be a character since variables are not words.
        public char VariableName { get; set; }

        //This is the value of the variable as an integer.
        public int Value { get; set; }
    }
}
