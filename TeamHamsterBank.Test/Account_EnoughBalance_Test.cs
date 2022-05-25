using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeamHamsterBank.Test
{
    [TestClass]
    public class Account_EnoughBalance_Test
    {
        Account sut;
        [TestInitialize]
        public void Initialize()
        {
            sut = new Account("TestAccount", "test", "SEK", "123456");
        }
        [TestMethod]
        [DataRow(20000, 201)]
        [DataRow(0.23, 0.22)]
        [DataRow(100.52, 100.52)]
        [DataRow(1000625.62, 926452.25)]
        public void EnoughBalance_EnoughBalanceOnAccount_Balance_FromDR_CheckSum_FromDR_Return_True(double balanceDouble, double checkSumDouble)
        {
            //Arrange
            sut.Balance = Convert.ToDecimal(balanceDouble);
            decimal checkSum = Convert.ToDecimal(checkSumDouble);
            //Act
            var actual = sut.EnoughBalance(checkSum);
            //Arrange
            Assert.IsTrue(actual);
        }
        [TestMethod]
        [DataRow(201, 402)]
        [DataRow(-3, 5)]
        [DataRow(0.22, 0.23)]
        [DataRow(400000.2, 4080000.3)]
        public void EnoughBalance_NotEnoughBalanceOnAccount_Balance_FromDR_CheckSum_FromDR_Return_False(double balanceDouble, double checkSumDouble)
        {
            //Arrange
            decimal checkSum = Convert.ToDecimal(checkSumDouble);
            sut.Balance = Convert.ToDecimal(balanceDouble);
            //decimal checkSum = 402;
            //Act
            var actual = sut.EnoughBalance(checkSum);
            //Arrange
            Assert.IsFalse(actual);
        }
    }
}
