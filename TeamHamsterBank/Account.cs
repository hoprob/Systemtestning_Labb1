using System;
using System.Collections.Generic;
using System.Text;

namespace TeamHamsterBank
{
    class Account
    {
        string _accountName = "New Account";
        int _accountNum;
        decimal _balance;
        public Account(string accountName, int accountNum)
        {
            this._accountName = accountName;
            this._accountNum = accountNum;
            this._balance = 0.0M;
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
