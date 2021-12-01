using System;
using System.Collections.Generic;
using System.Text;

namespace TeamHamsterBank
{
    class SavingsAccount : Account
    {
        decimal _interest = 1.01m;

        public SavingsAccount(string accountName, string accountType, string currency, string customerID) 
            : base (accountName, accountType, currency, customerID)
        {
            _interest = 0.01m;
        }

        public decimal CalculateSavingsInterest(decimal deposit)
        {
            return deposit * _interest;
        }
    }
}
