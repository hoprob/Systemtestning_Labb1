using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Art = TeamHamsterBank.HamsterArt;

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
            Bank.ReturnInstruction(2);
            Art.HeadLine("\t*** Skapa ett nytt konto ***\n");

            // Options for account type
            Console.WriteLine(" \n\nVänligen ange kontotyp:\n\n" +
                "  [1] Allkonto\n" +
                "  [2] Sparkonto\n" +
                "  [3] Framtidskonto\n" +
                "  [4] Investeringskonto\n" +
                "  [5] Kreditkonto\n");

            string accountType = String.Empty;
            string input;
            bool rerunSelection;
<<<<<<< HEAD
=======
            bool accepted = false; // Accept for credit account

>>>>>>> d67653aaa4cf13bbb3438f3ef88767c9a88d8d18
            // Select account type
            do
            {
                Console.Write("\tKontotyp: ");
                Int32.TryParse(input = Console.ReadLine(), out int slctAccount);
                if (input.Trim().ToUpper() == "R")
                {
                    return;
                }

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
                    case 5:
                        accountType = "Kreditkonto";

                        Console.Clear();
                        Console.CursorVisible = false;
                        Console.WriteLine("\n  Vänligen vänta medan vi genomför vår kreditupplysning.");
                        for (int i = 0; i < 35; i++)
                        {
                            Console.SetCursorPosition(i + 10, 2);
                            Console.Write(".");
                            Thread.Sleep(200);
                        }
                        Console.CursorVisible = true;
                        Console.CursorVisible = false;

                        Random rnd = new Random();
                        int num = rnd.Next(1, 3);
                        Console.Clear();
                        if (num == 1)
                        {
                            Console.WriteLine("\n  Du är inte beviljad ett kreditkonto hos vår bank.\n\n");
                            accepted = false;
                        }
                        else
                        {
                            Console.WriteLine("\n  Du är beviljad ett kreditkonto.\n\n" +
                            "  Vänligen klicka Enter för att fortsätta.");
                            Console.ReadKey();
                            accepted = true;
                        }
                        rerunSelection = false;
                        break;
                    default:
                        Console.WriteLine("\n  Ogiltligt val. Vänligen ange nummer igen.\n");
                        rerunSelection = true;
                        break;
                }
            } while (rerunSelection);

            if (accepted)
            {
                Console.Clear();
                Console.WriteLine($"\n  {accountType.Trim()} har valts.");
                // Get name for new account
                string accountName = String.Empty;
                do
                {
                    Console.Write("\n  Vänligen ange kontonamn: ");
                    accountName = Console.ReadLine().Trim();

                    if (accountName.Length > 20)
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
                switch (accountType)
                {
                    case "Allkonto":
                        MainAccount newMAccount = new MainAccount(accountName, accountType, currency, _userId);
                        _accounts.Add(newMAccount);
                        break;
                    case "Sparkonto":
                        SavingsAccount newSAccount = new SavingsAccount(accountName, accountType, currency, _userId);
                        _accounts.Add(newSAccount);

                        Console.Clear(); // Prints an example of how much the money will be worth with interest
                        SavingsAccount.CalculateSavingsInterest(1000, 0.5, true, currency); // 6 months
                        SavingsAccount.CalculateSavingsInterest(1000, 1, false, currency); // 1 year
                        SavingsAccount.CalculateSavingsInterest(1000, 5, false, currency); // 5 years
                        SavingsAccount.CalculateSavingsInterest(1000, 10, false, currency); // 10 years
                        Console.WriteLine("\n  Tryck Enter för att fortsätta");
                        Console.ReadKey();
                        break;
                    case "Framtidskonto":
                        FutureAccount newFAccount = new FutureAccount(accountName, accountType, currency, _userId);
                        _accounts.Add(newFAccount);
                        break;
                    case "Investeringskonto":
                        InvestmentAccount newIAccount = new InvestmentAccount(accountName, accountType, currency, _userId);
                        _accounts.Add(newIAccount);
                        break;
                    case "Kreditkonto":
                        CreditAccount newCAccount = new CreditAccount(accountName, accountType, currency, _userId);
                        _accounts.Add(newCAccount);
                        break;
                    default:
                        break;
                }

                // Print details of new account
                Console.Clear();
                Console.WriteLine($"\n  Nytt {accountType.Trim()}, {accountName}, har skapats med valuta [{currency}].");
            }
        }
    }
}
