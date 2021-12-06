using System;
using System.Collections.Generic;
using System.Text;

namespace TeamHamsterBank
{
    class FutureAccount : Account
    {
        public FutureAccount(string accountName, string accountType, string currency, string customerID) 
            : base(accountName, accountType, currency, customerID)
        {
        }
    }
}
