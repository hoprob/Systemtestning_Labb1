using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TeamHamsterBank.Test
{
    [TestClass]
    public class Bank_VerifyCostumer_Test
    {
        Customer testCustomer;
        [TestInitialize]
        public void Initialize()
        {
            testCustomer = new Customer("123456", "Tester Test", "Test1234");
        }
        [TestMethod]
        public void VerifyCustomer_CorrectPassword_InputPassword_Test1234_Return_True()
        {
            //Arrange
            string inputPassword = "Test1234";
            //Act
            bool actual = Bank.VerifyCustomer(testCustomer, inputPassword);
            //Assert
            Assert.IsTrue(actual);
        }
        [TestMethod]
        [DataRow("Tesst12345")]
        [DataRow("test12345")]
        [DataRow("TEST12345")]
        [DataRow("Test12346")]
        public void VerifyCustomer_WrongPassword_InputPassword_FromDR_Return_false(string inputPassword)
        {
            //Arrange
            //Act
            bool actual = Bank.VerifyCustomer(testCustomer, inputPassword);
            //Assert
            Assert.IsFalse(actual);
        }
    }
}
