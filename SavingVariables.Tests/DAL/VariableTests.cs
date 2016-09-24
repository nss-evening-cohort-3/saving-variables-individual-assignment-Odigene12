using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using SavingVariables.Models;
using System.Collections.Generic;
using System.Linq;
using SavingVariables.DAL;


namespace SavingVariables.Tests.DAL
{
    [TestClass]
    public class VariableTests
    {
        //Create a private data member for your DbContext for testing.
        private Mock<VariableContext> myContext { get; set; }

        //Create your Mock DbSets that will be used for testing.
        Mock<DbSet<Variable>> mockVariablesTable { get; set; }

        //Create a list for your DbSet that will be used for testing.
        List<Variable> variableList = new List<Variable>()
        {
            new Variable {VariableName = "a", Value = 1, VariableId = 1 }
        };

        public void ConnectingToDatabase()
        {
            var queryVariables = variableList.AsQueryable();

            //Lie to LINQ make it think that our new Queryable List is a Database table.
            mockVariablesTable.As<IQueryable<Variable>>().Setup(m => m.Provider).Returns(queryVariables.Provider);
            mockVariablesTable.As<IQueryable<Variable>>().Setup(m => m.Expression).Returns(queryVariables.Expression);
            mockVariablesTable.As<IQueryable<Variable>>().Setup(m => m.ElementType).Returns(queryVariables.ElementType);
            mockVariablesTable.As<IQueryable<Variable>>().Setup(m => m.GetEnumerator()).Returns(() => (queryVariables.GetEnumerator()));

            //Here, I am setting up the Mock Context to return my DbSet.
            myContext.Setup(c => c.Variables).Returns(mockVariablesTable.Object);

            //This says that whenever the "Add" method is called on a my Mock DbSet ("mockVariablesTable") then call then it should be adding to my variable list and do the adding.
            mockVariablesTable.Setup(t => t.Add(It.IsAny<Variable>())).Callback((Variable letter) => variableList.Add(letter));

            //Tell the repo what to do when it sees a remove method on the DbSet
            mockVariablesTable.Setup(t => t.Remove(It.IsAny<Variable>())).Callback((Variable letter) => variableList.Remove(letter));

        }

        [TestInitialize]
        public void Setup()
        {
            //Set up my mocks so as to not clutter the actual unit tests. In other words, have this already established so that the unit tests do not always have to do it.
            myContext = new Mock<VariableContext>();
            mockVariablesTable = new Mock<DbSet<Variable>>();
            //Here you are taking your list of Variables 
            ConnectingToDatabase();

        }


        //Test to make sure a new instance of the DbSet is not null.
        [TestMethod]
        public void CanIMakeAnInstanceOfVariableClass()
        {
            Mock<DbSet<Variable>> newvariables = new Mock<DbSet<Variable>>();
            Assert.IsNotNull(newvariables);
        }

        [TestMethod]
        public void CanIMakeANewRepository()
        {
            VariableRepository repo = new VariableRepository(myContext.Object);
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void CanIGetAllVariables()
        {
            //Arrange
            var repo = new VariableRepository(myContext.Object);//my Mock context

            //Act
            var actualVariables = repo.GetAllVariables();

            //Assert
            Assert.AreEqual(actualVariables.Count, 1); //I have only one in the test data created

        }

        [TestMethod]
        public void CanIAddVariable()
        {
            //Arrange
            var repo = new VariableRepository(myContext.Object);//my Mock context
            //Act

            //Create a new variable object to add to list.
            Variable newvariable = new Variable { VariableId = 2, VariableName = "b", Value = 15 };
            repo.AddVariable(newvariable);
            //Assert
            Assert.AreEqual(variableList.Count, 2);
            mockVariablesTable.Verify(x => x.Add(newvariable), Times.Once); //Moq Testing to make sure it does not keep adding
            myContext.Verify(x => x.SaveChanges(), Times.Once); //Moq Testing to make sure it saves only once.
            Assert.IsTrue(variableList.Contains(newvariable));
        }

        [TestMethod]
        public void CanIRemoveAVariable()
        {
            //Arrange
            var repo = new VariableRepository(myContext.Object);//my Mock context
            var testVariable = new Variable { VariableId = 2, VariableName = "b", Value = 14 };
            repo.AddVariable(testVariable);
            //Act
            repo.RemoveVariable(testVariable);
            //Assert 
            Assert.IsFalse(variableList.Contains(testVariable));
        }

        [TestMethod]
        public void CanIGetASingleVariableValue()
        {
            //Arrange
            var repo = new VariableRepository(myContext.Object);//my Mock context
            var testVariable1 = new Variable { VariableId = 2, VariableName = "b", Value = 45 };
            var testVariable2 = new Variable { VariableId = 3, VariableName = "c", Value = 55 };
            var testVariable3 = new Variable { VariableId = 4, VariableName = "d", Value = 75 };

            string uservariable = "c";
            //Act
            repo.AddVariable(testVariable1);
            repo.AddVariable(testVariable2);
            repo.AddVariable(testVariable3);

            //locate the variable you need and return the value
            int returnValue = repo.FindAndReturnVariableValue(uservariable);

            //Assert
            Assert.AreEqual(55, returnValue);
        }
    }
}
