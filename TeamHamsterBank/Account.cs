using System;
using System.Collections.Generic;
using System.Text;

namespace TeamHamsterBank
{
    class Account
    {
        private string _accountName = "New Account";
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
        internal int AccountNumber { get  => _accountNum;  }
        private string _customerID;
        internal string CustomerID { get => _customerID; }
        private List<string[]> _transaction = new List<string[]>();
        //This Constructor will only be called when the app starts to
        // declare the accounts that already exist.
        public Account(string accountName, decimal balance, string currency, string customerID)
        {
            _staticAccountNum++;
            _accountName = accountName;
            _accountNum = _staticAccountNum;
            _balance = balance;
            _currency = currency;
            _customerID = customerID;
        }
        // This constructer will be called by the customer-object to open a new accounnt
        public Account(string accountName, string currency, string customerID)
        {
            _staticAccountNum++;
            _accountName = accountName;
            _accountNum = _staticAccountNum;
            _balance = 0.00m;
            _currency=currency;
            _customerID = customerID;
        }
        public string ToSave()
        {

            return $"{_accountName}________{_accountNum}________{_balance}" +
                   $"________{_currency}________{_customerID}\n";
        }
        public override string ToString()
        {
            return $"{_accountName}    KontoNr: [{_accountNum}]" +
                   $"    Saldo: {_balance:0.00}    {_currency}\n\n";
        }
        public static string PrintAccounts(Customer customer)
        {
            string output = String.Empty;
            for (int i = 0; i < customer._accounts.Count; i++)
            {
                output += $"[{i + 1}]    {customer._accounts[i]}";
            }
            return $"\n\n{output}";
        }
        public static void SelectAccount(Customer customer, int numberOfAccounts)
        {
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
            toAccount._balance += transferSum;
        }
    }
}
