using System;
using System.Collections.Generic;
using System.Text;

namespace TeamHamsterBank
{
    class Customer : User
    {
        internal List<Account> _accounts = new List<Account>();
        public Customer(string UserId, string FullName, string Password)
         : base(UserId, FullName, Password)
        {
            SortOutDetails(StoreAndLoad.AccountFile);

        }
        public void SortOutDetails(List<string[]> accountsFile)
        {
            foreach (string[] account in accountsFile)
            {
                if (_userId == account[4])
                {
                    _accounts.Add(new Account(account[0],
                    Decimal.Parse(account[2]), account[3], account[4]));
                }
            }
        }
        public void CreateNewAccount()
        {
            Console.WriteLine("\t*** Skapa ett nytt konto ***\n");

            // Options for account name
            Console.WriteLine("  Vänligen ange kontotyp:\n\n" +
                "  [1] Allkonto\n" +
                "  [2] Sparkonto\n" +
                "  [3] Framtidskonto\n" +
                "  [4] Investeringskonto\n");

            string accountName = String.Empty;
            bool rerunSelection;

            // Select account name
            do
            {
                Console.Write("\tKontotyp: ");
                Int32.TryParse(Console.ReadLine(), out int slctAccount);

                switch (slctAccount)
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
                        Console.WriteLine("  Ogiltligt val. Vänligen ange nummer igen.\n");
                        rerunSelection = true;
                        break;
                }
            } while (rerunSelection);

            Console.Clear();
            Console.WriteLine($"\n  {accountName.Trim()} har valts.");
            Console.Write("\n  Vänligen ange kontonamn: ");
            string nameAccount = Console.ReadLine().Trim();

            // Options for account name
            Console.WriteLine("\n  Vänligen ange valuta:\n\n" +
                "  [1] [SEK]\n" +
                "  [2] [EUR]\n" +
                "  [3] [GBP]\n" +
                "  [4] [USD]\n");

            string currency = String.Empty;

            do
            {
                Console.Write("\tValuta: ");
                Int32.TryParse(Console.ReadLine(), out int slctCurrency);

                switch (slctCurrency)
                {
                    case 1:
                        currency = "[SEK]";
                        rerunSelection = false;
                        break;
                    case 2:
                        currency = "[EUR]";
                        rerunSelection = false;
                        break;
                    case 3:
                        currency = "[GBP]";
                        rerunSelection = false;
                        break;
                    case 4:
                        currency = "[USD]";
                        rerunSelection = false;
                        break;
                    default:
                        Console.WriteLine("Ogiltligt val. Vänligen ange nummer igen.\n");
                        rerunSelection = true;
                        break;
                }
            } while (rerunSelection);

            // Create a new account object and add to _accounts list
            Account newAccount = new Account(accountName, currency, _userId);
            _accounts.Add(newAccount);

            Console.Clear();
            Console.WriteLine($"\nNytt {accountName.Trim()}, {nameAccount}, har skapats med valuta {currency}.");
        }
        
    }
}
