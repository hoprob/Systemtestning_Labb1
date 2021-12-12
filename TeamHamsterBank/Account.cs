using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TeamHamsterBank
{
    class Account
    {
        private string _accountType = "New Account";
        private string _accountName = "Konto";
        private decimal _balance;
        internal decimal Balance
        {
            set
            {
                _balance = value;
            }
            get => _balance;
        }
        private string _currency;
        private static int _staticAccountNum = 100000000;
        private int _accountNum;
        internal string Currency { get => _currency;}
        internal string AccountType { get => _accountType; }
        internal int AccountNumber { get  => _accountNum;  }
        private string _customerID;
        internal string CustomerID { get => _customerID; }

        internal static List<string[]> CurrencyList = new List<string[]>
        {
            new string[] { "SEK", "1.0" },
            new string[] { "EUR", "10.23" },
            new string[] { "USD", "9.03" },
            new string[] { "GBP", "12.03" },
            new string[] { "CAD", "7.08" },
            new string[] { "JPY", "0.080" },
            new string[] { "CHF", "9.82" },
            new string[] { "AUD", "6.46" },
        }; // List of available currencies in the bank

        private List<string[]> _transaction = new List<string[]>();
        //This Constructor will only be called when the app starts to
        // declare the accounts that already exist.
        public Account(string accountName, string accountType, decimal balance, string currency, string customerID)
        {
            _staticAccountNum++;
            _accountName = accountName;
            _accountType = accountType;
            _accountNum = _staticAccountNum;
            _balance = balance;
            _currency = currency;
            _customerID = customerID;
        }
        public Account(string accountName, string accountType, string accountNum,
                        decimal balance, string currency, string customerID)
        {
            _staticAccountNum++;
            _accountName = accountName;
            _accountType = accountType;
            _accountNum = int.Parse(accountNum);
            _balance = balance;
            _currency = currency;
            _customerID = customerID;
        }
        // This constructer will be called by the customer-object to open a new accounnt
        public Account(string accountName, string accountType, string currency, string customerID)
        {
            _staticAccountNum++;
            _accountName = accountName;
            _accountType = accountType;
            _accountNum = _staticAccountNum;
            _balance = 0.00m;
            _currency=currency;
            _customerID = customerID;
        }
        public string ToSave()
        {

            return $"{_accountName}________{_accountType.Trim()}________{_accountNum}________{_balance}" +
                   $"________{_currency}________{_customerID}\n";
        }
        public override string ToString()
        {
            return $"{_accountName}    Kontotyp: [{_accountType}]    KontoNr: [{_accountNum}]" +
                   $"    Saldo: {_balance:0.00}    [{_currency}]\n\n";
        }
        public static string PrintAccounts(Customer customer)
        {
            StringBuilder output = new StringBuilder();
            string accountName = string.Empty;
            string accountType = string.Empty;
            
            output.Append("\t╔═══╤═══════════╤══════════════╤════════════╤════════════════════╤══════╗\n" +
                          "\t║Val│Kontonummer│    Namn      │  Kontotyp  │        Saldo       │Valuta║\n" +
                          "\t╠═══╪═══════════╪══════════════╪════════════╪════════════════════╪══════╣\n");
            for (int i = 0; i < customer._accounts.Count; i++)
            {
                accountName = customer._accounts[i]._accountName;
                accountType = customer._accounts[i]._accountType;
                string balanceOutput = $"{customer._accounts[i]._balance:0.00}";
                if ((accountName = customer._accounts[i]._accountName).Length > 14)
                    accountName = accountName.Remove(14);
                if ((accountType = customer._accounts[i]._accountType).Length > 12)
                    accountType = accountType.Remove(12);
                output.Append(String.Format("\t║{0,-3}│{1,-11}│{2,-14}│{3,-12}│{4,20}│{5,-6}║\n",
                    i + 1, customer._accounts[i]._accountNum, accountName, accountType,
                    balanceOutput, customer._accounts[i]._currency));
                output.Append("\t╟───┼───────────┼──────────────┼────────────┼────────────────────┼──────╢\n");
            }
            output.Append("\t╚═══╧═══════════╧══════════════╧════════════╧════════════════════╧══════╝");
            return $"\n\n{output.ToString()}";
        }
        public static void SelectAccount(Customer customer, int numberOfAccounts)
        {
            if (customer._accounts.Count == 0)
            {
                Console.WriteLine("\n\n\t\tDu har inget registrerat konto." +
                                        "  Var god och öppna ett nytt konto");
                return;
            }
            Console.Write("\n\tVälj ett konto och tryck 'Enter' eller tryck 'Enter' två gångar: ");
            Int32.TryParse(Console.ReadLine(), out int accountIndex);
            if ( accountIndex <= numberOfAccounts && accountIndex > 0 )
            {
                Console.Clear();
                Console.WriteLine("\n\n\n\t"+customer._accounts[accountIndex -1].PrintAccountHistory());
            }
        }
        public string PrintAccountHistory()
        {
            string output = String.Empty;
            foreach (string[] transaction in _transaction)
            {
                                    //  Value     ,      Date       
                output += $"\n\n\t\t{transaction[0]}\t\t{transaction[1]}\n\n";
            }
            return output;
        }
        public static void SubmitTransaction(Customer customer, int accountIndex, decimal value)
        {
            DateTime date = DateTime.Now;
            customer._accounts[accountIndex]._transaction.Add(
                new string[] { value.ToString(), date.ToString() }); 
        }

        public bool EnoughBalance(decimal checkSum)
        {
            return checkSum <= _balance;
        }
        public void MakeTransfer(decimal transferSum, Account toAccount)
        {

            this._balance -= transferSum;
            Bank.ExchangeCurrency( ref transferSum, ref _currency);
            Bank.ExchangeBack(ref transferSum, ref toAccount._currency);
            toAccount._balance += transferSum;
        }
        public void MakeExternalTransfer(decimal transferSum, Account toAccount)
        {

            this._balance -= transferSum;
            Bank.ExchangeCurrency(ref transferSum, ref _currency);
            Bank.ExchangeBack(ref transferSum, ref toAccount._currency);
            Bank.UpcomingTransactions.Add(new Task(() =>
            {
                toAccount._balance += transferSum;

            }));

        }
        public static void PrintCurrencies() // Prints available currencies in the bank
        {
            Console.WriteLine("  * Tillgängliga valutor *\n");
            foreach (string[] currency in CurrencyList)
            {
                Console.WriteLine($"    [{currency[0]}]\n");
            }
        }
        public static void BankLoan(Customer customer)
        {
            Console.WriteLine("  * Banklån * \n");

            // Checks the total balance the customer has 
            decimal totalBalance = 0;
            for (int i = 0; i < customer._accounts.Count; i++)
            {
                totalBalance = totalBalance + customer._accounts[i].Balance;
            }

            if (totalBalance >= 1000m)
            {
                decimal maxLoanAmount = totalBalance * 2;
                // Customer can borrow double the current total balance
                Console.WriteLine($"  Minimumsumman för lån är 1000,00 [SEK]. " +
                    $"Du kan låna mellan 1000,00 - {maxLoanAmount.ToString("F")} [SEK] baserat på ditt nuvarande kapital.\n");
                Console.WriteLine($"  Vi har en engångsavgift på 10% och månadsräntan är 5% av lånsumman\n");

                // Input loan amount
                Console.Write("  Vänligen ange hur mycket du vill låna: ");
                decimal loanAmount = 0;
                do
                {
                    Decimal.TryParse(Console.ReadLine(), out loanAmount);

                    if (loanAmount > totalBalance * 2 || loanAmount < 1000m)
                    {
                        Console.WriteLine($"  Ogiltligt val. Vänligen ange en summa mellan 1000 - {maxLoanAmount.ToString("F")} [SEK]");
                    }
                } while (loanAmount > totalBalance * 2 || loanAmount < 1000m);

                Console.Clear();
                Console.WriteLine($"  Du har valt att låna {loanAmount.ToString("F")} [SEK]\n");
                Console.WriteLine($"  Engångsavgift\t {(loanAmount * 0.1m).ToString("F")} [SEK]\n");

                decimal interestCost = 0;
                int months = 0;
                decimal loanAmountLeft = loanAmount;
                while (loanAmountLeft > 0m)
                {
                    interestCost = interestCost + (loanAmountLeft * 0.05m);
                    loanAmountLeft = loanAmountLeft - 100;
                    months++;
                }

                Console.WriteLine($"  Du måste minst betala av 100 [SEK] per månad och om du betalar av minimumsumman: \n" +
                    $"  Har du betalat av lånet efter {months} månader \n" +
                    $"  Kostnaden för räntan blir {interestCost.ToString("F")} [SEK]");

                // Account selection and transfer
                Console.WriteLine(PrintAccounts(customer));
                int index = 0;
                while (index < 1 || index > customer._accounts.Count)
                {
                    Console.Write("\n\n   Välj vilket konto du vill överföra pengarna till:   ");
                    Int32.TryParse(Console.ReadLine(), out index);
                }
                index += -1;
                string bankCurrency = customer._accounts[index].Currency; // Exchange currency if the to account is not SEK
                Bank.ExchangeBack(ref loanAmount, ref bankCurrency);

                customer._accounts[index].Balance += loanAmount;

                // Print summary
                Console.Clear();
                Console.Write($"\n\n   '{loanAmount.ToString("F")}' har lagts till [{customer._accounts[index].AccountNumber}]");
                Account.SubmitTransaction(customer, index, loanAmountLeft);
                Console.WriteLine(Account.PrintAccounts(customer));
                Console.ReadKey();
            }
            // If the customer doesn't have enough money for a loan
            else { Console.WriteLine("  Du har inte tillräckligt med pengar för att låna från vår bank.\n\n  Vänligen tryck 'Enter' för att fortsätta"); }
            Console.ReadKey();
        }
    }
}
