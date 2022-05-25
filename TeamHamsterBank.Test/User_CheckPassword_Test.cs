
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
                new Customer("123457", "Does Thiswork", "Tesing987654"),
                new Customer("123458", "Whats Theoutcome", "guessthis1245"),
                new Customer("123459", "Isit Bugfree", "Secure321"),
                new Customer("123451", "Allabout Unittests", "HackMe2356"),
            };
        }
        [TestMethod]
        public void CheckPassword_CorrectUser_CorrectPassword_UserId_123456_Password_Test123_Return_User()
        {
            //Arrange
            string inputId = "123456";
            string inputPassword = "Test1234";
            var expected = _testUsers[0];
            //Act
            var actual = User.CheckPassword(_testUsers, inputId, inputPassword);
            //Assert
            Assert.AreSame(expected, actual);
        }
        [TestMethod]
        public void CheckPassword_CorrectUser_IncorrectPassword_UserId_123458_Password_WrongPW_Return_null()
        {
            //Arrange
            string inputId = "123458";
            string inputPassword = "WrongPW";
            //Act
            var actual = User.CheckPassword(_testUsers, inputId, inputPassword);
            //Assert
            Assert.IsNull(actual);
        }
        [TestMethod]
        public void CheckPassword_Wronguser_CorrectPassword_UserId_123954_Password_Test1234_Return_null()
        {
            //Arrange
            string inputId = "123954";
            string inputPassword = "Test1234";
            //Act
            var actual = User.CheckPassword(_testUsers, inputId, inputPassword);
            //Assert
            Assert.IsNull(actual);
        }
        [TestMethod]
        public void CheckPassword_CorrectUser_PasswordLowerCase_UserId_123459_Password_secure321_Return_False()
        {
            //Arrange
            string inputId = "123459";
            string inputPassword = "secure321";
            //Act
            var actual = User.CheckPassword(_testUsers, inputId, inputPassword);
            //Assert
            Assert.IsNull(actual);
        }
    }
}