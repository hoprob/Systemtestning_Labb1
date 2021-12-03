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
                if (_userId == account[5])
                {
                    _accounts.Add(new Account(account[0], account[1], account[2],
                        Decimal.Parse(account[3]), account[4], account[5]));
                }
            }
        }
        public void CreateNewAccount()
        {
            Console.WriteLine("\t*** Skapa ett nytt konto ***\n");

            // Options for account type
            Console.WriteLine("  Vänligen ange kontotyp:\n\n" +
                "  [1] Allkonto\n" +
                "  [2] Sparkonto\n" +
                "  [3] Framtidskonto\n" +
                "  [4] Investeringskonto\n");

            string accountType = String.Empty;
            bool rerunSelection;

            // Select account type
            do
            {
                Console.Write("\tKontotyp: ");
                Int32.TryParse(Console.ReadLine(), out int slctAccount);

                switch (slctAccount)
                {
                    case 1:
                        accountType = "Allkonto";
                        rerunSelection = false;
                        break;
                    case 2:
                        accountType = "Sparkonto";
                        rerunSelection = false;
                        break;
                    case 3:
                        accountType = "Framtidskonto";
                        rerunSelection = false;
                        break;
                    case 4:
                        accountType = "Investeringskonto";
                        rerunSelection = false;
                        break;
                    default:
                        Console.WriteLine("\n  Ogiltligt val. Vänligen ange nummer igen.\n");
                        rerunSelection = true;
                        break;
                }
            } while (rerunSelection);

            Console.Clear();
            Console.WriteLine($"\n  {accountType.Trim()} har valts.");
            // Get name for new account
            string accountName = String.Empty;
            do
            {
                Console.Write("\n  Vänligen ange kontonamn: ");
                accountName = Console.ReadLine().Trim();

                if (accountName.Length > 15)
                {
                    Console.WriteLine("  Kontonamnet är för långt.");
                }
            } while (accountName.Length > 15);

            // Get currency for new account
            Console.Write("\n  Vänligen ange valuta för kontot: ");
            string inputCurrency = string.Empty;
            string currency = String.Empty;
            do
            {
                inputCurrency = Console.ReadLine().Trim().ToUpper(); // Input currency

                if (inputCurrency.Length != 3 || !( Account.CurrencyList.Exists( e => e[0].Contains(inputCurrency)) ))
                {
                    Console.Clear();
                    Console.WriteLine("\n  Oglitligt val. Vänligen ange valuta med tre bokstäver.\n");
                    Account.PrintCurrencies();
                    Console.Write("\n  Välj: ");
                }
                else
                {
                    currency = inputCurrency; // Set currency
                }
            } while (inputCurrency.Length != 3 || !(Account.CurrencyList.Exists(e => e[0].Contains(inputCurrency))));

            // Create a new account object and add to _accounts list
            Account newAccount = new Account(accountName, accountType, currency, _userId);
            _accounts.Add(newAccount);

            // Print details of new account
            Console.Clear();
            Console.WriteLine($"\n  Nytt {accountType.Trim()}, {accountName}, har skapats med valuta [{currency}].");
        }
        
    }
}
