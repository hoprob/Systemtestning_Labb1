using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TeamHamsterBank.Test
{
    [TestClass]
    public class Bank_VerifyCostumer_Test
    {
        [TestMethod]
        public void VerifyCustomerCorrectPassword_InputPassword_Test1234_Return_True()
        {
            //Arrange
            Customer customer = new Customer("123456", "Tester Test", "Test1234");
            string inputPassword = "Test1234";
            //Act
            bool actual = Bank.VerifyCustomer(customer, inputPassword);
            //Assert
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void VerifyCustomerWrongPassword_InputPassword_Tesst12345_Return_false()
        {
            //Arrange
            Customer customer = new Customer("123456", "Tester Test", "Test1234");
            string inputPassword = "Tesst12345";
            //Act
            bool actual = Bank.VerifyCustomer(customer, inputPassword);
            //Assert
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void VerifyCustomerWrongPassword_LowerCase_InputPassword_test12345_Return_false()
        {
            //Arrange
            Customer customer = new Customer("123456", "Tester Test", "Test1234");
            string inputPassword = "test12345";
            //Act
            bool actual = Bank.VerifyCustomer(customer, inputPassword);
            //Assert
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void VerifyCustomerWrongPassword_UpperCase_InputPassword_TEST12345_Return_false()
        {
            //Arrange
            Customer customer = new Customer("123456", "Tester Test", "Test1234");
            string inputPassword = "TEST12345";
            //Act
            bool actual = Bank.VerifyCustomer(customer, inputPassword);
            //Assert
            Assert.IsFalse(actual);
        }
    }
}
