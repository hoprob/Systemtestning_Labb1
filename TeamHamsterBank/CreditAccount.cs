using System;
using System.Collections.Generic;
using System.Text;

namespace TeamHamsterBank
{
    class CreditAccount : Account
    {
        public CreditAccount(string accountName, string accountType, string currency, string customerID)
            : base(accountName, accountType, currency, customerID)
        {
        }
    }
}
