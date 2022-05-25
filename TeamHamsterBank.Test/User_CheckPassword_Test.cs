
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeamHamsterBank.Test
{
    [TestClass]
    public class User_CheckPassword_Test
    {
        List<User> _testUsers;

        [TestInitialize]
        public void Initialize()
        {
            _testUsers = new List<User>()
            {
                new Customer("123456", "Test Testsson", "Test1234"),
                new Customer("123457", "Does Thiswork", "Testing987654"),
                new Customer("123458", "Whats Theoutcome", "Guessthis1245"),
                new Customer("123459", "Isit Bugfree", "Secure321"),
                new Customer("123451", "Allabout Unittests", "HackMe2356"),
            };
        }
        [TestMethod]
        [DataRow("123456", "Test1234", 0)]
        [DataRow("123457", "Testing987654", 1)]
        [DataRow("123458", "Guessthis1245", 2)]
        [DataRow("123459", "Secure321", 3)]
        [DataRow("123451", "HackMe2356", 4)]
        public void CheckPassword_CorrectUser_CorrectPassword_UserId_FromDR_Password_FromDR_Return_User(string inputId, string inputPassword, int index)
        {
            //Arrange
            var expected = _testUsers[index];
            //Act
            var actual = User.CheckPassword(_testUsers, inputId, inputPassword);
            //Assert
            Assert.AreSame(expected, actual);
        }
        [TestMethod]
        [DataRow("WrongPW")]
        [DataRow("guessthis1245")]
        [DataRow("GUESSTHIS1245")]
        [DataRow("Guessthis12456")]
        public void CheckPassword_CorrectUser_IncorrectPassword_UserId_123458_Password_FromDR_Return_null(string inputPassword)
        {
            //Arrange
            string inputId = "123458";
            //Act
            var actual = User.CheckPassword(_testUsers, inputId, inputPassword);
            //Assert
            Assert.IsNull(actual);
        }
        [TestMethod]
        [DataRow("123456D", "Test1234")]
        [DataRow("123457D", "Testing987654")]
        [DataRow("123451D", "HackMe2356")]
        public void CheckPassword_Wronguser_CorrectPassword_UserId_FromDR_Password_FromDR_Return_null(string inputId, string inputPassword)
        {
            //Arrange
            //Act
            var actual = User.CheckPassword(_testUsers, inputId, inputPassword);
            //Assert
            Assert.IsNull(actual);
        }
    }
}