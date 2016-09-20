using SavingVariables.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;



namespace SavingVariables.DAL
{
    public class VariableContext: DbContext
    {
        public virtual DbSet<Variables> Variables { get; set; }
    }
}
