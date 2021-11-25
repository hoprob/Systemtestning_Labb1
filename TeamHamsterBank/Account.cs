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

        public int AccountNum { get => _accountNum; }

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
    }
}
