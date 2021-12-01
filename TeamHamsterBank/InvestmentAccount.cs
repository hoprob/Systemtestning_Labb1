using System;
using System.Collections.Generic;
using System.Text;

namespace TeamHamsterBank
{
    class InvestmentAccount : Account
    {
        public InvestmentAccount(string accountName, string accountType, string currency, string customerID) 
            : base(accountName, accountType, currency, customerID)
        {
        }
    }
}
