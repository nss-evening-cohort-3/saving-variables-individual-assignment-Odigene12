using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using SavingVariables.Models;
using System.Collections.Generic;
using System.Linq;

namespace SavingVariables.Tests.DAL
{
    [TestClass]
    public class VariableTests
    {
        //Test to make sure a new instance of the DbSet is not null.
        [TestMethod]
        public void CanIMakeAnInstanceOfVariableClass()
        {
            Mock<DbSet<Variables>> newvariables = new Mock<DbSet<Variables>>();
            Assert.IsNotNull(newvariables);
        }
    }
}
