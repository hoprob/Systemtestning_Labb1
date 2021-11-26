using System;
using System.Collections.Generic;
using System.Text;

namespace TeamHamsterBank
{
    class Customer : User
    {
        public Customer(string UserId, string FullName, string Password)
         :base(UserId, FullName, Password)
        {

        }
        int customerId;
        internal List<Account> _accounts = new List<Account>();

        public void CreateNewAccount()
        {
            Console.WriteLine("\t*** Skapa ett nytt konto ***\n");

            // Get account name
            Console.Write("Vänligen ange kontonamn: ");
            string accountName = Console.ReadLine().Trim();

            // Create a new account object and add to _accounts list
            Account newAccount = new Account(accountName);
            _accounts.Add(newAccount);

            Console.WriteLine($"\nNytt konto {accountName} har skapats.");
        }
    }
}
