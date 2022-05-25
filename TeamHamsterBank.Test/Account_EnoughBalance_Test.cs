using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeamHamsterBank.Test
{
    [TestClass]
    public class Account_EnoughBalance_Test
    {
        [TestMethod]
        public void EnoughBalance_EnoughBalanceOnAccount_Balance_20000_CheckSum_201_Return_True()
        {
            //Arrange
            Account sut = new Account("TestAccount", "test", "SEK", "123456") { Balance = 20000 };
            decimal checkSum = 201;
            //Act
            var actual = sut.EnoughBalance(checkSum);
            //Arrange
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void EnoughBalance_NotEnoughBalanceOnAccount_Balance_201_CheckSum_402_Return_False()
        {
            //Arrange
            Account sut = new Account("TestAccount", "test", "SEK", "123456") { Balance = 201 };
            decimal checkSum = 402;
            //Act
            var actual = sut.EnoughBalance(checkSum);
            //Arrange
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void EnoughBalance_MinusBalance_Balance_minus3_CheckSum_5_Return_False()
        {
            //Arrange
            Account sut = new Account("TestAccount", "test", "SEK", "123456") { Balance = -3 };
            decimal checkSum = 5;
            //Act
            var actual = sut.EnoughBalance(checkSum);
            //Arrange
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void EnoughBalance_MinusCheckSum_Balance_200_CheckSum_minus5_Return_False()
        {
            //Arrange
            Account sut = new Account("TestAccount", "test", "SEK", "123456") { Balance = 200 };
            decimal checkSum = -5;
            //Act
            var actual = sut.EnoughBalance(checkSum);
            //Arrange
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void EnoughBalance_EnoughBalanceWithDecimals_Balance_0_23_CheckSum_0_22_Return_True()
        {
            //Arrange
            Account sut = new Account("TestAccount", "test", "SEK", "123456") { Balance = 0.23m };
            decimal checkSum = 0.23m;
            //Act
            var actual = sut.EnoughBalance(checkSum);
            //Arrange
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void EnoughBalance_NotEnoughBalanceWithDecimals_Balance_0_22_CheckSum_0_23_Return_True()
        {
            //Arrange
            Account sut = new Account("TestAccount", "test", "SEK", "123456") { Balance = 0.22m };
            decimal checkSum = 0.23m;
            //Act
            var actual = sut.EnoughBalance(checkSum);
            //Arrange
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void EnoughBalance_EnoughBalanceSameNumber_Balance_100_52_CheckSum_100_52_Return_True()
        {
            //Arrange
            Account sut = new Account("TestAccount", "test", "SEK", "123456") { Balance = 100.52m };
            decimal checkSum = 100.52m;
            //Act
            var actual = sut.EnoughBalance(checkSum);
            //Arrange
            Assert.IsTrue(actual);
        }
    }
}
