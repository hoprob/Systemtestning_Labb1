using System;
using System.Collections.Generic;
using System.Text;

namespace TeamHamsterBank
{
    class MainAccount : Account
    {
        public MainAccount(string accountName, string accountType, string currency, string customerID) 
            : base(accountName, accountType, currency, customerID)
        {
        }
    }
}
