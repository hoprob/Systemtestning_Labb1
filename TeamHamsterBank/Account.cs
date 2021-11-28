using System;
using System.Collections.Generic;
using System.Text;

namespace TeamHamsterBank
{
    class Account
    {
        string _accountName = "New Account";
        decimal _balance;
        static int _accountNum = 100000000;
        int _newAccount;
        //This Constructor will only be called when the app starts to
        // declare the accounts that already exist.
        public Account(string accountName, decimal balance)
        {
            _accountNum++;
            _accountName = accountName;
            _newAccount = _accountNum;
            _balance = balance;
        }
        // This constructer will be called by the customer-object to open a new accounnt
        public Account(string accountName)
        {
            _accountNum++;
            _accountName = accountName;
            _newAccount = _accountNum;
            _balance = 0;
        }
        public override string ToString()
        {
            return $"{_accountName}    KontoNr: [{_newAccount}]    Saldo: {_balance:#.##}\n\n";
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
        public static void SelectAccount(Customer customer , int numberOfAccounts)
        {
            Console.Write("\n\tVälj ett konto och tryck 'Enter' eller tryck 'Enter' två gångar: ");
            Int32.TryParse(Console.ReadLine(), out int choose);
            if ( choose <= numberOfAccounts && choose > 0 )
            {
                Console.Clear();
                Console.WriteLine("\n\n\n\t"+customer._accounts[choose -1].PrintAccountHistory());
            }
        }
        public string PrintAccountHistory()
        {
            return "History of transactions is yet to be developed :-)";
        } 
        public bool EnoughBalance(decimal checkSum)
        {
            return checkSum > _balance;
        }
        public void MakeTransfer(decimal transferSum, Account toAccount)
        {
            this._balance -= transferSum;   
            toAccount._balance += transferSum;
        }
    }
}
