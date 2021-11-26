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
            Console.WriteLine("Vänligen ange kontonamn:\n" +
                "  [1] Allkonto\n" +
                "  [2] Sparkonto\n" +
                "  [3] Framtidskonto\n" +
                "  [4] Invesesteringskonto\n");

            string accountName = String.Empty;
            bool rerunSelection;

            do
            {
                Console.Write("Välj kontonamn: ");
                Int32.TryParse(Console.ReadLine(), out int select);

                switch (select)
                {
                    case 1:
                        accountName = "Allkonto         ";
                        rerunSelection = false;
                        break;
                    case 2:
                        accountName = "Sparkonto        ";
                        rerunSelection = false;
                        break;
                    case 3:
                        accountName = "Framtidskonto    ";
                        rerunSelection = false;
                        break;
                    case 4:
                        accountName = "Investeringskonto";
                        rerunSelection = false;
                        break;
                    default:
                        Console.WriteLine("Ogiltligt val. Vänligen ange nummer igen.\n");
                        rerunSelection = true;
                        break;
                }
            } while (rerunSelection);

            // Create a new account object and add to _accounts list
            Account newAccount = new Account(accountName);
            _accounts.Add(newAccount);

            Console.WriteLine($"\nNytt {accountName.Trim()} har skapats.");
        }
    }
}
